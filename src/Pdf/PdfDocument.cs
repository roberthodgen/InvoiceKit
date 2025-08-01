namespace InvoiceKit.Pdf;

using Layouts.Text;
using Layouts;
using Layouts.Images;
using Layouts.Stacks;
using Layouts.Tables;
using SkiaSharp;
using Styles.Text;
using Svg.Skia;

public class PdfDocument : IDisposable
{
    private const float PointsPerInch = 72f;
    private const float Margin = 50f;

    public static PdfDocument UsLetter => new (8.5f * PointsPerInch, 11f * PointsPerInch);

    private readonly SKSize _pageSize;
    private readonly MemoryStream _stream = new ();
    private readonly SKDocument _document;

    private List<IDrawable> _blocks = [];
    private bool _debug = false;

    public TextStyle DefaultTextStyle { get; private set; } = new ();

    public PdfDocument(float width, float height)
    {
        _pageSize = new SKSize(width, height);
        _document = SKDocument.CreatePdf(_stream);
    }

    /// <summary>
    /// Adds a page break.
    /// </summary>
    public PdfDocument AddPageBreak()
    {
        _blocks.Add(new PageBreak());
        return this;
    }

    public PdfDocument DefaultFont(string fontPath, float fontSize = TextStyle.DefaultFontSize, SKColor? color = null)
    {
        DefaultTextStyle = new TextStyle
        {
            FontPath = fontPath,
            FontSize = fontSize,
            Color = color ?? SKColors.Black
        };

        return this;
    }

    /// <summary>
    /// Adds a new block.
    /// </summary>
    public PdfDocument AddBlock(Func<PdfDocument, IDrawable> blockFactory)
    {
        var block = blockFactory(this);
        _blocks.Add(block);
        return this;
    }

    /// <summary>
    /// Adds a new text block.
    /// </summary>
    public PdfDocument AddTextBlock(Action<TextBlock> configureTextBlock)
    {
        var block = new TextBlock(DefaultTextStyle);
        configureTextBlock(block);
        _blocks.Add(block);
        return this;
    }

    /// <summary>
    /// Adds a new image block.
    /// </summary>
    public PdfDocument AddImageBlock(string imagePath)
    {
        var block = ImageBlock.CreateSvg(imagePath);
        _blocks.Add(block);
        return this;
    }

    /// <summary>
    /// Adds a new horizontal rule.
    /// </summary>
    public PdfDocument AddHorizontalRule()
    {
        var block = new HorizontalRule();
        _blocks.Add(block);
        return this;
    }

    /// <summary>
    /// Adds a stack of columns.
    /// </summary>
    public PdfDocument AddColumnStack(Action<HStack> configureColumnStack)
    {
        var block = new HStack();
        configureColumnStack(block);
        _blocks.Add(block);
        return this;
    }

    /// <summary>
    /// Adds spacing between blocks.
    /// </summary>
    /// <param name="height">Float for the amount of spacing. Default of 5f.</param>
    public PdfDocument AddSpacingBlock(float height = 5f)
    {
        var block = new SpacingBlock(height);
        _blocks.Add(block);
        return this;
    }
    /// <summary>
    /// Adds a new table block.
    /// </summary>
    public PdfDocument AddTableBlock(Action<TableLayoutBuilder> configureTableBlock)
    {
        var block = new TableLayoutBuilder(DefaultTextStyle);
        configureTableBlock(block);
        _blocks.Add(block);
        return this;
    }

    public byte[] Build()
    {
        var page = BeginNewPage(); // TODO handle dispose/using

        foreach (var block in _blocks)
        {
            var size = block.Measure(page.Available.Size);
            if (!page.TryAllocateRect(size, out var rect))
            {
                EndPage();
                page = BeginNewPage();
                if (block is not PageBreak)
                {
                    page.ForceAllocateRect(size, out rect);
                }
            }

            block.Draw(page, rect);
        }

        EndPage();
        _document.Close();
        return _stream.ToArray();
    }
    
    public PdfDocument DisplayLayoutGuidelines()
    {
        _debug = true;
        return this;
    }

    public void Dispose()
    {
        _stream.Dispose();
        _document.Dispose();
    }

    private PageLayout BeginNewPage()
    {
        return new PageLayout(
            _document.BeginPage(_pageSize.Width, _pageSize.Height),
            _pageSize,
            SKRect.Create(Margin, Margin, _pageSize.Width - Margin, _pageSize.Height - Margin),
            _debug);
    }

    private void EndPage()
    {
        _document.EndPage();
    }
}

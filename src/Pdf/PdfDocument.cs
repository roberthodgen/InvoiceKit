namespace InvoiceKit.Pdf;

using Elements;
using Elements.Images;
using Elements.Text;
using Layouts;
using Layouts.Stacks;
using Layouts.Tables;
using SkiaSharp;
using Styles.Text;

public class PdfDocument : IDisposable
{
    private const float PointsPerInch = 72f;
    private const float Margin = 50f;

    private readonly SKSize _pageSize;
    private readonly MemoryStream _stream = new ();
    private readonly SKDocument _document;
    private readonly List<IDrawable> _children = [];

    private bool _debug = false;

    public static PdfDocument UsLetter => new (8.5f * PointsPerInch, 11f * PointsPerInch);

    public TextStyle DefaultTextStyle { get; private set; } = new ();

    private PdfDocument(float width, float height)
    {
        _pageSize = new SKSize(width, height);
        _document = SKDocument.CreatePdf(_stream);
    }

    /// <summary>
    /// Adds a page break.
    /// </summary>
    public PdfDocument AddPageBreak()
    {
        _children.Add(new PageBreak());
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

    public byte[] Build()
    {
        var page = BeginNewPage(); // TODO handle dispose/using

        foreach (var child in _children)
        {
            var size = child.Measure(page.Available.Size);
            if (!page.TryAllocateRect(size, out var rect))
            {
                // TODO temporarily disabled pagination while switching to the VStack child layout
                // EndPage();
                // page = BeginNewPage();
                if (child is not PageBreak)
                {
                    page.ForceAllocateRect(size, out rect);
                }
            }

            child.Draw(page, rect);
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

    public PdfDocument WithHStack(Action<HStack> action)
    {
        var hStack = new HStack(DefaultTextStyle);
        action(hStack);
        _children.Add(hStack);
        return this;
    }

    public PdfDocument WithVStack(Action<VStack> action)
    {
        var vStack = new VStack(DefaultTextStyle);
        action(vStack);
        _children.Add(vStack);
        return this;
    }

    public PdfDocument WithTable(Action<TableLayoutBuilder> action)
    {
        var table = new TableLayoutBuilder(DefaultTextStyle);
        action(table);
        _children.Add(table);
        return this;
    }

    public PdfDocument WithText(Action<TextBlock> action)
    {
        var textBlock = new TextBlock(DefaultTextStyle);
        action(textBlock);
        _children.Add(textBlock);
        return this;
    }

    public PdfDocument WithImage(Func<ImageBuilder, IDrawable> builder)
    {
        var image = builder(new ImageBuilder());
        _children.Add(image);
        return this;
    }
}

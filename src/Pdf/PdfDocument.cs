namespace InvoiceKit.Pdf;

using Elements;
using Elements.Images;
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

    private IDrawable? _drawable;
    private bool _debug;

    public static PdfDocument UsLetter => new (8.5f * PointsPerInch, 11f * PointsPerInch);

    private TextStyle DefaultTextStyle { get; set; } = new ();

    private PdfDocument(float width, float height)
    {
        _pageSize = new SKSize(width, height);
        _document = SKDocument.CreatePdf(_stream);
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
        using var context = new MultiPageContext(BeginNewPage, _debug);
        _drawable?.Draw(context, context.GetCurrentPage().Available);
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
        _document.Dispose();
        _stream.Dispose();
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
        _drawable = hStack;
        return this;
    }

    public PdfDocument WithVStack(Action<VStack> action)
    {
        var vStack = new VStack(DefaultTextStyle);
        action(vStack);
        _drawable = vStack;
        return this;
    }

    public PdfDocument WithTable(Action<TableLayoutBuilder> action)
    {
        var table = new TableLayoutBuilder(DefaultTextStyle);
        action(table);
        _drawable = table;
        return this;
    }

    public PdfDocument WithText(Func<TextBuilder, IDrawable> builder)
    {
        _drawable = builder(new TextBuilder(DefaultTextStyle));
        return this;
    }

    public PdfDocument WithImage(Func<ImageBuilder, IDrawable> builder)
    {
        _drawable = builder(new ImageBuilder());
        return this;
    }
}

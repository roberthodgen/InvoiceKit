namespace InvoiceKit.Pdf;

using Containers.Stacks;
using SkiaSharp;
using Styles.Text;

public class PdfDocument : IDisposable
{
    private const float PointsPerInch = 72f;
    private const float Margin = 50f;

    private readonly SKSize _pageSize;
    private readonly MemoryStream _stream = new ();
    private readonly SKDocument _document;

    private IViewBuilder? _rootViewBuilder;

    private bool _debug;

    public static PdfDocument UsLetter => new (8.5f * PointsPerInch, 11f * PointsPerInch);

    private TextStyle DefaultTextStyle { get; set; } = new ();

    private SKRect DrawableArea => SKRect.Create(Margin, Margin, _pageSize.Width - Margin, _pageSize.Height - Margin);

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
        if (_rootViewBuilder is null)
        {
            throw new ApplicationException("No root view builder added to document.");
        }

        foreach (var page in new LayoutTree(_rootViewBuilder).ToPages(DrawableArea))
        {
            var canvas = _document.BeginPage(_pageSize.Width, _pageSize.Height);
            var drawableContext = new DrawableContext(canvas, _debug);
            foreach (var drawable in page.Drawables)
            {
                drawable.Draw(drawableContext);
            }

            _document.EndPage();
        }

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

    /// <summary>
    /// Should only every start with a VStack.
    /// Might need to create a StarterStack class that handles different logic that a basic VStack.
    /// </summary>
    public PdfDocument WithVStack(Action<VStack> action)
    {
        var vStack = new VStack(DefaultTextStyle);
        action(vStack);
        _rootViewBuilder = vStack;
        return this;
    }
}

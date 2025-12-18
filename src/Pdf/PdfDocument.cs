namespace InvoiceKit.Pdf;

using Geometry;
using SkiaSharp;
using Views;

/// <summary>
/// The PDF document renderer takes inspiration from SwiftUI's flexible layout structure: HStack and VStack are used to
/// layout blocks horizontally or vertically, respectively. Combed with text, images, and tables, this provides a
/// flexible path to building complex layouts for invoices and other documents.
/// </summary>
/// <remarks>
/// Units are points (1 inch = 72 points). Currently, 1 inch on every side.
/// </remarks>
public sealed class PdfDocument : IPdfDocument
{
    private const float PointsPerInch = 72f;

    private readonly SKSize _pageSize;
    private readonly MemoryStream _stream = new ();
    private readonly SKDocument _document;

    private IViewBuilder? _rootViewBuilder;
    private bool _debug;

    /// <summary>
    /// Creates a document with US Letter dimensions.
    /// </summary>
    public static PdfDocument UsLetter => new (8.5f * PointsPerInch, 11f * PointsPerInch);

    private BlockStyle DocumentStyle { get; set; } = new ()
    {
        Margin = new Margin(1f * PointsPerInch)
    };

    private SKRect DrawableArea => DocumentStyle.GetContentRect(new OuterRect(_pageSize)).ToRect();

    private PdfDocument(float width, float height)
    {
        _pageSize = new SKSize(width, height);
        _document = SKDocument.CreatePdf(_stream);
    }

    /// <summary>
    /// Renders all views in this document into a PDF byte array.
    /// </summary>
    /// <returns>A byte array containing the PDF document.</returns>
    public byte[] Build()
    {
        if (_rootViewBuilder is null)
        {
            throw new ApplicationException("No root view builder added to document.");
        }

        using var layout = new LayoutEngine(_rootViewBuilder);
        layout.LayoutDocument(DrawableArea);
        foreach (var page in layout.Pages)
        {
            using var canvas = _document.BeginPage(_pageSize.Width, _pageSize.Height);
            using var drawableContext = new DrawableContext(canvas, DrawableArea, _debug);
            foreach (var drawable in page.Drawables)
            {
                drawable.Draw(drawableContext);
            }

            _document.EndPage();
        }

        _document.Close();
        return _stream.ToArray();
    }

    /// <summary>
    /// Defines the margin and padding for the document.
    /// </summary>
    /// <param name="configureStyle"></param>
    /// <returns></returns>
    public PdfDocument WithDocumentStyle(Func<BlockStyle, BlockStyle> configureStyle)
    {
        DocumentStyle = configureStyle(DocumentStyle);
        return this;
    }

    /// <summary>
    /// Enables visual layout guidelines while rendering (useful for debugging).
    /// </summary>
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
    /// Defines the root layout as a vertical stack. You compose the entire view tree here.
    /// </summary>
    public PdfDocument WithVStack(Action<VStack> action)
    {
        var vStack = new VStack(DocumentStyle);
        action(vStack);
        _rootViewBuilder = vStack;
        return this;
    }
}

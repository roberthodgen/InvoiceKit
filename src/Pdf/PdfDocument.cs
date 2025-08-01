namespace InvoiceKit.Pdf;

using Layouts;
using SkiaSharp;
using Styles.Text;

public class PdfDocument : LayoutBuilderBase, IDisposable
{
    private const float PointsPerInch = 72f;
    private const float Margin = 50f;

    public static PdfDocument UsLetter => new (8.5f * PointsPerInch, 11f * PointsPerInch);

    private readonly SKSize _pageSize;
    private readonly MemoryStream _stream = new ();
    private readonly SKDocument _document;

    private bool _debug = false;

    public PdfDocument(float width, float height)
        : base(new TextStyle())
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
                EndPage();
                page = BeginNewPage();
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
}

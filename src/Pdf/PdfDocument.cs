namespace InvoiceKit.Pdf;

using Layouts;
using SkiaSharp;
using Styles.Text;

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

    public async Task SaveAsPdfAsync(string path, CancellationToken ct = default)
    {
        var bytes = Build();
        await File.WriteAllBytesAsync(path, bytes, ct);
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

    private byte[] Build()
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

    private PageLayout BeginNewPage()
    {
        return new PageLayout(
            _document.BeginPage(_pageSize.Width, _pageSize.Height),
            _pageSize,
            SKRect.Create(Margin, Margin, _pageSize.Width - (Margin * 2), _pageSize.Height - (Margin * 2)),
            _debug);
    }

    private void EndPage()
    {
        _document.EndPage();
    }
}

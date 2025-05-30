namespace InvoiceKit.Pdf;

using Columns;
using SkiaSharp;
using Tables;

public class PdfDocumentBuilder : IDisposable
{
    private const float PointsPerInch = 72f;

    public static PdfDocumentBuilder UsLetter => new (8.5f * PointsPerInch, 11f * PointsPerInch);

    private readonly float _width;
    private readonly MemoryStream _stream = new ();
    private readonly SKDocument _document;
    private readonly SKCanvas _canvas;
    private readonly List<IRenderable> _elements = new ();

    private TextStyle _defaultTextStyle = new ();

    public PdfDocumentBuilder(float width, float height)
    {
        _width = width;
        _document = SKDocument.CreatePdf(_stream);
        _canvas = _document.BeginPage(_width, height);
    }

    public PdfDocumentBuilder UseColumns(Action<ColumnLayoutBuilder> config)
    {
        var rect = new SKRect(50, 50, _width - 50, 200);
        var builder = new ColumnLayoutBuilder(_canvas, rect, _defaultTextStyle);
        config(builder);
        _elements.Add(builder);
        return this;
    }

    public PdfDocumentBuilder UseTable(Action<TableLayoutBuilder> config)
    {
        var rect = new SKRect(50, 250, _width - 50, 600);
        var builder = new TableLayoutBuilder(_canvas, rect, _defaultTextStyle);
        config(builder);
        _elements.Add(builder);
        return this;
    }

    public PdfDocumentBuilder DefaultFont(string fontPath, float fontSize = 12f, SKColor? color = null)
    {
        _defaultTextStyle = new TextStyle
        {
            FontPath = fontPath,
            FontSize = fontSize,
            Color = color ?? SKColors.Black
        };

        return this;
    }

    public byte[] Build()
    {
        foreach (var element in _elements)
        {
            element.Render();
        }

        _document.EndPage();
        _document.Close();
        return _stream.ToArray();
    }

    public void Dispose()
    {
        _stream.Dispose();
        _document.Dispose();
        _canvas.Dispose();
    }
}

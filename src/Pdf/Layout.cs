namespace InvoiceKit.Pdf;

using SkiaSharp;

public class Layout
{
    private const float _margin = 50;

    private readonly SKCanvas _canvas;
    private readonly SKPaint _textPaint = new ();
    private readonly SKFont _font = new ();

    public Layout(SKCanvas canvas)
    {
        _canvas = canvas;
        _textPaint.Color = SKColors.Black;
        _textPaint.IsAntialias = true;
        _font.Size = 12;
    }

    public void DrawText(string text, float x, float y)
    {
        _canvas.DrawText(text, x, y, SKTextAlign.Left, _font, _textPaint);
    }

    public void DrawTableHeader(float startY, float[] colWidths, string[] headers)
    {
        var x = _margin;
        for (var i = 0; i < headers.Length; i++)
        {
            DrawText(headers[i], x + 4, startY);
            x += colWidths[i];
        }

        // Draw bottom line of header row
        _canvas.DrawLine(_margin, startY + 6, _margin + colWidths.Sum(), startY + 6, _textPaint);
    }

    public void DrawRow(float startY, float[] colWidths, string[] cells)
    {
        var x = _margin;
        for (var i = 0; i < cells.Length; i++)
        {
            DrawText(cells[i], x + 4, startY);
            x += colWidths[i];
        }
    }
}

namespace InvoiceKit.Pdf;

using SkiaSharp;

public class Layout
{
    private const float documentMargin = 50;

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

    public void DrawText(string text, SKRect rect)
    {
        _canvas.DrawRect(rect, new SKPaint
        {
            Color = SKColors.AliceBlue,
        });

        _canvas.DrawText(text, rect.Left, rect.Bottom, SKTextAlign.Left, _font, _textPaint);
    }

    public void DrawCurrency(string text, SKRect rect)
    {
        _canvas.DrawRect(rect, new SKPaint
        {
            Color = SKColors.Beige,
        });

        _canvas.DrawText(text, rect.Right, rect.Bottom, SKTextAlign.Right, _font, _textPaint);
    }

    public void DrawTableHeader(float startY, float[] colWidths, string[] headers)
    {
        var start = new SKPoint(documentMargin, startY);
        for (var i = 0; i < headers.Length; i++)
        {
            var size = new SKSize(colWidths[0], 25);
            var rect = SKRect.Create(start, size);
            DrawText(headers[i], rect);
            start.Offset(colWidths[i], 0);
        }

        // Draw bottom line of header row
        _canvas.DrawLine(
            new SKPoint(documentMargin, startY + 25),
            new SKPoint(documentMargin + colWidths.Sum(), startY + 25),
            _textPaint);
    }

    public void DrawRow(float startY, float[] colWidths, string[] cells)
    {
        var start = new SKPoint(documentMargin, startY);
        for (var i = 0; i < cells.Length; i++)
        {
            var size = new SKSize(colWidths[0], 25);
            var rect = SKRect.Create(start, size);
            DrawText(cells[i], rect);
            start.Offset(colWidths[i], 0);
        }
    }
}

namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;

internal class HorizontalRuleDrawable(SKRect rect, SKPaint paint) : IDrawable
{
    public void Draw(IDrawableContext context)
    {
        context.Canvas.DrawLine(
            rect.Location,
            SKPoint.Add(rect.Location, new SKSize(rect.Width, 0)),
            paint);
    }

    public void Dispose()
    {
    }
}

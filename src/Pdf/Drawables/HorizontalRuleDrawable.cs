namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;

internal sealed class HorizontalRuleDrawable(SKRect rect) : IDrawable
{
    public void Draw(IDrawableContext context)
    {
        context.Canvas.DrawLine(
            rect.Location,
            SKPoint.Add(rect.Location, new SKSize(rect.Width, 0)),
            new SKPaint
            {
                Color = SKColors.Black,
                StrokeWidth = 1f,
            });
    }

    public void Dispose()
    {
    }
}

namespace InvoiceKit.Pdf;

using Layouts;
using SkiaSharp;

public sealed class HorizontalRule : IDrawable
{
    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, 1);
    }

    public void Draw(PageLayout page, SKRect rect)
    {
        page.Canvas.DrawLine(
            rect.Location,
            SKPoint.Add(rect.Location, new SKSize(rect.Width, 0)),
            new SKPaint
            {
                Color = SKColors.Black,
                StrokeWidth = 1f,
            });
    }
}

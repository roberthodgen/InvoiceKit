namespace InvoiceKit.Pdf.Elements;

using Layouts;
using SkiaSharp;

public sealed class HorizontalRuleDrawable : IDrawable
{
    public HorizontalRuleDrawable()
    {
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, 1);
    }

    public void Draw(PageLayout page)
    {
        page.Canvas.DrawLine(
            page.Available.Location,
            SKPoint.Add(page.Available.Location, new SKSize(page.Available.Width, 0)),
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

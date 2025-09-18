namespace InvoiceKit.Pdf.Elements;

using Containers;
using SkiaSharp;

public sealed class HorizontalRuleDrawable : IDrawable
{
    public SKRect SizeAndLocation { get; }

    public HorizontalRuleDrawable(SKRect rect)
    {
        SizeAndLocation = rect;
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, 1);
    }

    public void Draw(SKCanvas canvas, PageLayout page)
    {
        canvas.DrawLine(
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

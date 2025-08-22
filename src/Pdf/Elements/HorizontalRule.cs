namespace InvoiceKit.Pdf.Elements;

using SkiaSharp;

public sealed class HorizontalRule : IDrawable
{
    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, 1);
    }

    public void Draw(MultiPageContext context, SKRect rect)
    {
        context.GetCurrentPage().Canvas.DrawLine(
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

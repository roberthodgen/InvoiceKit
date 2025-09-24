namespace InvoiceKit.Pdf.Elements.HorizontalRule;

using Containers;
using SkiaSharp;

public sealed class HorizontalRuleDrawable : IDrawable
{
    public SKRect SizeAndLocation { get; }

    public HorizontalRuleDrawable(SKRect rect)
    {
        SizeAndLocation = rect;
    }

    public void Draw(IDrawableContext context)
    {
        context.Canvas.DrawLine(
            SizeAndLocation.Location,
            SKPoint.Add(SizeAndLocation.Location, new SKSize(SizeAndLocation.Width, 0)),
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

namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;
using Styles;

internal class HorizontalRuleDrawable(SKRect rect, BlockStyle style) : IDrawable
{
    public void Draw(IDrawableContext context)
    {
        context.Canvas.DrawLine(
            rect.Location,
            SKPoint.Add(rect.Location, new SKSize(rect.Width, 0)),
            style.ForegroundToPaint());
    }

    public void Dispose()
    {
    }
}

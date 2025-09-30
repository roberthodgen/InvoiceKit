namespace InvoiceKit.Pdf.Containers.Stacks;

using SkiaSharp;

internal class DebugDrawable(SKRect rect) : IDrawable
{
    public void Dispose()
    {
    }

    public void Draw(IDrawableContext context)
    {
        if (context.Debug)
        {
            context.Canvas.DrawRect(
                rect,
                new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Cyan,
                    StrokeWidth = 1f,
                });
        }
    }
}

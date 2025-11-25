namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;

internal class DebugDrawable(SKRect rect, SKColor color) : IDrawable
{
    public static SKColor DrawableAreaDebug => SKColors.Magenta;

    public static SKColor AllocatedDebug => SKColors.Cyan;

    public static SKColor MarginDebug => SKColors.Cyan;

    public static SKColor PaddingDebug => SKColors.Magenta;

    public static SKColor ContentDebug => SKColors.Yellow;

    public void Draw(IDrawableContext context)
    {
        if (context.Debug)
        {
            context.Canvas.DrawRect(
                rect,
                new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = color,
                    StrokeWidth = 1f,
                });
        }
    }

    public void Dispose()
    {
    }
}

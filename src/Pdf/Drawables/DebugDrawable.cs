namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;

internal class DebugDrawable(SKRect rect, SKColor color) : IDrawable
{
    public static SKColor MarginDebug => SKColors.Red;

    public static SKColor TextDebug => SKColors.Gray;

    public static SKColor PaddingDebug => SKColors.Blue;

    public static SKColor AllocatedDebug => SKColors.Cyan;

    public static SKColor DrawableAreaDebug => SKColors.Magenta;

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

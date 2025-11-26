namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;

internal class DebugDrawable(SKRect rect, SKColor color) : IDrawable
{
    public static SKColor DrawableAreaColor => SKColors.DarkRed;

    public static SKColor AllocatedColor => SKColors.DarkBlue;

    public static SKColor MarginColor => SKColors.Cyan;

    public static SKColor PaddingColor => SKColors.Magenta;

    public static SKColor ContentColor => SKColors.Yellow;

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

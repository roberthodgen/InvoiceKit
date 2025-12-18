namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;

internal class DebugDrawable(SKRect rect, BlockStyle style) : IDrawable
{
    public void Draw(IDrawableContext context)
    {
        if (context.Debug == false)
        {
            return;
        }

        // // Draw Margin Debug
        // if (style.Margin.HasValue)
        // {
        //     context.Canvas.DrawRect(
        //         style.GetMarginDebugRect(rect),
        //         new SKPaint
        //         {
        //             Style = SKPaintStyle.Stroke,
        //             Color = SKColors.Yellow,
        //         });
        // }
        //
        // // Draw Padding Debug
        // if (style.Padding.HasValue)
        // {
        //     context.Canvas.DrawRect(
        //         style.GetPaddingDebugRect(rect),
        //         new SKPaint
        //         {
        //             Style = SKPaintStyle.Stroke,
        //             Color = SKColors.Magenta,
        //         });
        // }

        // Draw Content Debug
        context.Canvas.DrawRect(
            rect,
            new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Cyan,
            });
    }

    public void Dispose()
    {
    }
}

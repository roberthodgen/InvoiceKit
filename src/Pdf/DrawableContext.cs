namespace InvoiceKit.Pdf;

using SkiaSharp;

internal class DrawableContext : IDrawableContext
{
    public SKCanvas Canvas { get; }

    public bool Debug { get; }

    internal DrawableContext(SKCanvas canvas, SKRect drawableArea, bool debug)
    {
        Canvas = canvas;
        Debug = debug;

        if (debug)
        {
            canvas.DrawRect(
                drawableArea,
                new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Black,
                    StrokeWidth = 1f,
                });
        }
    }

    public void Dispose()
    {
        Canvas.Dispose();
    }
}

namespace InvoiceKit.Pdf;

using SkiaSharp;

internal class DrawableContext : IDrawableContext
{
    public SKCanvas Canvas { get; }

    public bool Debug { get; }

    internal DrawableContext(SKCanvas canvas, bool debug)
    {
        Canvas = canvas;
        Debug = debug;
    }

    public void Dispose()
    {
        Canvas.Dispose();
    }
}

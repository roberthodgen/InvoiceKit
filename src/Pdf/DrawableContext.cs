namespace InvoiceKit.Pdf;

using SkiaSharp;

public class DrawableContext : IDrawableContext
{
    public SKCanvas Canvas { get; }

    public bool Debug { get; }

    internal DrawableContext(SKCanvas canvas, bool debug)
    {
        Canvas = canvas;
        Debug = debug;
    }

}

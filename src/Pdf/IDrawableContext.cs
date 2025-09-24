namespace InvoiceKit.Pdf;

using SkiaSharp;

public interface IDrawableContext
{
    public SKCanvas Canvas { get; }

    public bool Debug { get; }
}

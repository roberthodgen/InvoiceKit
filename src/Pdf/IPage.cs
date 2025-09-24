namespace InvoiceKit.Pdf;

using SkiaSharp;

public interface IPage
{
    List<IDrawable> Drawables { get; }
}

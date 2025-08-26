namespace InvoiceKit.Pdf;

using SkiaSharp;

public interface IPage
{
    IEnumerable<IDrawable> Drawables { get; }
    SKSize AvailableSize { get; }
    bool IsFullyDrawn { get; }
}

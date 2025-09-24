namespace InvoiceKit.Pdf;

using SkiaSharp;

public interface ILayoutTree
{
    public List<IPage> ToPages(SKRect pageSize);
}

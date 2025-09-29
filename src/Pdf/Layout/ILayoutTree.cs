namespace InvoiceKit.Pdf.Layout;

using SkiaSharp;

public interface ILayoutTree
{
    public List<IPage> ToPages(SKRect pageSize);
}

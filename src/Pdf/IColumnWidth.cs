namespace InvoiceKit.Pdf;

using Geometry;

public interface IColumnWidth
{
    OuterSize GetColumnWidth(ILayoutContext context);
}

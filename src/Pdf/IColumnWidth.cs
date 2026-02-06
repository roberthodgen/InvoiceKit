namespace InvoiceKit.Pdf;

using Geometry;

public interface IColumnWidth
{
    OuterSize GetColumnSize(ILayoutContext context);
}

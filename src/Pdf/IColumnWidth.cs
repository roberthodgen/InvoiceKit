namespace InvoiceKit.Pdf;

using Geometry;

public interface IColumnWidth
{
    OuterRect GetColumnWidth(HorizonalLayoutContext context);
}

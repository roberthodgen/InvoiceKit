namespace InvoiceKit.Pdf;

using Containers.Tables;

public interface IColumnWidth
{
    public ColumnWidthType Type { get; }

    public float Width { get; }
}

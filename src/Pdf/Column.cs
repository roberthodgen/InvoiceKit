namespace InvoiceKit.Pdf;

public sealed class Column
{
    public ILayout Layout { get; }

    public IColumnWidth ColumnWidth { get; }

    public Column(ILayout layout, IColumnWidth columnWidth)
    {
        Layout = layout;
        ColumnWidth = columnWidth;
    }
}

namespace InvoiceKit.Pdf;

using Containers.Tables;

public sealed class ColumnWidth : IColumnWidth
{
    public ColumnWidthType Type { get; }

    public float Width { get; }

    internal ColumnWidth(ColumnWidthType type, float width)
    {
        Type = type;
        Width = width;
    }
}

namespace InvoiceKit.Pdf;

using Geometry;

public sealed class ColumnWidthEqual : IColumnWidth
{
    private readonly int _columnCount;

    private ColumnWidthEqual(int columnCount)
    {
        _columnCount = columnCount;
    }

    public OuterSize GetColumnSize(ILayoutContext context)
    {
        var width = context.Available.Width / _columnCount;
        return new OuterSize(width, context.Available.Height);
    }

    public static List<IColumnWidth> CreateEqualColumns(int columnCount)
    {
        return Enumerable.Range(0, columnCount).Select(_ => new ColumnWidthEqual(columnCount) as IColumnWidth).ToList();
    }
}

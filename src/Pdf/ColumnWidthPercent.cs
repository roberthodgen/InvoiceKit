namespace InvoiceKit.Pdf;

using Geometry;

public sealed record ColumnWidthPercent : IColumnWidth
{
    private float Percent { get; }

    private ColumnWidthPercent(float percent)
    {
        Percent = percent;
    }

    public static ColumnWidthPercent FromPercent(float percent)
    {
        return new ColumnWidthPercent(percent);
    }

    public OuterSize GetColumnWidth(ILayoutContext context)
    {
        var columnSize = new OuterSize(context.Available.Width * (Percent / 100), context.Available.Height);

        if (context.CanFit(columnSize) == false)
        {
            throw new ApplicationException(
                "Column width cannot fit, check that all percentages do not exceed 100 percent.");
        }

        return columnSize;
    }
}

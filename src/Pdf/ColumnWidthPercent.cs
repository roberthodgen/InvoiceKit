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

    public OuterRect GetColumnWidth(HorizonalLayoutContext context)
    {
        var width = context.Available.Width * (Percent / 100);

        var column = new OuterRect(
            context.Available.Left,
            context.Available.Top,
            context.Available.Left + width,
            context.Available.Bottom);

        if (context.CanFit(column.ToSize()) == false)
        {
            throw new ApplicationException(
                "Column width cannot fit, check that all percentages do not exceed 100 percent.");
        }

        return column;
    }
}

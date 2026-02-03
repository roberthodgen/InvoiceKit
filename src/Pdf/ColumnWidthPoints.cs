namespace InvoiceKit.Pdf;

using Geometry;

public sealed record ColumnWidthPoints : IColumnWidth
{
    private float Points { get; }

    private ColumnWidthPoints(float points)
    {
        Points = points;
    }

    public static ColumnWidthPoints FromPoints(float points)
    {
        return new ColumnWidthPoints(points);
    }

    public OuterRect GetColumnWidth(HorizonalLayoutContext context)
    {
        var column = new OuterRect(
            context.Available.Left,
            context.Available.Top,
            context.Available.Left + Points,
            context.Available.Bottom);

        if (context.CanFit(column.ToSize()) == false)
        {
            throw new ApplicationException(
                "Column width cannot fit, check that all points do not exceed available width.");
        }

        return column;
    }
}

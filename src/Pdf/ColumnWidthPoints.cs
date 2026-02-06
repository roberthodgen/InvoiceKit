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

    public OuterSize GetColumnSize(ILayoutContext context)
    {
        var columnSize = new OuterSize(Points, context.Available.Height);

        if (context.CanFit(columnSize) == false)
        {
            throw new ApplicationException(
                "Column width cannot fit, check that all points do not exceed available width.");
        }

        return columnSize;
    }
}

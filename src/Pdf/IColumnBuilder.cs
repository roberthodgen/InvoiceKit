namespace InvoiceKit.Pdf;

public interface IColumnBuilder
{
    /// <summary>
    /// Returns the stored column widths as a list.
    /// </summary>
    List<IColumnWidth> Build();

    /// <summary>
    /// Adds a column width based on a percentage of the available space.
    /// </summary>
    IColumnBuilder AddColumnPercent(float percent);

    /// <summary>
    /// Adds a column width based on a number of points.
    /// </summary>
    IColumnBuilder AddColumnPoints(float points);
}

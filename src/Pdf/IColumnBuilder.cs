namespace InvoiceKit.Pdf;

public interface IColumnBuilder
{
    /// <summary>
    /// Stores the added column widths.
    /// </summary>
    List<ColumnWidth> ColumnWidths { get; }

    /// <summary>
    /// Returns the stored column widths as a list.
    /// </summary>
    List<ColumnWidth> Build();

    /// <summary>
    /// Adds a column width based on a percentage of the available space.
    /// </summary>
    IColumnBuilder AddColumnPercent(float percent);

    /// <summary>
    /// Adds a column width based on a number of points.
    /// </summary>
    IColumnBuilder AddColumnPoints(float points);
}

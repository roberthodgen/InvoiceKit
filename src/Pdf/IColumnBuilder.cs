namespace InvoiceKit.Pdf;

using Layouts;

public interface IColumnBuilder
{
    /// <summary>
    /// Adds a column width based on a percentage of the available space.
    /// </summary>
    IColumnBuilder AddColumnPercent(float percent);

    /// <summary>
    /// Adds a column width based on a number of points.
    /// </summary>
    IColumnBuilder AddColumnPoints(float points);

    /// <summary>
    ///
    /// </summary>
    IReadOnlyList<IColumnWidth> ColumnWidths { get; }

    ColumnType ColumnType { get; }
}

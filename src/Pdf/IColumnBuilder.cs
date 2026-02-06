namespace InvoiceKit.Pdf;

/// <summary>
/// Used inside other builders to define column widths for layouts.
/// </summary>
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
    /// Stores the column widths for the layout.
    /// </summary>
    IReadOnlyList<IColumnWidth> ColumnWidths { get; }

    /// <summary>
    /// Specifies how columns are laid out for an HStackLayout.
    /// </summary>
    ColumnType ColumnType { get; }
}

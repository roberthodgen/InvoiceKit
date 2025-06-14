namespace InvoiceKit.Pdf.Layouts.Tables;

using Styles.Text;

/// <summary>
/// Reference type representing a table column. Holds a reference to all cells contained in the column.
/// </summary>
public sealed class TableColumn
{
    /// <summary>
    /// The index of this column relative to others.
    /// </summary>
    /// <remarks>
    /// Starts at <c>0</c>.
    /// </remarks>
    public int Index { get; }

    private readonly List<TableCell> _cells = [];

    internal TableColumn(int index)
    {
        Index = index;
    }

    /// <summary>
    /// Adds a cell to this column.
    /// </summary>
    /// <returns>A new <see cref="TableCell"/> for use in a row.</returns>
    internal TableCell AddCell(TextStyle style, int rowIndex)
    {
        var cell = new TableCell(style, rowIndex, Index);
        _cells.Add(cell);
        return cell;
    }
}

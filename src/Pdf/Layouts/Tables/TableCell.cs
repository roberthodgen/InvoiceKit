namespace InvoiceKit.Pdf.Layouts.Tables;

using Elements;
using Styles.Text;

/// <summary>
/// Renders a table cell. Currently only supports text via wrapping <see cref="TextLayout"/>.
/// </summary>
public class TableCell : ElementBase
{
    public int RowIndex { get; }

    public int ColumnIndex { get; }

    /// <summary>
    /// Table cells should be created by the <see cref="TableColumn"/> to ensure property tracking and assignment.
    /// </summary>
    internal TableCell(TextStyle defaultTextStyle, int rowIndex, int columnIndex)
        : base(defaultTextStyle)
    {
        RowIndex = rowIndex;
        ColumnIndex = columnIndex;
    }
}

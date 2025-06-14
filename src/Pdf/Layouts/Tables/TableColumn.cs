namespace InvoiceKit.Pdf.Layouts.Tables;

using Styles.Text;

public sealed class TableColumn
{
    public int Index { get; }

    private readonly List<TableCell> _cells = [];

    internal TableColumn(int index)
    {
        Index = index;
    }

    public TableCell AddCell(TextStyle style)
    {
        var cell = new TableCell(style);
        _cells.Add(cell);
        return cell;
    }

    public float GetMinWidth()
    {
        return 0;
    }

    public float GetMaxWidth()
    {
        return 0;
    }
}

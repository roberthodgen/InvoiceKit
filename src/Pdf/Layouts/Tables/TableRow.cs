namespace InvoiceKit.Pdf.Layouts.Tables;

using SkiaSharp;
using Styles.Text;

public class TableRow(TableLayoutBuilder table, TextStyle defaultTextStyle)
    : IDrawable
{
    private int _columnIndex = 0;

    public List<TableCell> Cells { get; } = [];

    public TextStyle Style { get; } = defaultTextStyle;

    private bool IsLastRow => table.Rows.Last() == this;

    public TableRow AddCell(Action<TableCell> config)
    {
        var column = table.GetOrAddColumn(_columnIndex);
        var cell = column.AddCell(Style, table.Rows.Count - 1);
        config(cell);
        Cells.Add(cell);
        _columnIndex++;
        return this;
    }

    /// <summary>
    /// A table row will only return a single size result despite being able to be broken down into smaller pieces. A
    /// single row is considered the smallest drawable component for a table and the entire row must be rendered on the
    /// same page.
    /// </summary>
    public SKSize Measure(SKSize available)
    {
        float height = 0;
        foreach (var cell in Cells)
        {
            var width = table.GetColumnWidth(available.Width, cell.ColumnIndex);
            var tallestCell = cell.Measure(new SKSize(width, available.Height));
            height = Math.Max(height, tallestCell.Height);
        }

        return new SKSize(available.Width, height);
    }

    public void Draw(MultiPageContext context, SKRect rect)
    {
        var top = rect.Top;
        var left = rect.Left;
        var height = Measure(rect.Size).Height;
        foreach (var cell in Cells)
        {
            var columnWidth = table.GetColumnWidth(rect.Width, cell.ColumnIndex);
            cell.Draw(context, new SKRect(left, top, left + columnWidth, top + height));
            left += columnWidth;
        }

        if (table.ShowRowSeparators && IsLastRow == false)
        {
            context.GetCurrentPage().Canvas.DrawLine(
                new SKPoint(rect.Left, rect.Bottom),
                new SKPoint(rect.Right, rect.Bottom),
                new SKPaint
                {
                    Color = SKColors.Black,
                    StrokeWidth = 1f,
                });
        }
    }

    public void Dispose()
    {
        table.Dispose();
    }
}

namespace InvoiceKit.Pdf.Containers.Tables;

using SkiaSharp;
using Styles.Text;

public class TableRow(TableViewBuilder table, TextStyle defaultTextStyle, SKRect rect) : IDrawable
{
    private int _columnIndex = 0;

    private List<TableCell> Cells { get; } = [];

    private TextStyle Style { get; } = defaultTextStyle;

    private bool IsLastRow => table.Rows.Last() == this;

    public SKRect SizeAndLocation { get; } = rect;

    public bool Debug { get; }

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
    /// single row is considered the smallest drawable component for a table, and the entire row must be rendered on the
    /// same page.
    /// </summary>
    public SKSize Measure(SKSize available)
    {
        float height = 0;
        // foreach (var cell in Cells)
        // {
        //     var width = table.GetColumnWidth(available.Width, cell.ColumnIndex);
        //     var tallestCell = cell.Measure(new SKSize(width, available.Height));
        //     height = Math.Max(height, tallestCell.Height);
        // }

        return new SKSize(available.Width, height);
    }

    public void Draw(SKCanvas canvas, Page page)
    {
        // var top = page.Available.Top;
        // var left = page.Available.Left;
        // var height = Measure(page.Available.Size).Height;
        // foreach (var cell in Cells)
        // {
        //     var columnWidth = table.GetColumnWidth(page.Available.Width, cell.ColumnIndex);
        //     cell.Draw(context, new SKRect(left, top, left + columnWidth, top + height));
        //     left += columnWidth;
        // }
        //
        // if (table.ShowRowSeparators && IsLastRow == false)
        // {
        //     canvas.DrawLine(
        //         new SKPoint(page.Available.Left, page.Available.Bottom),
        //         new SKPoint(page.Available.Right, page.Available.Bottom),
        //         new SKPaint
        //         {
        //             Color = SKColors.Black,
        //             StrokeWidth = 1f,
        //         });
        // }
    }

    public void Dispose()
    {
        table.Dispose();
    }
}

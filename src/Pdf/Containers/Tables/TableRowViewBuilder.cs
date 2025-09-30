namespace InvoiceKit.Pdf.Containers.Tables;

using SkiaSharp;
using Styles.Text;

public sealed class TableRowViewBuilder : IViewBuilder
{
    private List<TableCell> Cells { get; } = [];

    private List<ColumnWidthPercent> ColumnWidths { get; }

    private TextStyle Style { get; }

    public IReadOnlyCollection<IViewBuilder> Children => [];

    internal TableRowViewBuilder(TextStyle defaultTextStyle, List<ColumnWidthPercent> columnWidths)
    {
        Style = defaultTextStyle;
        ColumnWidths = columnWidths;
    }

    public TableRowViewBuilder AddCell(Action<TableCell> action)
    {
        var cell = new TableCell(Style);
        action(cell);
        Cells.Add(cell);
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
        // for (var i = 0; i < Cells.Count - 1; i++)
        // {
        //     var cellHeight = Cells[i].Measure(new SKSize(ColumnWidths[i].Percent, available.Height)).Height;
        //     height = Math.Max(height, cellHeight);
        // }

        return new SKSize(available.Width, height);
    }

    // public void Draw(SKCanvas canvas, IPage page)
    // {
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
    // }

    public ILayout ToLayout()
    {
        throw new NotImplementedException();
    }
}

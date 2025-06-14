namespace InvoiceKit.Pdf.Layouts.Tables;

using SkiaSharp;
using Styles.Text;

public class TableRow(TableLayoutBuilder table, TextStyle defaultTextStyle)
    : IDrawable
{
    private int _columnIndex = 0;

    public List<TableCell> Cells { get; } = [];

    public TextStyle Style { get; private set; } = defaultTextStyle;

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

    public TableRow UseText(Action<TextOptionsBuilder> options)
    {
        var builder = new TextOptionsBuilder(Style);
        options(builder);
        Style = builder.Build();
        return this;
    }

    public SKSize Measure(SKSize available)
    {
        float height = 0;
        foreach (var cell in Cells)
        {
            var width = table.GetColumnWidth(available.Width, cell.ColumnIndex);
            var cellSize = cell.Measure(new SKSize(width, available.Height));
            height = Math.Max(height, cellSize.Height);
        }

        return new SKSize(available.Width, height);
    }

    public void Draw(PageLayout page, SKRect rect)
    {
        var top = rect.Top;
        var left = rect.Left;
        var height = Measure(rect.Size).Height;
        foreach (var cell in Cells)
        {
            var columnWidth = table.GetColumnWidth(rect.Width, cell.ColumnIndex);
            cell.Draw(page, new SKRect(left, top, left + columnWidth, top + height));
            left += columnWidth;
        }

        if (table.ShowRowSeparators && IsLastRow == false)
        {
            page.Canvas.DrawLine(
                new SKPoint(rect.Left, rect.Bottom),
                new SKPoint(rect.Right, rect.Bottom),
                new SKPaint
                {
                    Color = SKColors.Black,
                    StrokeWidth = 1f,
                });
        }
    }
}

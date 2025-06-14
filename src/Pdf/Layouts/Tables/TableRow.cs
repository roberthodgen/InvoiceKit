namespace InvoiceKit.Pdf.Layouts.Tables;

using SkiaSharp;
using Styles.Text;

public class TableRow : IDrawable
{
    private int _columnIndex = 0;

    private readonly List<TableColumn> _columns;

    public List<TableCell> Cells { get; } = [];

    public TextStyle Style { get; private set; }

    private int ColumnCount => _columns.Count;

    public TableRow(TextStyle defaultTextStyle, List<TableColumn> columns)
    {
        _columns = columns;
        Style = defaultTextStyle;
    }

    public TableRow AddCell(Action<TableCell> config)
    {
        if (_columns.ElementAtOrDefault(_columnIndex) is null)
        {
            _columns.Add(new TableColumn(_columnIndex));
        }

        var cell = _columns[_columnIndex].AddCell(Style);
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
        var width = available.Width / ColumnCount;
        var cellAvailable = new SKSize(width, available.Height);
        var height = Cells.Max(cell => cell.Measure(cellAvailable).Height);
        return new SKSize(width, height);
    }

    public void Draw(PageLayout page, SKRect rect)
    {
        var columnWidth = rect.Width / ColumnCount;
        var top = rect.Top;
        var left = rect.Left;
        var height = Measure(rect.Size).Height;
        foreach (var cell in Cells)
        {
            cell.Draw(page, new SKRect(left, top, left + columnWidth, top + height));
            left += columnWidth;
        }
    }
}

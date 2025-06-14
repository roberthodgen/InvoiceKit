namespace InvoiceKit.Pdf.Layouts.Tables;

using SkiaSharp;
using Styles.Text;

public class TableLayoutBuilder : IDrawable
{
    private readonly List<TableRow> _headers = [];
    private readonly List<TableRow> _rows = [];
    private readonly List<TableColumn> _columns = [];

    private TextStyle _defaultTextStyle; // TODO make this editable for the table

    public TableLayoutBuilder(TextStyle defaultTextStyle)
    {
        _defaultTextStyle = defaultTextStyle;
    }

    public TableLayoutBuilder AddHeader(Action<TableRow> config)
    {
        var row = new TableRow(_defaultTextStyle, _columns);
        config(row);
        _headers.Add(row);
        return this;
    }

    public TableLayoutBuilder ConfigureText(Action<TextOptionsBuilder> options)
    {
        var builder = new TextOptionsBuilder(_defaultTextStyle);
        options(builder);
        _defaultTextStyle = builder.Build();
        return this;
    }

    public TableLayoutBuilder AddRow(Action<TableRow> config)
    {
        var row = new TableRow(_defaultTextStyle, _columns);
        config(row);
        _rows.Add(row);
        return this;
    }

    public SKSize Measure(SKSize available)
    {
        var width = available.Width; // always fills the available width
        var height = _rows.Sum(row => row.Measure(available).Height);
        return new SKSize(width, height);
    }

    public void Draw(PageLayout page, SKRect rect)
    {
        var x = rect.Left;
        var y = rect.Top;

        foreach (var row in _headers)
        {
            var rowHeight = row.Measure(rect.Size).Height;
            row.Draw(page, new SKRect(x, y, x + rect.Width, y + rowHeight));
            y += rowHeight;
        }
        
        foreach (var row in _rows)
        {
            // TODO detect when page changes and re-draw header row(s)
            var rowHeight = row.Measure(rect.Size).Height;
            row.Draw(page, new SKRect(x, y, x + rect.Width, y + rowHeight));
            y += rowHeight;
        }
    }
}

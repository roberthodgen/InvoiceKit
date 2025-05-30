namespace InvoiceKit.Pdf.Layouts.Tables;

using SkiaSharp;

public class TableLayoutBuilder : IDrawable
{
    private readonly SKCanvas _canvas;
    private readonly SKRect _bounds;
    private readonly List<TableRow> _rows = new();
    
    private TextStyle _defaultTextStyle; // TODO make this editable for the table

    public TableLayoutBuilder(SKCanvas canvas, SKRect bounds, TextStyle defaultTextStyle)
    {
        _canvas = canvas;
        _bounds = bounds;
        _defaultTextStyle = defaultTextStyle;
    }

    public TableLayoutBuilder AddHeader(Action<TableRow> config)
    {
        var row = new TableRow(_defaultTextStyle);
        config(row);
        _rows.Add(row);
        return this;
    }

    public float Render()
    {
        var rowHeight = 24f;
        var y = _bounds.Top;

        foreach (var row in _rows)
        {
            var x = _bounds.Left;
            var colWidth = (_bounds.Width) / row.Cells.Count;
            foreach (var cell in row.Cells)
            {
                var paint = cell.Style.ToPaint();
                var font = cell.Style.ToFont();
                _canvas.DrawText(cell.Text, x + 4, y + font.Size, SKTextAlign.Left, font, paint);
                x += colWidth;
            }
            y += rowHeight;
        }

        return y + 20; // add vertical spacing
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
        var row = new TableRow(_defaultTextStyle);
        config(row);
        _rows.Add(row);
        return this;
    }
}

public class TableRow
{
    public List<TableCell> Cells { get; } = new();
    
    public TextStyle Style { get; private set; }

    public TableRow(TextStyle defaultTextStyle)
    {
        Style = defaultTextStyle;
    }

    public TableRow AddCell(Action<TableCell> config)
    {
        var cell = new TableCell(Style);
        config(cell);
        Cells.Add(cell);
        return this;
    }

    public TableRow UseText(Action<TextOptionsBuilder> options)
    {
        var builder = new TextOptionsBuilder(Style);
        options(builder);
        Style = builder.Build();
        return this;
    }
}

public class TableCell
{
    public TextStyle Style { get; private set; }

    public TableCell(TextStyle style)
    {
        Style = style;
    }

    public string Text { get; private set; } = "";

    public TableCell AddText(string text)
    {
        return AddText(text, _ => { });
    }

    public TableCell AddText(string text, Action<TextOptionsBuilder> options)
    {
        var builder = new TextOptionsBuilder(Style);
        options(builder);
        Style = builder.Build();
        Text = text;
        return this;
    }
}

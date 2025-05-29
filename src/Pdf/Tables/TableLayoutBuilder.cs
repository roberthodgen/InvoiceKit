namespace InvoiceKit.Pdf.Tables;

using SkiaSharp;

public class TableLayoutBuilder : IRenderable
{
    private readonly SKCanvas _canvas;
    private readonly SKRect _bounds;
    private readonly List<TableRow> _rows = new();

    public TableLayoutBuilder(SKCanvas canvas, SKRect bounds)
    {
        _canvas = canvas;
        _bounds = bounds;
    }

    public TableLayoutBuilder AddHeader(Action<TableRow> config)
    {
        var row = new TableRow();
        config(row);
        _rows.Add(row);
        return this;
    }

    public void Render()
    {
        float rowHeight = 24f;
        float y = _bounds.Top;

        foreach (var row in _rows)
        {
            float x = _bounds.Left;
            float colWidth = (_bounds.Width) / row.Cells.Count;
            for (int i = 0; i < row.Cells.Count; i++)
            {
                var paint = new SKPaint { Color = SKColors.Black, TextSize = 14, IsAntialias = true };
                _canvas.DrawText(row.Cells[i], x + 4, y + paint.TextSize, paint);
                x += colWidth;
            }
            y += rowHeight;
        }
    }
}

public class TableRow
{
    public List<string> Cells { get; } = new();

    public TableRow AddCell(Action<TableCell> config)
    {
        var cell = new TableCell();
        config(cell);
        Cells.Add(cell.Content);
        return this;
    }
}

public class TableCell
{
    public string Content { get; private set; } = "";

    public TableCell Text(string text)
    {
        Content = text;
        return this;
    }
}


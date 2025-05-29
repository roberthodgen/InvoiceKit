namespace InvoiceKit.Pdf.Columns;

using SkiaSharp;

public class ColumnLayoutBuilder : IRenderable
{
    private readonly SKCanvas _canvas;
    private readonly SKRect _bounds;
    private readonly List<ColumnBuilder> _columns = new();

    public ColumnLayoutBuilder(SKCanvas canvas, SKRect bounds)
    {
        _canvas = canvas;
        _bounds = bounds;
    }

    public ColumnLayoutBuilder AddColumn(Action<ColumnBuilder> config)
    {
        var column = new ColumnBuilder();
        config(column);
        _columns.Add(column);
        return this;
    }

    public void Render()
    {
        float columnWidth = _bounds.Width / _columns.Count;
        for (int i = 0; i < _columns.Count; i++)
        {
            var columnRect = new SKRect(
                _bounds.Left + i * columnWidth,
                _bounds.Top,
                _bounds.Left + (i + 1) * columnWidth,
                _bounds.Bottom
            );
            _columns[i].Render(_canvas, columnRect);
        }
    }
}


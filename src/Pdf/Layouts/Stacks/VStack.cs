namespace InvoiceKit.Pdf.Layouts.Stacks;

using SkiaSharp;

/// <summary>
/// Renders content vertically. Each row is rendered on a new line.
/// </summary>
public class VStack : IDrawable
{
    private readonly List<IDrawable> _rows = [];

    public SKSize Measure(SKSize available)
    {
        return available; // VStack will use all available height
    }

    public void Draw(PageLayout page, SKRect rect)
    {
        if (_rows.Count == 0)
        {
            return;
        }

        var childHeight = rect.Height / _rows.Count;
        var top = rect.Top;
        foreach (var _row in _rows)
        {
            var childRect = new SKRect(rect.Left, top, rect.Right, top + childHeight);
            _row.Draw(page, childRect);
            top += childHeight;
        }
    }

    public VStack AddRow(IDrawable row)
    {
        _rows.Add(row);
        return this;
    }
}

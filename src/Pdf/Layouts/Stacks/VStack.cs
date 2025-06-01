namespace InvoiceKit.Pdf.Layouts.Stacks;

using SkiaSharp;

/// <summary>
/// Renders content vertically.
/// </summary>
public class VStack : IDrawable
{
    private readonly List<IDrawable> _rows = [];

    public SKSize Measure(SKSize available)
    {
        var columnHeight = available.Height / _rows.Count;
        var columnSize = new SKSize(available.Width, columnHeight);
        var maxHeight = _rows.Max(child => child.Measure(columnSize).Height);
        return new SKSize(available.Width, maxHeight);
    }

    public void Draw(PageLayout page, SKRect rect)
    {
        throw new NotImplementedException();
    }

    public VStack Add(IDrawable row)
    {
        _rows.Add(row);
        return this;
    }
}

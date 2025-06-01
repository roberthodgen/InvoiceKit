namespace InvoiceKit.Pdf.Layouts.Stacks;

using SkiaSharp;

/// <summary>
/// Renders content horizontally.
/// </summary>
public class HStack : IDrawable
{
    private readonly List<IDrawable> _columns = [];

    public SKSize Measure(SKSize available)
    {
        var columnWidth = available.Width / _columns.Count;
        var columnSize = new SKSize(columnWidth, 0);
        var maxHeight = _columns.Max(child => child.Measure(columnSize).Height);
        return new SKSize(available.Width, maxHeight);
    }

    public void Draw(PageLayout page, SKRect rect)
    {
        var columnWidth = rect.Width / _columns.Count;
        foreach (var (column, index) in _columns.Select((column, index) => (column, index)))
        {
            var colRect = new SKRect(
                rect.Left + (index * columnWidth),
                rect.Top,
                rect.Left + ((index + 1) * columnWidth),
                rect.Bottom
            );

            column.Draw(page, colRect);
        }
    }

    /// <summary>
    /// Add another horizontal block to the current stack.
    /// </summary>
    public HStack Add(IDrawable column)
    {
        _columns.Add(column);
        return this;
    }
}

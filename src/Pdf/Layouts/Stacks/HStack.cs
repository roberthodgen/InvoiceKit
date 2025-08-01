namespace InvoiceKit.Pdf.Layouts.Stacks;

using SkiaSharp;
using Styles.Text;

/// <summary>
/// Renders content horizontally. Each column is rendered side-by-side.
/// </summary>
public class HStack : LayoutBuilderBase, IDrawable
{
    private List<IDrawable> _children = [];

    internal HStack(TextStyle defaultTextStyle)
        : base(defaultTextStyle)
    {
    }

    public SKSize Measure(SKSize available)
    {
        var columnWidth = available.Width / _children.Count;
        var columnSize = new SKSize(columnWidth, 0);
        var maxHeight = _children.Max(child => child.Measure(columnSize).Height);
        return new SKSize(available.Width, maxHeight);
    }

    public void Draw(PageLayout page, SKRect rect)
    {
        var columnWidth = rect.Width / _children.Count;
        foreach (var (column, index) in _children.Select((column, index) => (column, index)))
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
}

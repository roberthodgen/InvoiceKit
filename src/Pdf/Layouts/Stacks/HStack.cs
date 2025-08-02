namespace InvoiceKit.Pdf.Layouts.Stacks;

using SkiaSharp;
using Styles.Text;

/// <summary>
/// Renders content horizontally. Each column is rendered side-by-side.
/// </summary>
public class HStack : LayoutBase, IDrawable
{
    internal HStack(TextStyle defaultTextStyle)
        : base(defaultTextStyle)
    {
    }

    public override SKSize Measure(SKSize available)
    {
        var columnWidth = available.Width / Children.Count;
        var columnSize = new SKSize(columnWidth, 0);
        var maxHeight = Children.Max(child => child.Measure(columnSize).Height);
        return new SKSize(available.Width, maxHeight);
    }

    public override void Draw(PageLayout page, SKRect rect, Func<PageLayout> getNextPage)
    {
        var columnWidth = rect.Width / Children.Count;
        foreach (var (column, index) in Children.Select((column, index) => (column, index)))
        {
            var colRect = new SKRect(
                rect.Left + (index * columnWidth),
                rect.Top,
                rect.Left + ((index + 1) * columnWidth),
                rect.Bottom
            );

            column.Draw(page, colRect, getNextPage);
        }
    }
}

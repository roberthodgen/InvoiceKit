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

    /// <summary>
    /// Returns two sizes: one for each child.
    /// </summary>
    public override SKSize Measure(SKSize available)
    {
        var columnWidth = available.Width / Children.Count;
        var columnSize = new SKSize(columnWidth, available.Height);
        var sizes = Children.Select(child => child.Measure(columnSize)).ToList();
        var width = sizes.Sum(size => size.Width);
        var height = sizes.Sum(size => size.Height);
        return new SKSize(width, height);
    }

    // TODO: we should have a two stage drawing for these type of side-by-side elements where we:
    // 1. Draw each into the current Multi-Page Context with a beginning layout (this should push a layout onto a stack)
    // 2. Commit or finish that layout (pop the layout off the stack).
    // Each time Context is called, we use the current starting page for the given layout stack. Sub-renders would begin
    // with a new current page context. Tracking that at the MultiPageContext object will be key
    public override void Draw(MultiPageContext context, SKRect rect)
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

            column.Draw(context, colRect);
        }
    }
}

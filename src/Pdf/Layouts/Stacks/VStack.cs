namespace InvoiceKit.Pdf.Layouts.Stacks;

using SkiaSharp;
using Styles.Text;

/// <summary>
/// Renders content vertically. Each row is rendered on a new line.
/// </summary>
public class VStack : LayoutBuilderBase, IDrawable
{
    private List<IDrawable> _children = [];
    internal VStack(TextStyle defaultTextStyle)
        : base(defaultTextStyle)
    {
    }

    public SKSize Measure(SKSize available)
    {
        return available; // VStack will use all available height
    }

    public void Draw(PageLayout page, SKRect rect)
    {
        if (_children.Count == 0)
        {
            return;
        }

        var childHeight = rect.Height / _children.Count;
        var top = rect.Top;
        foreach (var child in _children)
        {
            var childRect = new SKRect(rect.Left, top, rect.Right, top + childHeight);
            child.Draw(page, childRect);
            top += childHeight;
        }
    }
}

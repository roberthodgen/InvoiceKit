namespace InvoiceKit.Pdf.Layouts.Stacks;

using SkiaSharp;
using Styles.Text;

/// <summary>
/// Renders content vertically. Each row is rendered on a new line.
/// </summary>
public class VStack : LayoutBase, IDrawable
{
    internal VStack(TextStyle defaultTextStyle)
        : base(defaultTextStyle)
    {
    }

    public override SKSize Measure(SKSize available)
    {
        var childrenSizes = Children.Select(child => child.Measure(available)).ToList();
        var maxWidth = childrenSizes.Max(child => child.Width);
        var height = childrenSizes.Sum(child => child.Height);
        return new SKSize(maxWidth, height);
    }

    public override void Draw(PageLayout page, SKRect rect)
    {
        if (Children.Count == 0)
        {
            return;
        }

        var top = rect.Top;
        foreach (var child in Children)
        {
            var childSize = child.Measure(rect.Size);
            var childRect = new SKRect(rect.Left, top, rect.Right, top + childSize.Height);
            child.Draw(page, childRect);
            top += childSize.Height;
        }
    }
}

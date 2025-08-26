namespace InvoiceKit.Pdf.Layouts.Stacks;

using SkiaSharp;
using Styles.Text;

/// <summary>
/// Renders content vertically. Each row is rendered on a new line.
/// </summary>
public class VStack : LayoutBuilderBase, IDrawable
{
    internal VStack(TextStyle defaultTextStyle)
        : base(defaultTextStyle)
    {
    }

    public SKSize Measure(SKSize available)
    {
        var sizes = Children.Select(child => child.Measure(available)).ToList();
        return new SKSize(available.Width, sizes.Sum(size => size.Height));
    }

    public void Draw(PageLayout page)
    {
        // if (Children.Count == 0) return;
        // var top = rect.Top;
        // foreach (var child in Children)
        // {
        //     var size = child.Measure(rect.Size);
        //     var childRect = new SKRect(rect.Left, top, rect.Right, top + size.Height);
        //     child.Draw(context, childRect);
        //     top += size.Height;
        // }
    }
}

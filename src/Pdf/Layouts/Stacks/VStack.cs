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
        var sizes = Children.Select(child => child.Measure(available)).ToList();
        return new SKSize(available.Width, sizes.Sum(size => size.Height));
    }

    public override void Draw(MultiPageContext context, SKRect rect)
    {
        if (Children.Count == 0) return;

        var top = rect.Top;
        var page = context.GetCurrentPage();

        foreach (var child in Children)
        {
            if (!page.TryAllocateChild(child))
            {
                context.NewPage();
            }

            if (page.TryAllocateChild(child))
            {
                var size = child.Measure(rect.Size);
                var childRect = new SKRect(rect.Left, top, rect.Right, top + size.Height);
                child.Draw(context, childRect);
                top += size.Height;
            }
        }
    }
}

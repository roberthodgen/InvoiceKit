namespace InvoiceKit.Pdf.Containers.Stacks;

using SkiaSharp;

public class VStackLayout : ILayout
{
    private Stack<ILayout> Children { get; }

    internal VStackLayout(List<ILayout> children)
    {
        Children = new Stack<ILayout>(children.AsEnumerable().Reverse());
    }

    public LayoutResult Layout(LayoutContext context)
    {
        if (Children.Count == 0)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var layout = Children.Pop();

        var layoutResult = layout.Layout(context);

        if (layoutResult.Status == LayoutStatus.NeedsNewPage)
        {
            Children.Push(layout);
        }

        return layoutResult;
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, Children.Sum(child => child.Measure(available).Height));
    }
}

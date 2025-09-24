namespace InvoiceKit.Pdf.Containers.Stacks;

using SkiaSharp;

public class VStackLayout : ILayout
{
    private List<ILayout> Children { get; }

    internal VStackLayout(List<ILayout> children)
    {
        Children = children;
    }

    public LayoutResult Layout(LayoutContext context)
    {
        if (Children.Count == 0)
        {
            return new LayoutResult([], LayoutState.IsEmpty);
        }

        var layoutResults = new List<LayoutResult>();
        var top = context.Available.Top;
        foreach (var child in Children)
        {
            var rect = new SKRect(context.Available.Left, top, context.Available.Right, context.Available.Bottom);
            layoutResults.Add(child.Layout(context));
        }

        return layoutResults.Last();
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, Children.Sum(child => child.Measure(available).Height));
    }
}

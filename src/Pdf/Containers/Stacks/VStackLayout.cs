namespace InvoiceKit.Pdf.Containers.Stacks;

using Layout;
using SkiaSharp;

internal class VStackLayout : ILayout
{
    public bool IsFullyDrawn { get; set; }

    private Queue<ILayout> Children { get; }

    internal VStackLayout(List<ILayout> children)
    {
        Children = new Queue<ILayout>(children);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        if (Children.Count == 0 || IsFullyDrawn)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var drawables = new List<IDrawable>();

        while (Children.Count > 0)
        {
            var layout = Children.Peek();

            var childContext = new LayoutContext(context.Available);
            var layoutResult = layout.Layout(childContext);
            drawables.AddRange(layoutResult.Drawables);
            context.CommitChildContext(childContext);

            if (layoutResult.Status == LayoutStatus.IsFullyDrawn)
            {
                Children.Dequeue();
            }
            else
            {
                return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
            }
        }
        IsFullyDrawn = true;
        return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, Children.Sum(child => child.Measure(available).Height));
    }
}

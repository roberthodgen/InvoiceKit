namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

internal class VStackLayout(List<ILayout> children) : ILayout
{
    private readonly Queue<ILayout> _children = new (children);

    private bool _drawn;

    public IReadOnlyCollection<ILayout> Children => [];

    public LayoutResult Layout(LayoutContext context)
    {
        var drawables = new List<IDrawable>();

        while (_children.Count > 0)
        {
            var layout = _children.Peek();

            var childContext = context.GetChildContext();
            var layoutResult = layout.Layout(childContext);
            drawables.Add(new DebugDrawable(childContext.Allocated));
            drawables.AddRange(layoutResult.Drawables);
            context.CommitChildContext(childContext);

            if (layoutResult.Status == LayoutStatus.IsFullyDrawn)
            {
                _children.Dequeue();
            }
            else
            {
                return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
            }
        }

        _drawn = true;
        return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, _children.Sum(child => child.Measure(available).Height));
    }
}

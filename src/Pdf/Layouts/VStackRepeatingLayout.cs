namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

internal class VStackRepeatingLayout(List<ILayout> children) : ILayout
{
    public SKSize Measure(SKSize available)
    {
        if (children.Count == 0)
        {
            return SKSize.Empty;
        }

        return new SKSize(available.Width, children.Sum(child => child.Measure(available).Height));
    }

    public LayoutResult Layout(LayoutContext context)
    {
        var drawables = new List<IDrawable>();

        foreach (var child in children)
        {
            var childContext = context.GetChildContext();
            var result = child.Layout(childContext);
            drawables.AddRange(result.Drawables);
            drawables.Add(new DebugDrawable(childContext.Allocated,  DebugDrawable.AllocatedColor));
            context.CommitChildContext(childContext);

            if (result.Status == LayoutStatus.NeedsNewPage)
            {
                return new LayoutResult([], LayoutStatus.NeedsNewPage);
            }
        }

        return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
    }
}

namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;
using Styles;

internal class VStackRepeatingLayout(List<ILayout> children, BlockStyle style) : ILayout
{
    public BlockStyle Style { get; } = style;

    public SKSize Measure(SKSize available)
    {
        if (children.Count == 0)
        {
            return SKSize.Empty;
        }
        var sizeAfterStyling = Style.GetContentSize(available);
        return new SKSize(sizeAfterStyling.Width, children.Sum(child => child.Measure(sizeAfterStyling).Height));
    }

    public LayoutResult Layout(LayoutContext context)
    {
        var drawables = new List<IDrawable>();

        foreach (var child in children)
        {
            var childContext = context.GetChildContext();
            var result = child.Layout(childContext);
            drawables.AddRange(result.Drawables);
            drawables.Add(new DebugDrawable(childContext.Allocated,  DebugDrawable.AllocatedDebug));
            context.CommitChildContext(childContext);

            if (result.Status == LayoutStatus.NeedsNewPage)
            {
                return new LayoutResult([], LayoutStatus.NeedsNewPage);
            }
        }

        return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
    }
}

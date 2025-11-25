namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;
using Styles;

internal class VStackRepeatingLayout(List<ILayout> children, BlockStyle style) : ILayout
{
    private BlockStyle Style { get; } = style;

    public SKSize Measure(SKSize available)
    {
        if (children.Count == 0)
        {
            return SKSize.Empty;
        }
        var stylingSize = Style.GetStyleSize();
        var sizeAfterStyling = new SKSize(available.Width - stylingSize.Width, available.Height - stylingSize.Height);
        return new SKSize(sizeAfterStyling.Width, children.Sum(child => child.Measure(sizeAfterStyling).Height));
    }

    public LayoutResult Layout(LayoutContext context)
    {
        var drawables = new List<IDrawable>();

        if (context.TryAllocate(Style.GetStyleSize()) == false)
        {
            return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
        }

        var stackContext = context.GetChildContext(Style.GetContentRect(context.Available));

        foreach (var child in children)
        {
            var childContext = stackContext.GetChildContext();
            var result = child.Layout(childContext);
            drawables.AddRange(result.Drawables);
            drawables.Add(new DebugDrawable(childContext.Allocated,  DebugDrawable.AllocatedDebug));
            stackContext.CommitChildContext(childContext);

            if (result.Status == LayoutStatus.NeedsNewPage)
            {
                context.CommitChildContext(stackContext);
                return new LayoutResult([], LayoutStatus.NeedsNewPage);
            }
        }

        context.CommitChildContext(stackContext);
        return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
    }
}

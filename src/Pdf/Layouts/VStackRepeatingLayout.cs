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

        var styleSize = Style.GetStyleSize();
        var sumChildHeight = children.Sum(child => child.Measure(Style.GetSizeAfterStyle(available)).Height);
        return new SKSize(available.Width, sumChildHeight + styleSize.Height);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        var drawables = new List<IDrawable>();

        var stackContext = context.GetChildContext(Style.GetContentRect(context.Available));

        if (context.TryAllocate(Style.GetStyleSize()) == false)
        {
            return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
        }

        foreach (var child in children)
        {
            var childContext = stackContext.GetChildContext();
            var result = child.Layout(childContext);
            drawables.AddRange(result.Drawables);
            drawables.Add(new DebugDrawable(childContext.Allocated,  DebugDrawable.AllocatedColor));
            stackContext.CommitChildContext(childContext);

            if (result.Status == LayoutStatus.NeedsNewPage)
            {
                // Add background and border drawables
                drawables.Add(new BorderDrawable(Style.GetBorderRect(stackContext.Allocated), Style.Border));
                drawables.Insert(0, new BackgroundDrawable(Style.GetBackgroundRect(stackContext.Allocated), Style.BackgroundToPaint()));

                // Add debug drawables for margin and padding
                drawables.Add(new DebugDrawable(Style.GetMarginDebugRect(stackContext.Allocated), DebugDrawable.MarginColor));
                drawables.Add(new DebugDrawable(Style.GetBackgroundRect(stackContext.Allocated), DebugDrawable.PaddingColor));

                context.CommitChildContext(stackContext);
                return new LayoutResult([], LayoutStatus.NeedsNewPage);
            }
        }

        // Add background and border drawables
        drawables.Add(new BorderDrawable(Style.GetBorderRect(stackContext.Allocated), Style.Border));
        drawables.Insert(0, new BackgroundDrawable(Style.GetBackgroundRect(stackContext.Allocated), Style.BackgroundToPaint()));

        // Add debug drawables for margin and padding
        drawables.Add(new DebugDrawable(Style.GetMarginDebugRect(stackContext.Allocated), DebugDrawable.MarginColor));
        drawables.Add(new DebugDrawable(Style.GetBackgroundRect(stackContext.Allocated), DebugDrawable.PaddingColor));

        context.CommitChildContext(stackContext);
        return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
    }
}

namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

internal class HorizontalRuleLayout(BlockStyle style) : ILayout
{
    private BlockStyle Style { get; } = style;

    public LayoutResult Layout(ILayoutContext context)
    {
        var drawables = new List<IDrawable>();
        var childContext = GetContext(context);

        if (context.TryAllocate(Style.GetStyleSize()) == false)
        {
            return LayoutResult.NeedsNewPage([]);
        }

        if (childContext.TryAllocate(new SKSize(context.Available.Width, Style.ForegroundToPaint().StrokeWidth)))
        {
            drawables.AddRange(Style.GetStyleDrawables(childContext.Allocated));
            drawables.Add(new HorizontalRuleDrawable(context.Available, Style.ForegroundToPaint()));

            childContext.CommitChildContext();
            return LayoutResult.FullyDrawn(drawables);
        }

        childContext.CommitChildContext();
        return LayoutResult.NeedsNewPage([]);
    }

    public ILayoutContext GetContext(ILayoutContext parentContext)
    {
        return parentContext.GetVerticalChildContext(Style.GetContentRect(parentContext.Available));
    }
}

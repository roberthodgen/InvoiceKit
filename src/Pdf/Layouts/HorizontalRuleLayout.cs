namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

internal class HorizontalRuleLayout(BlockStyle style) : ILayout
{
    private BlockStyle Style { get; } = style;

    public LayoutResult Layout(ILayoutContext context)
    {
        var drawables = new List<IDrawable>();
        if (context.TryAllocate(Style.GetStyleSize()) == false)
        {
            return LayoutResult.NeedsNewPage([]);
        }

        if (context.TryAllocate(new SKSize(context.Available.Width, Style.ForegroundToPaint().StrokeWidth)))
        {
            drawables.AddRange(Style.GetStyleDrawables(context.Allocated));
            drawables.Add(new HorizontalRuleDrawable(context.Available, Style.ForegroundToPaint()));
            return LayoutResult.FullyDrawn(drawables);
        }

        return LayoutResult.NeedsNewPage([]);
    }

    public ILayoutContext GetContext(ILayoutContext parentContext)
    {
        return parentContext.GetVerticalChildContext();
    }

    public ILayoutContext GetContext(ILayoutContext parentContext, SKRect intersectingRect)
    {
        return parentContext.GetVerticalChildContext(intersectingRect);
    }
}

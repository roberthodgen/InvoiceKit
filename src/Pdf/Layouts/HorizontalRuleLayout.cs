namespace InvoiceKit.Pdf.Layouts;

using Drawables;

internal class HorizontalRuleLayout(BlockStyle style) : ILayout
{
    private BlockStyle Style { get; } = style;

    public LayoutResult Layout(ILayoutContext context)
    {
        var drawables = new List<IDrawable>();
        if (context.TryAllocate(Style.GetStyleSize()))
        {
            drawables.Add(new HorizontalRuleDrawable(context.Available, Style.ForegroundToPaint()));
            return LayoutResult.FullyDrawn(drawables);
        }

        return LayoutResult.NeedsNewPage(drawables);
    }

    public ILayoutContext GetContext(ILayoutContext parentContext)
    {
        return parentContext.GetVerticalChildContext(parentContext.Available);
    }
}

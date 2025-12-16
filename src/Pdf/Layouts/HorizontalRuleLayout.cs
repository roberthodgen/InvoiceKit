namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

internal class HorizontalRuleLayout(BlockStyle style) : ILayout
{
    private BlockStyle Style { get; } = style;

    public LayoutResult Layout(ILayoutContext context)
    {
        var drawables = new List<IDrawable>();
        var childContext = context.GetVerticalChildContext(Style.GetContentRect(context.Available));

        if (childContext.TryAllocate(Style.GetStyleSize()))
        {
            drawables.Add(new HorizontalRuleDrawable(childContext.Available, Style.ForegroundToPaint()));
            return LayoutResult.FullyDrawn(drawables);
        }

        return LayoutResult.NeedsNewPage(drawables);
    }
}

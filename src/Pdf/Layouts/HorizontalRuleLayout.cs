namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;
using Styles;

internal class HorizontalRuleLayout(BlockStyle style) : ILayout
{
    private BlockStyle Style { get; } = style;

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, 1);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        var drawables = new List<IDrawable>();
        var ruleContext = context.GetChildContext(Style.GetContentRect(context.Available));

        if (context.TryAllocate(Style.GetStyleSize()) == false)
        {
            return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
        }

        if (ruleContext.TryAllocate(this, out var rect))
        {
            drawables.Add(new HorizontalRuleDrawable(rect, Style.ForegroundToPaint()));
            context.CommitChildContext(ruleContext);
            return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
        }

        context.CommitChildContext(ruleContext);
        return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
    }
}

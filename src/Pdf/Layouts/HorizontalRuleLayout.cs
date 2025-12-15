namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

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
        var childContext = context.GetChildContext(Style.GetContentRect(context.Available));

        if (context.TryAllocate(Style.GetStyleSize()) == false)
        {
            drawables.Add(new HorizontalRuleDrawable(childContext.Available, Style.ForegroundToPaint()));
            return LayoutResult.FullyDrawn(drawables);
        }

        return LayoutResult.NeedsNewPage(drawables);
    }
}

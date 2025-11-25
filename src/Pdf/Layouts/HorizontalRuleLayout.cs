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
        if (context.TryAllocate(this, out var rect))
        {
            drawables.Add(new HorizontalRuleDrawable(rect, Style));
            return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
        }

        return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
    }
}

namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

internal class HorizontalRuleLayout : ILayout
{
    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, 1);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        var drawables = new List<IDrawable>();
        if (context.TryAllocate(this, context.Available.Size, out var rect))
        {
            drawables.Add(new HorizontalRuleDrawable(rect));
            return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
        }

        return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
    }
}

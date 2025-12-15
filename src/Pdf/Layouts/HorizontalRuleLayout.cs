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
        if (context.TryAllocate(this, out var rect))
        {
            drawables.Add(new HorizontalRuleDrawable(rect));
            return LayoutResult.FullyDrawn(drawables);
        }

        return LayoutResult.NeedsNewPage(drawables);
    }
}

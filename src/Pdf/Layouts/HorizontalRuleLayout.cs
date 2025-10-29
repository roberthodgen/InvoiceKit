namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

internal class HorizontalRuleLayout : ILayout
{
    public SKSize Measure(SKRect available)
    {
        return new SKSize(available.Width, 1);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        var listDrawables = new List<IDrawable>();
        var size = Measure(context.Available);

        var rect = new SKRect(
            context.Available.Left,
            context.Available.Top,
            context.Available.Left + size.Width,
            context.Available.Top + size.Height);

        if (context.TryAllocateRect(rect))
        {
            listDrawables.Add(new HorizontalRuleDrawable(rect));
            return new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
        }

        return new LayoutResult(listDrawables, LayoutStatus.NeedsNewPage);
    }
}

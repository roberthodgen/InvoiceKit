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
        var listDrawables = new List<IDrawable>();
        var size = Measure(context.Available.Size);

        if (context.TryAllocate(size))
        {
            listDrawables.Add(new HorizontalRuleDrawable(
                new SKRect(
                context.Available.Left,
                context.Available.Top,
                context.Available.Right,
                context.Available.Top + size.Height)));
            return new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
        }

        return new LayoutResult(listDrawables, LayoutStatus.NeedsNewPage);
    }
}

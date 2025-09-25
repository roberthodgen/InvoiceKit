namespace InvoiceKit.Pdf.Elements.HorizontalRule;

using SkiaSharp;

public class HorizontalRuleLayout : ILayout
{
    internal HorizontalRuleLayout()
    {
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, 1);
    }

    /// <summary>
    ///
    /// </summary>
    public LayoutResult Layout(LayoutContext context)
    {
        var listDrawables = new List<IDrawable>();
        var size = Measure(context.Available.Size);
        while (true)
        {
            var rect = new SKRect(
                context.Available.Left,
                context.Available.Top,
                context.Available.Left + size.Width,
                context.Available.Top + size.Height);

            if (context.TryAllocateRect(rect))
            {
                listDrawables.Add(new HorizontalRuleDrawable(rect));
                break;
            }

            // Will only be hit if the page is full.
            return new LayoutResult(listDrawables, LayoutStatus.NeedsNewPage);
        }

        return new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
    }
}

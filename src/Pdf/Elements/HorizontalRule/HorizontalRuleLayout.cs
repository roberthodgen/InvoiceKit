namespace InvoiceKit.Pdf.Elements.HorizontalRule;

using SkiaSharp;

internal class HorizontalRuleLayout : ILayout
{
    public bool IsFullyDrawn { get; set; }

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
        if (IsFullyDrawn)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var listDrawables = new List<IDrawable>();
        var size = Measure(context.Available.Size);

        var rect = new SKRect(
            context.Available.Left,
            context.Available.Top,
            context.Available.Left + size.Width,
            context.Available.Top + size.Height);

        if (context.TryAllocateRect(rect))
        {
            listDrawables.Add(new HorizontalRuleDrawable(rect));
            IsFullyDrawn = true;
            return new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
        }

        // Will only be hit if the page is full.
        return new LayoutResult(listDrawables, LayoutStatus.NeedsNewPage);
    }
}

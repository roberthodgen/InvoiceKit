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

    public void LayoutPages(MultiPageContext context, bool debug)
    {
        var page = context.GetCurrentPage();
        while (true)
        {
            var size = Measure(page.Available.Size);
            var rect = new SKRect(
                page.Available.Left,
                page.Available.Top,
                page.Available.Left + size.Width,
                page.Available.Top + size.Height);

            if (page.TryAllocateRect(rect))
            {
                page.AddDrawable(new HorizontalRuleDrawable(rect, debug));
                break;
            }

            // Will only be hit if the page is full.
            page.MarkFullyDrawn();
            page = context.GetCurrentPage();
        }
    }
}

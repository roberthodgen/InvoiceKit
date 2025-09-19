namespace InvoiceKit.Pdf.Containers.SpacingBlock;

using SkiaSharp;

public class SpacingBlockLayout : ILayout
{
    private float Height { get; }

    internal SpacingBlockLayout(float height)
    {
        Height = height;
    }

    public SKSize Measure(SKSize available)
    {
        throw new NotImplementedException();
    }

    public void LayoutPages(MultiPageContext context, bool debug)
    {
        var page = context.GetCurrentPage();
        while (true)
        {
            var rect = new SKRect(page.Available.Left, page.Available.Top, page.Available.Right,
                page.Available.Top + Height);
            if (page.TryAllocateRect(rect))
            {
                break;
            }

            // Will only be hit if the page is full.
            page.MarkFullyDrawn();
            page = context.GetCurrentPage();
        }
    }
}

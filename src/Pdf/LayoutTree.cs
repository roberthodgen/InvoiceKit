namespace InvoiceKit.Pdf;

using SkiaSharp;

internal class LayoutTree(IViewBuilder root)
{
    private readonly ILayout _root = root.ToLayout();

    private bool _isFullyDrawn;

    /// Depth-first search that skips layouts that were already fully drawn.
    private PageLayoutResult LayoutPage(LayoutContext context)
    {
        var page = new Page();

        while (!_isFullyDrawn)
        {
            var childContext = context.GetChildContext();
            var layoutResult = _root.Layout(childContext, LayoutType.DrawOnceElement);
            page.AddDrawables(layoutResult.Drawables);
            context.CommitChildContext(childContext);

            if (layoutResult.Status == LayoutStatus.NeedsNewPage)
            {
                return new PageLayoutResult(page, LayoutStatus.NeedsNewPage);
            }

            _isFullyDrawn = true;
        }

        return new PageLayoutResult(page, LayoutStatus.IsFullyDrawn);
    }

    public List<Page> ToPages(SKRect drawableArea)
    {
        var pages = new List<Page>();

        var status = LayoutStatus.NeedsNewPage;
        while (status == LayoutStatus.NeedsNewPage)
        {
            var result = LayoutPage(new LayoutContext(drawableArea));
            pages.Add(result.Page);
            status = result.Status;
        }

        return pages;
    }

    private record PageLayoutResult(Page Page, LayoutStatus Status);
}

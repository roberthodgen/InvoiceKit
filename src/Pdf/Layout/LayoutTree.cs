namespace InvoiceKit.Pdf.Layout;

using SkiaSharp;

internal class LayoutTree : ILayoutTree
{
    private readonly LayoutNode _root;

    private bool _isFullyDrawn;

    public LayoutTree(IViewBuilder root)
    {
        _root = LayoutNode.CreateFromView(root);
    }

    /// Depth-first search that skips layouts that were already fully drawn.
    private PageLayoutResult LayoutPage(LayoutContext context)
    {
        var page = new Page();

        while (!_isFullyDrawn)
        {
            var childContext = new LayoutContext(context.Available);
            var layoutResult = _root.Layout.Layout(childContext);
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

    public List<IPage> ToPages(SKRect drawableArea)
    {
        var pages = new List<IPage>();

        LayoutStatus status = LayoutStatus.NeedsNewPage;
        while (status == LayoutStatus.NeedsNewPage)
        {
            var result = LayoutPage(new LayoutContext(drawableArea));
            pages.Add(result.Page);
            status = result.Status;
        }

        return pages;
    }

    private record LayoutNode(ILayout Layout, List<LayoutNode> Children)
    {
        public static LayoutNode CreateFromView(IViewBuilder root)
        {
            var children = new List<LayoutNode>();
            foreach (var child in root.Children)
            {
                children.Add(CreateFromView(child));
            }

            return new LayoutNode(root.ToLayout(), children);
        }
    }

    private record PageLayoutResult(IPage Page, LayoutStatus Status);
}

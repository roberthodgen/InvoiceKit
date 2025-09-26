namespace InvoiceKit.Pdf;

using Containers;
using SkiaSharp;

public sealed class LayoutTree : ILayoutTree
{
    private readonly LayoutNode _root;
    private readonly HashSet<ILayout> _layoutComplete = new ();

    public LayoutTree(IViewBuilder root)
    {
        _root = LayoutNode.CreateFromView(root);
    }

    /// Depth-first search that skips layouts that were already fully drawn.
    private PageLayoutResult LayoutPage(LayoutContext context)
    {
        var stack = new Stack<LayoutNode>();
        stack.Push(_root);
        var page = new Page();

        while (stack.Count > 0)
        {
            var layout = stack.Pop();

            // Skip nodes that we've already laid out completely
            if (_layoutComplete.Contains(layout.Layout))
            {
                continue;
            }

            var childContext = new LayoutContext(context.Available);
            var layoutResult = layout.Layout.Layout(childContext);
            page.AddDrawables(layoutResult.Drawables);
            context.CommitChildContext(childContext);

            if (layoutResult.Status == LayoutStatus.NeedsNewPage)
            {
                return new PageLayoutResult(page, LayoutStatus.NeedsNewPage);
            }

            // Remember that this layout is complete
            // Todo: This might not be needed if the root element is the only one on the stack.
            _layoutComplete.Add(layout.Layout);
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

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

    /// Depth-first search that skips nodes previously seen that did not satisfy the predicate.
    /// Returns the first match (preorder) or default if none.
    private IPage LayoutPage(LayoutContext context)
    {
        var stack = new Stack<LayoutNode>();
        stack.Push(_root);
        var page = new Page();

        while (stack.Count > 0)
        {
            var layout = stack.Pop();

            // Skip nodes that we've already tested and that failed before
            if (_layoutComplete.Contains(layout.Layout))
            {
                continue;
            }

            var layoutResult = layout.Layout.Layout(context);
            page.AddDrawables(layoutResult.Drawables);
            if (layoutResult.State == LayoutState.IsFullyDrawn)
            {
                break;
            }

            // Remember that this node failed, so we can skip it next time
            _layoutComplete.Add(layout.Layout);

            // Push children in reverse so the first child is visited first
            // (typical DFS preorder behavior)
            foreach (var child in Enumerable.Reverse(layout.Children))
            {
                stack.Push(child);
            }
        }

        return page;
    }

    public List<IPage> ToPages(SKRect drawableArea)
    {
        var pages = new List<IPage>();

        DocumentLayoutStatus documentStatus = DocumentLayoutStatus.NeedsAdditionalPage;
        while (documentStatus == DocumentLayoutStatus.NeedsAdditionalPage)
        {
            pages.Add(LayoutPage(new LayoutContext(drawableArea)));
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

    private readonly record struct DocumentLayoutStatus
    {
        public static readonly DocumentLayoutStatus AllPagesComplete = new (1);

        public static readonly DocumentLayoutStatus NeedsAdditionalPage = new (2);

        private int Value { get; }

        private DocumentLayoutStatus(int value)
        {
            Value = value;
        }
    }
}

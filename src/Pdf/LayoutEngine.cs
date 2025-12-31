namespace InvoiceKit.Pdf;

using SkiaSharp;

/// <summary>
/// Implements the layout algorithm for a document. That is, it takes a root <see cref="ILayout"/> and calls
/// <see cref="ILayout.Layout"/> on it until fully drawn.
/// </summary>
internal class LayoutEngine(IViewBuilder root) : IDisposable
{
    private readonly ILayout _root = root.ToLayout();

    private readonly List<Page> _pages = [];

    private readonly HashSet<ILayout> _laidOut = [];

    public IReadOnlyCollection<Page> Pages => _pages.AsReadOnly();

    /// <summary>
    /// Lays out a page, starts with the root layout, and traverses its children.
    /// </summary>
    /// <param name="rootContext">The page's layout context to track drawn space on.</param>
    /// <returns>
    /// A <see cref="PageLayoutResult"/> which contains the page, its <see cref="IDrawable"/>s, and status.
    /// </returns>
    private PageLayoutResult LayoutPage(RootLayoutContext rootContext)
    {
        var stack = new Stack<ChildLayout>();
        stack.Push(ChildLayout.CreateRoot(_root, rootContext));
        var page = new Page();
        var repeatingLayouts = new HashSet<ILayout>();

        while (stack.Count > 0)
        {
            var layout = stack.Peek();
            if (_laidOut.Contains(layout.Layout))
            {
                continue;
            }

            if (layout.Context.Repeating && repeatingLayouts.Contains(layout.Layout))
            {
                continue;
            }

            var layoutResult = layout.Layout.Layout(layout.Context);
            page.AddDrawables(layoutResult.Drawables);

            if (layoutResult.Status == LayoutStatus.NeedsNewPage)
            {
                return new PageLayoutResult(page, LayoutStatus.NeedsNewPage);
            }

            if (layoutResult.Status == LayoutStatus.Deferred)
            {
                var childrenNeedingLayout = new List<ChildLayout>();
                foreach (var child in layoutResult.Children)
                {
                    if (_laidOut.Contains(child.Layout))
                    {
                        continue;
                    }

                    if (child.Context.Repeating && repeatingLayouts.Contains(child.Layout))
                    {
                        continue;
                    }

                    childrenNeedingLayout.Add(child);
                    stack.Push(child);
                }

                if (childrenNeedingLayout.Count > 0)
                {
                    continue;
                }
            }

            layout.Context.CommitChildContext();
            stack.Pop();
            if (layout.Context.Repeating)
            {
                repeatingLayouts.Add(layout.Layout);
                continue;
            }

            _laidOut.Add(layout.Layout);
        }

        return new PageLayoutResult(page, LayoutStatus.IsFullyDrawn);
    }

    public void LayoutDocument(SKRect drawableArea)
    {
        var status = LayoutStatus.NeedsNewPage;
        while (status == LayoutStatus.NeedsNewPage)
        {
            var result = LayoutPage(new RootLayoutContext(drawableArea));
            _pages.Add(result.Page);
            status = result.Status;
        }
    }

    public void Dispose()
    {
        foreach (var page in _pages)
        {
            page.Dispose();
        }
    }

    private record PageLayoutResult(Page Page, LayoutStatus Status);
}

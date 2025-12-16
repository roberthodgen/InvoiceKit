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
    private PageLayoutResult LayoutPage(LayoutContext rootContext)
    {
        var stack = new Stack<ChildLayout>();
        stack.Push(new ChildLayout(_root, rootContext));
        var page = new Page();

        while (stack.Count > 0)
        {
            var layout = stack.Peek();
            if (_laidOut.Contains(layout.Layout))
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
                foreach (var child in layoutResult.Children)
                {
                    stack.Push(child);
                }
            }

            if (layoutResult.Status == LayoutStatus.IsFullyDrawn)
            {
                _laidOut.Add(layout.Layout);
            }

            stack.Pop();
        }

        return new PageLayoutResult(page, LayoutStatus.IsFullyDrawn);
    }

    public void LayoutDocument(SKRect drawableArea)
    {
        var status = LayoutStatus.NeedsNewPage;
        while (status == LayoutStatus.NeedsNewPage)
        {
            var result = LayoutPage(new LayoutContext(drawableArea));
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

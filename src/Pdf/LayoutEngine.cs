namespace InvoiceKit.Pdf;

using SkiaSharp;

internal class LayoutEngine(IViewBuilder root) : IDisposable
{
    private readonly ILayout _root = root.ToLayout();

    private readonly HashSet<ILayout> _completed = [];

    private readonly List<Page> _pages = [];

    public IReadOnlyCollection<Page> Pages => _pages.AsReadOnly();

    /// <summary>
    /// Lays out a page from traversing the root layout and recursively calling layout on it and its children.
    /// </summary>
    /// <param name="context">The page's layout context to track drawn space on.</param>
    /// <returns>
    /// A <see cref="PageLayoutResult"/> which contains the page, its <see cref="IDrawable"/>s, and status.
    /// </returns>
    private PageLayoutResult LayoutPage(LayoutContext context)
    {
        var page = new Page();
        var needsLayout = new Stack<ILayout>();
        needsLayout.Push(_root);
        var completed = new Stack<ILayout>();

        while (needsLayout.TryPop(out var layout))
        {
            if (_completed.Contains(layout))
            {
                continue;
            }

            var childContext = context.GetChildContext();
            var layoutResult = layout.Layout(childContext);
            page.AddDrawables(layoutResult.Drawables);
            context.CommitChildContext(childContext);

            if (layoutResult.Status == LayoutStatus.NeedsNewPage)
            {
                return new PageLayoutResult(page, LayoutStatus.NeedsNewPage);
            }

            // Only complete the layout if it's fully drawn
            completed.Push(layout);
            foreach (var child in layout.Children)
            {
                needsLayout.Push(child);
            }
        }

        while (completed.TryPop(out var layout))
        {
            _completed.Add(layout);
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

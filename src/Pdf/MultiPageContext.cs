namespace InvoiceKit.Pdf;

using Layouts;

public class MultiPageContext : IDisposable
{
    private readonly Func<PageLayout> _getNextPage;

    private int _currentPageIndex;

    private readonly Stack<DrawState> _stateStack = new();

    private DrawState Current => _stateStack.Peek();

    private List<PageLayout> Pages { get; } = [];

    public bool Debug { get; }

    public MultiPageContext(Func<PageLayout> getNextPage, bool debug)
    {
        _getNextPage = getNextPage;
        Pages.Add(getNextPage());
        Debug = debug;
    }

    /// <summary>
    /// Returns the current page for drawing.
    /// </summary>
    /// <remarks>
    /// The return page may be partially drawn into or an empty page. When the current page is fully drawn, this class
    /// will create a new page and advance the marker.
    /// </remarks>
    public PageLayout GetCurrentPage()
    {
        var currentPage = Pages[_currentPageIndex];
        if (currentPage.IsFullyDrawn)
        {
            var nextPage = NextPage();
            _currentPageIndex++;
            return nextPage;
        }

        return currentPage;
    }

    /// <summary>
    /// Gets the next page for drawing overflow for the current <see cref="IDrawable"/>.
    /// </summary>
    /// <remarks>
    /// Does not advance the current page marker as other blocks (think columns or Z stacks) may still need to render
    /// into the current page.
    /// </remarks>
    private PageLayout NextPage()
    {
        if (_currentPageIndex < Pages.Count - 1)
        {
            Pages.Add(_getNextPage());
        }

        return Pages[_currentPageIndex + 1];
    }

    public void BeginBlock()
    {
        var newState = new DrawState(Current); // Clone or derive from current
        _stateStack.Push(newState);
    }

    public DrawState EndBlock()
    {
        var finalized = _stateStack.Pop();
        // Optionally merge results back to the parent
        if (_stateStack.Count > 0)
        {
            _stateStack.Peek().AdjustAfterChild(finalized);
        }

        return finalized;
    }

    public void PageBreak()
    {
        Current.StartNewPage();
    }

    public void Dispose()
    {
        foreach (var page in Pages)
        {
            page.Dispose();
        }
    }

}

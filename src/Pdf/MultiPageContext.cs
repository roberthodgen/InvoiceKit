namespace InvoiceKit.Pdf;

using Containers;
using ShimSkiaSharp;

public class MultiPageContext : IDisposable
{
    private readonly Func<PageLayout> _getNextPage;

    private int _currentPageIndex;

    public List<PageLayout> Pages { get; } = [];

    public MultiPageContext(Func<PageLayout> getNextPage)
    {
        _getNextPage = getNextPage;
        Pages.Add(getNextPage());
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
        if (!currentPage.IsFullyDrawn) return currentPage;

        var nextPage = NextPage();
        _currentPageIndex++;
        return nextPage;
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
        if (_currentPageIndex + 1 >= Pages.Count)
        {
            Pages.Add(_getNextPage());
        }

        return Pages[_currentPageIndex + 1];
    }

    public void Dispose()
    {
        foreach (var page in Pages)
        {
            page.Dispose();
        }
    }

}

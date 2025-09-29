namespace InvoiceKit.Pdf.Layout;

/// <summary>
/// Used to render a layout across a single page.
/// </summary>
internal class Page : IPage, IDisposable
{
    /// <summary>
    /// Enumerable of drawables that fit onto the page.
    /// </summary>
    public List<IDrawable> Drawables { get; } = [];

    public Page()
    {
    }

    public void AddDrawables(IEnumerable<IDrawable> drawables)
    {
        Drawables.AddRange(drawables);
    }

    public void Dispose()
    {
    }
}

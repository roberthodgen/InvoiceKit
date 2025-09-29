namespace InvoiceKit.Pdf.Layout;

/// <summary>
/// Used to render a layout across a single page.
/// </summary>
internal class Page : IDisposable
{
    private readonly List<IDrawable> _drawables = [];

    /// <summary>
    /// Enumerable of drawables that fit onto the page.
    /// </summary>
    public IReadOnlyCollection<IDrawable> Drawables => _drawables.AsReadOnly();

    public Page()
    {
    }

    internal void AddDrawables(IEnumerable<IDrawable> drawables)
    {
        _drawables.AddRange(drawables);
    }

    public void Dispose()
    {
        foreach (var drawable in _drawables)
        {
            drawable.Dispose();
        }
    }
}

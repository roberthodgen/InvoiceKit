namespace InvoiceKit.Pdf.Layout;

/// <summary>
/// Represents a single page in a PDF document and stores all drawables placed on it during the layout phase.
/// </summary>
internal class Page : IDisposable
{
    private readonly List<IDrawable> _drawables = [];

    /// <summary>
    /// Enumerable of drawables that fit onto the page.
    /// </summary>
    public IReadOnlyCollection<IDrawable> Drawables => _drawables.AsReadOnly();

    public void AddDrawables(IEnumerable<IDrawable> drawables)
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

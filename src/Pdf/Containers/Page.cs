namespace InvoiceKit.Pdf.Containers;

using SkiaSharp;

/// <summary>
/// Used to render a layout across a single page.
/// </summary>
public class Page : IPage, IDisposable
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

namespace InvoiceKit.Pdf;

using Layouts;
using SkiaSharp;

public interface IDrawable : IDisposable
{
    /// <summary>
    /// Attempts to fit the content within the available size. Returns the size necessary.
    /// </summary>
    /// <param name="available">The available rectangle to render content within.</param>
    /// <returns>The size necessary to render the content.</returns>
    /// <remarks>
    /// The returned size may be larger than the available size and indicates a new page or resize is necessary by the
    /// calling layout component.
    /// </remarks>
    SKSize Measure(SKSize available);

    /// <summary>
    /// Draws the content within the specified rectangle.
    /// </summary>
    /// <param name="page">The canvas into which to draw.</param>
    /// <param name="rect">The rectangle into which the content should be drawn.</param>
    void Draw(PageLayout page, SKRect rect, Func<PageLayout> getNextPage);
}

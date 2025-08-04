namespace InvoiceKit.Pdf;

using SkiaSharp;

public interface IDrawable : IDisposable
{
    /// <summary>
    /// Attempts to fit the content within the available size. Returns the sizes of all children.
    /// </summary>
    /// <param name="available">The available rectangle to render content within.</param>
    /// <returns>The size necessary to render the content.</returns>
    /// <remarks>
    /// The sum of all returned sizes may be larger than the available area and indicates a new page or resize is
    /// necessary.
    /// </remarks>
    SKSize Measure(SKSize available);

    /// <summary>
    /// Draws the content within the specified rectangle.
    /// </summary>
    /// <param name="context">A multi-page drawing context.</param>
    /// <param name="rect">The rectangle into which the content should be drawn.</param>
    void Draw(MultiPageContext context, SKRect rect);
}

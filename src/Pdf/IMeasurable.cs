namespace InvoiceKit.Pdf;

using SkiaSharp;

public interface IMeasurable
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
    SKSize Measure(SKRect available);
}

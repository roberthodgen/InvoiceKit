namespace InvoiceKit.Pdf;

using SkiaSharp;

public interface IRenderable
{
    /// <summary>
    /// Attempts to fit the content within the available size. Returns the size necessary.
    /// </summary>
    /// <param name="available">The available rectangle to render content within.</param>
    /// <param name="measured">The minimum rectangle necessary to render the content within.</param>
    /// <returns>The rectangle is necessary to render the content within.</returns>
    bool TryMeasure(SKSize available, out SKSize measured);

    void Draw(SKCanvas canvas, SKRect rect);
}

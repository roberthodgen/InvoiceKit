namespace InvoiceKit.Pdf;

using Containers;
using SkiaSharp;

public interface IDrawable : IMeasurable, IDisposable
{
    /// <summary>
    /// Used after the content has been laid out to store the final size and location of the drawable on a page.
    /// </summary>
    SKRect SizeAndLocation { get; }

    /// <summary>
    /// Used to activate debug box outlines for the drawable.
    /// </summary>
    bool Debug { get; }

    /// <summary>
    /// Draws the content within the specified rectangle.
    /// </summary>
    void Draw(SKCanvas canvas, PageLayout page);
}

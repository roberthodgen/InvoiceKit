namespace InvoiceKit.Pdf;

using Containers;

public interface IDrawable : IMeasurable, IDisposable
{
    /// <summary>
    /// Draws the content within the specified rectangle.
    /// </summary>
    void Draw(PageLayout page);
}

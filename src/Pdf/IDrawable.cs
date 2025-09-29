namespace InvoiceKit.Pdf;

/// <summary>
/// A piece of content that can be drawn to a page. Its location, size, and content were determined during the layout
/// phase. This object contains all data necessary to draw.
/// </summary>
public interface IDrawable : IDisposable
{
    /// <summary>
    /// Draws the content to a canvas within the <see cref="IDrawableContext"/>.
    /// </summary>
    void Draw(IDrawableContext context);
}

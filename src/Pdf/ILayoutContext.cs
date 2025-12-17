namespace InvoiceKit.Pdf;

using SkiaSharp;

/// <summary>
/// Represents the available space for a layout.
/// </summary>
/// <remarks>
/// Uses a two-phase commit model where children contexts should be created then committed back to their parents.
/// </remarks>
public interface ILayoutContext
{
    /// <summary>
    /// Gets the total space allocated to this layout.
    /// </summary>
    public SKRect Allocated { get; }

    /// <summary>
    /// Gets the available space left for this layout.
    /// </summary>
    public SKRect Available { get; }

    /// <summary>
    /// Determines if this layout can accomodate the size of a rect.
    /// </summary>
    /// <param name="size">The size to check.</param>
    /// <returns>True if the rect can be accommodated, false otherwise.</returns>
    /// <remarks>
    /// This method will automatically add the height of the rect to the allocated space when it returns true.
    /// </remarks>
    public bool TryAllocate(SKSize size);

    /// <summary>
    /// Determines if the measurable can fit onto the page and returns a rect.
    /// </summary>
    /// <param name="size">SKSize of the element being allocated.</param>
    /// <param name="rect">Outputs the allocated rect for the drawing.</param>
    public bool TryAllocate(SKSize size, out SKRect rect);

    /// <summary>
    /// Commits the allocated space of a child layout context.
    /// </summary>
    public void CommitChildContext();

    /// <summary>
    /// Creates a new vertical child context from the remaining available space that intersects with the given rect.
    /// </summary>
    /// <param name="intersectingRect">A rect to limit the child context to.</param>
    ILayoutContext GetVerticalChildContext(SKRect intersectingRect);

    /// <summary>
    /// Creates a new horizontal child context from the remaining available space that intersects with the given rect.
    /// </summary>
    /// <param name="intersectingRect">A rect to limit the child context to.</param>
    ILayoutContext GetHorizontalChildContext(SKRect intersectingRect);
}

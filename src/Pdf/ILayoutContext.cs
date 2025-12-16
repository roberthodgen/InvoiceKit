namespace InvoiceKit.Pdf;

using SkiaSharp;

public interface ILayoutContext
{
    public SKRect Allocated { get; }

    public SKRect Available { get; }

    public bool TryAllocate(SKSize size);

    public bool TryAllocate(SKSize size, out SKRect rect);

    public void CommitChildContext(ILayoutContext child);

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

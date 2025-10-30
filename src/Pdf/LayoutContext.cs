namespace InvoiceKit.Pdf;

using SkiaSharp;

/// <summary>
/// Represents the available space for a layout.
/// </summary>
/// <remarks>
/// Uses a two-phase commit model where children contexts should be created then committed back to their parents.
/// </remarks>
public sealed class LayoutContext
{
    private readonly List<float> _allocatedTop = [];

    private readonly List<float> _allocatedBottom = [];

    private readonly SKRect _originalSpace;

    /// <summary>
    /// Gets the total space allocated to this layout.
    /// </summary>
    public SKRect Allocated => new (
        _originalSpace.Left,
        _originalSpace.Top,
        _originalSpace.Right,
        _originalSpace.Top + _allocatedTop.Sum());

    /// <summary>
    /// Gets the available space left for this layout.
    /// </summary>
    public SKRect Available => new (
        _originalSpace.Left,
        Allocated.Bottom,
        _originalSpace.Right,
        _originalSpace.Bottom - _allocatedBottom.Sum());

    internal LayoutContext(SKRect available)
    {
        _originalSpace = available;
    }

    /// <summary>
    /// Determines if this layout can accomodate a rect of a certain size.
    /// </summary>
    /// <param name="rect">The rect to check.</param>
    /// <returns>True if the rect can be accommodated, false otherwise.</returns>
    /// <remarks>
    /// This method will automatically add the height of the rect to the allocated space when it returns true.
    /// </remarks>
    public bool TryAllocateRect(SKRect rect)
    {
        if (rect.Height > Available.Height || rect.Width > Available.Width)
        {
            return false;
        }

        _allocatedTop.Add(rect.Height);
        return true;
    }

    /// <summary>
    /// Commits the allocated space of a child layout context.
    /// </summary>
    /// <param name="child">Another layout to allocate on this layout.</param>
    public void CommitChildContext(LayoutContext child)
    {
        _allocatedTop.Add(child.Allocated.Height);
    }

    /// <summary>
    /// Commits the allocated space of a child layout context.
    /// </summary>
    /// <param name="child">Another layout to allocate on this layout.</param>
    /// <remarks>Should only be used when allocating the footer.</remarks>
    public void CommitContextFromBottom(LayoutContext child)
    {
        _allocatedBottom.Add(child.Allocated.Height);
    }

    /// <summary>
    /// Creates a new child context from the remaining available space.
    /// </summary>
    public LayoutContext GetChildContext()
    {
        return new LayoutContext(Available);
    }

    /// <summary>
    /// Creates a new child context from the remaining available space that intersects with the given rect.
    /// </summary>
    /// <param name="intersectingRect">A rect to limit the child context to.</param>
    public LayoutContext GetChildContextFromIntersect(SKRect intersectingRect)
    {
        return new LayoutContext(SKRect.Intersect(Available, intersectingRect));
    }
}

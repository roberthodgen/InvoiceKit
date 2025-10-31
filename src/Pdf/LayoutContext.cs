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
    private readonly List<float> _allocated = [];

    private readonly SKRect _originalSpace;

    /// <summary>
    /// Gets the total space allocated to this layout.
    /// </summary>
    public SKRect Allocated => new (
        _originalSpace.Left,
        _originalSpace.Top,
        _originalSpace.Right,
        _originalSpace.Top + _allocated.Sum());

    /// <summary>
    /// Gets the available space left for this layout.
    /// </summary>
    public SKRect Available => new (
        _originalSpace.Left,
        Allocated.Bottom,
        _originalSpace.Right,
        _originalSpace.Bottom);

    internal LayoutContext(SKRect available)
    {
        _originalSpace = available;
    }

    /// <summary>
    /// Determines if this layout can accomodate the size of a rect.
    /// </summary>
    /// <param name="size">The size to check.</param>
    /// <returns>True if the rect can be accommodated, false otherwise.</returns>
    /// <remarks>
    /// This method will automatically add the height of the rect to the allocated space when it returns true.
    /// </remarks>
    public bool TryAllocate(SKSize size)
    {
        if (size.Height > Available.Height || size.Width > Available.Width)
        {
            return false;
        }

        _allocated.Add(size.Height);
        return true;
    }

    /// <summary>
    /// Determines if the measurable can fit onto the page and returns a rect.
    /// </summary>
    /// <param name="measurable">Takes in a IMeasurable.</param>
    /// <param name="rect">Outputs a SKRect for the drawable.</param>
    public bool TryAllocate(IMeasurable measurable,  out SKRect rect)
    {
        var size = measurable.Measure(Available.Size);
        rect = new SKRect(Available.Left, Available.Top, Available.Left + size.Width, Available.Top + size.Height);
        return TryAllocate(size);
    }

    /// <summary>
    /// Commits the allocated space of a child layout context.
    /// </summary>
    /// <param name="child">Another layout to allocate on this layout.</param>
    public void CommitChildContext(LayoutContext child)
    {
        _allocated.Add(child.Allocated.Height);
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
    public LayoutContext GetChildContext(SKRect intersectingRect)
    {
        return new LayoutContext(SKRect.Intersect(Available, intersectingRect));
    }
}

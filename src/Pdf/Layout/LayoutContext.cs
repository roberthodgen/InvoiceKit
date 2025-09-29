namespace InvoiceKit.Pdf.Layout;

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
    /// Gets the total vertical space allocated to this layout.
    /// </summary>
    public float Allocated => _allocated.Sum();

    /// <summary>
    /// Gets the available space left for this layout.
    /// </summary>
    public SKRect Available
    {
        get
        {
            var rect = new SKRect(
                _originalSpace.Left,
                _originalSpace.Top + Allocated,
                _originalSpace.Right,
                _originalSpace.Bottom);
            return rect;
        }
    }

    internal LayoutContext(SKRect available)
    {
        _originalSpace = available;
    }

    /// <summary>
    /// Used to determine if this layout can accomodate a rect of a certain size.
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

        _allocated.Add(rect.Height);
        return true;
    }

    /// <summary>
    /// Commits the allocated space of a child layout context.
    /// </summary>
    /// <param name="child">Another layout to allocate on this layout.</param>
    public void CommitChildContext(LayoutContext child)
    {
        _allocated.Add(child.Allocated);
    }
}

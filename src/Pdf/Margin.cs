namespace InvoiceKit.Pdf;

using SkiaSharp;

public readonly record struct Margin
{
    public float Left { get; init; }

    public float Top { get; init; }

    public float Right { get; init; }

    public float Bottom { get; init; }

    public Margin(float left, float top, float right, float bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public Margin(float margin)
    {
        Left = margin;
        Top = margin;
        Right = margin;
        Bottom = margin;
    }

    /// <summary>
    /// Gets the starting location for the margin.
    /// </summary>
    /// <param name="content">SKRect of the drawable content.</param>
    /// <returns>SKRect for the Margin</returns>
    /// <remarks>Only used for drawing debug rects.</remarks>
    public SKRect GetDrawableRect(SKRect content)
    {
        return new SKRect(
            content.Left - Left,
            content.Top - Top,
            content.Right + Right,
            content.Bottom + Bottom);
    }

    /// <summary>
    /// Adjusts available space by removing the margin size from each side.
    /// </summary>
    /// <param name="available">The available space in the context.</param>
    public SKRect GetContentRect(SKRect available)
    {
        return new SKRect(
            available.Left + Left,
            available.Top + Top,
            available.Right - Right,
            available.Bottom - Bottom);
    }
}

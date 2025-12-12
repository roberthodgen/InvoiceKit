namespace InvoiceKit.Pdf;

using SkiaSharp;

public readonly record struct Padding
{
    public float Left { get; init; }

    public float Top { get; init; }

    public float Right { get; init; }

    public float Bottom { get; init; }

    public Padding(float left, float top, float right, float bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public Padding(float padding)
    {
        Left = padding;
        Top = padding;
        Right = padding;
        Bottom = padding;
    }

    /// <summary>
    /// Gets the starting location for the padding.
    /// </summary>
    /// <param name="content">SKRect of the drawable content.</param>
    /// <returns>SKRect for the padding</returns>
    public SKRect GetDrawableRect(SKRect content)
    {
        return new SKRect(
            content.Left - Left,
            content.Top - Top,
            content.Right + Right,
            content.Bottom + Bottom);
    }

    /// <summary>
    /// Adjusts available space by removing the padding size from each side.
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

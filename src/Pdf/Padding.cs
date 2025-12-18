namespace InvoiceKit.Pdf;

using Geometry;
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
    public PaddingRect GetPaddingRect(ContentRect content)
    {
        return new PaddingRect(
            content.ToRect().Left - Left,
            content.ToRect().Top - Top,
            content.ToRect().Right + Right,
            content.ToRect().Bottom + Bottom);
    }

    /// <summary>
    /// Adjusts available space by removing the padding size from each side.
    /// </summary>
    /// <param name="available">The available space in the context.</param>
    public ContentRect GetContentRect(PaddingRect available)
    {
        return new ContentRect(
            available.ToRect().Left + Left,
            available.ToRect().Top + Top,
            available.ToRect().Right - Right,
            available.ToRect().Bottom - Bottom);
    }

    public SKSize ToSize() => new (Left + Right, Top + Bottom);

    public PaddingSize ToSize(ContentSize content)
    {
        return new PaddingSize(content, ToSize());
    }
}

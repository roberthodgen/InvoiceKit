namespace InvoiceKit.Pdf;

using Geometry;
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

    public SKSize ToSize() => new (Left + Right, Top + Bottom);

    /// <summary>
    /// Adjusts available space by removing the margin size from each side.
    /// </summary>
    /// <param name="outer">The available space in the context.</param>
    public BorderRect GetBorderRect(OuterRect outer)
    {
        return new (
            outer.ToRect().Left + Left,
            outer.ToRect().Top + Top,
            outer.ToRect().Right - Right,
            outer.ToRect().Bottom - Bottom);
    }
}

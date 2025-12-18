namespace InvoiceKit.Pdf;

using Geometry;
using SkiaSharp;

public readonly record struct Margin
{
    public float Left { get; init; }

    public float Top { get; init; }

    public float Right { get; init; }

    public float Bottom { get; init; }

    public bool HasValue => Left != 0.0f || Top != 0.0f || Right != 0.0f || Bottom != 0.0f;

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

    public OuterSize ToSize(BorderSize borderSize)
    {
        var size = new SKSize(Top + Bottom, Left + Right);
        return new OuterSize(borderSize, size);
    }

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

    /// <summary>
    /// Adjusts a BorderRect back into an OuterRect
    /// </summary>
    /// <param name="border">The BorderRect within the OuterRect.</param>
    public OuterRect GetMarginRect(BorderRect border)
    {
        return new OuterRect(
            border.ToRect().Left - Left,
            border.ToRect().Top - Top,
            border.ToRect().Right + Right,
            border.ToRect().Bottom + Bottom);
    }
}

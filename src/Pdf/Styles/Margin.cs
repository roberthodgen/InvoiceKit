namespace InvoiceKit.Pdf.Styles;

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

    public SKRect GetDrawableRect(SKRect rect)
    {
        return new SKRect(
            rect.Left - Left,
            rect.Top - Top,
            rect.Right + Right,
            rect.Bottom + Bottom);
    }

    public SKRect GetContentRect(SKRect rect)
    {
        return new SKRect(
            rect.Left + Left,
            rect.Top + Top,
            rect.Right - Right,
            rect.Bottom - Bottom);
    }
}

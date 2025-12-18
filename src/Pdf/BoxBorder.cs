namespace InvoiceKit.Pdf;

using Geometry;
using SkiaSharp;

public readonly record struct BoxBorder(BorderStyle Top, BorderStyle Bottom, BorderStyle Left, BorderStyle Right)
{
    public static BoxBorder Create(BorderStyle style) => new ()
    {
        Top = style,
        Bottom = style,
        Left = style,
        Right = style,
    };

    public SKSize ToSize() => new (Left.Width + Right.Width, Top.Width + Bottom.Width);

    public BorderSize ToSize(PaddingSize paddingSize)
    {
        return new BorderSize(paddingSize, ToSize());
    }

    /// <summary>
    /// Adjusts available space by removing the border size from each side.
    /// </summary>
    /// <param name="available">The available space in the context.</param>
    public PaddingRect GetPaddingRect(BorderRect available)
    {
        return new PaddingRect(
            available.ToRect().Left + Left.Width,
            available.ToRect().Top + Top.Width,
            available.ToRect().Right - Right.Width,
            available.ToRect().Bottom - Bottom.Width);
    }

    /// <summary>
    /// Gets the starting location for the border.
    /// </summary>
    /// <param name="content">SKRect of the drawable content.</param>
    /// <returns>SKRect for the border</returns>
    public BorderRect GetBorderRect(PaddingRect content)
    {
        return new BorderRect(
            content.ToRect().Left - Left.Width,
            content.ToRect().Top - Top.Width,
            content.ToRect().Right + Right.Width,
            content.ToRect().Bottom + Bottom.Width);
    }
}

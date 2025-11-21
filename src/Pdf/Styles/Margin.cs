namespace InvoiceKit.Pdf.Styles;

using SkiaSharp;

public readonly record struct Margin(float Left, float Top, float Right, float Bottom)
{
    public SKRect GetRect(SKRect rect)
    {
        return new SKRect(
            rect.Left - Left,
            rect.Top - Top,
            rect.Right - Right,
            rect.Bottom - Bottom);
    }
}

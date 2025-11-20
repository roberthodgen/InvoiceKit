namespace InvoiceKit.Pdf;

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

    public (SKPoint, SKPoint) GetTopPoints(SKRect rect)
    {
        return (
            new SKPoint(rect.Left + (Top.Width / 2), rect.Top),
            new SKPoint(rect.Right - (Top.Width / 2), rect.Top));
    }

    public (SKPoint, SKPoint) GetBottomPoints(SKRect rect)
    {
        return (
            new SKPoint(rect.Left + (Bottom.Width / 2), rect.Bottom),
            new SKPoint(rect.Right - (Bottom.Width / 2), rect.Bottom));
    }

    public (SKPoint, SKPoint) GetLeftPoints(SKRect rect)
    {
        return (
            new SKPoint(rect.Left + Left.Width, rect.Top + (Left.Width / 2)),
            new SKPoint(rect.Left + Left.Width, rect.Bottom - (Left.Width / 2)));
    }

    public (SKPoint, SKPoint) GetRightPoints(SKRect rect)
    {
        return (
            new SKPoint(rect.Right - Right.Width, rect.Top + (Right.Width / 2)),
            new SKPoint(rect.Right - Right.Width, rect.Bottom - (Right.Width / 2)));
    }
}

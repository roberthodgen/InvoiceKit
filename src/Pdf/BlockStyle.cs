namespace InvoiceKit.Pdf;

using SkiaSharp;

public readonly record struct BlockStyle()
{
    public SKColor ForegroundColor { get; init; } = SKColors.Black;

    public SKColor? BackgroundColor { get; init; } = null;

    public TextStyle Text { get; init; } = new ();

    public SKPaint ToPaint()
    {
        var paint = new SKPaint
        {
            Color = ForegroundColor,
            IsAntialias = true,
        };

        return paint;
    }

    public SKFont ToFont()
    {
        var font = new SKFont
        {
            Size = Text.FontSize,
        };

        var truePath = $"Fonts/{Text.FontPath}.ttf";
        if (!string.IsNullOrWhiteSpace(Text.FontPath) && File.Exists(truePath))
        {
            font.Typeface = SKTypeface.FromFile(truePath);
        }

        return font;
    }
}

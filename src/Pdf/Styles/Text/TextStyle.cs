namespace InvoiceKit.Pdf.Styles.Text;

using SkiaSharp;

public sealed record TextStyle
{
    public string? FontPath { get; init; }

    public float LineHeight { get; init; } = 1.25f;

    public float FontSize { get; init; } = 16f;

    public SKColor Color { get; init; } = SKColors.Black;

    /// <summary>
    /// Additional vertical space between paragraphs (not wrapped lines).
    /// </summary>
    public ParagraphSpacing ParagraphSpacing { get; init; } = new (0f, 8f);

    public float Height => FontSize * LineHeight;

    public SKPaint ToPaint()
    {
        var paint = new SKPaint
        {
            Color = Color,
            IsAntialias = true
        };

        return paint;
    }

    public SKFont ToFont()
    {
        var font = new SKFont
        {
            Size = FontSize,
        };

        var truePath = $"Fonts/{FontPath}.ttf";
        if (!string.IsNullOrWhiteSpace(FontPath) && File.Exists(truePath))
        {
            font.Typeface = SKTypeface.FromFile(truePath);
        }

        return font;
    }
}

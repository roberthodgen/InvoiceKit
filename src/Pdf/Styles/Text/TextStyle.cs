namespace InvoiceKit.Pdf.Styles.Text;

using SkiaSharp;

public readonly record struct TextStyle()
{
    public const float DefaultFontSize = 12f;

    public string? FontPath { get; init; } = null;

    public float LineHeight { get; init; } = 1.1f;

    public float FontSize { get; init; } = DefaultFontSize;

    public SKColor Color { get; init; } = SKColors.Black;

    /// <summary>
    /// Additional vertical space between paragraphs (not wrapped lines).
    /// </summary>
    public ParagraphSpacing ParagraphSpacing { get; init; } = new ();

    /// <summary>
    /// The product of the <see cref="FontSize"/> and the <see cref="ParagraphSpacing.Before"/>.
    /// </summary>
    public float ParagraphSpacingBefore => (FontSize * ParagraphSpacing.Before) - FontSize;

    /// <summary>
    /// The product of the <see cref="FontSize"/> and the <see cref="ParagraphSpacing.After"/>.
    /// </summary>
    public float ParagraphSpacingAfter => (FontSize * ParagraphSpacing.After) - FontSize;

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

namespace InvoiceKit.Pdf.Styles;

using SkiaSharp;

public readonly record struct BlockStyle()
{
    /// <summary>
    /// Sets the foreground color for text and horizontal rules.
    /// </summary>
    public SKColor ForegroundColor { get; init; } = SKColors.Black;

    /// <summary>
    /// Sets the background fill color for blocks.
    /// </summary>
    public SKColor BackgroundColor { get; init; } = SKColor.Empty;

    /// <summary>
    /// Should be specified as <c>Font Name/Style</c>, e.g.: <c>Open Sans/SemiBold</c>.
    /// </summary>
    public string? FontPath { get; init; } = "Open Sans/Regular";

    /// <summary>
    /// Text line height.
    /// </summary>
    public float LineHeight { get; init; } = 1.1f;

    /// <summary>
    /// Text font size.
    /// </summary>
    public float FontSize { get; init; } = 12f;

    /// <summary>
    /// The relative amount of spacing before paragraphs.
    /// </summary>
    public float Before { get; init; } = 1.25f;

    /// <summary>
    /// The relative amount of spacing after paragraphs.
    /// </summary>
    public float After { get; init; } = 1.25f;

    /// <summary>
    /// The product of the <see cref="FontSize"/> and <see cref="Before"/>.
    /// </summary>
    public float ParagraphSpacingBefore => (FontSize * Before) - FontSize;

    /// <summary>
    /// The product of the <see cref="FontSize"/> and <see cref="After"/>.
    /// </summary>
    public float ParagraphSpacingAfter => (FontSize * After) - FontSize;

    public BoxBorder Border { get; init; } = new ();

    public Margin Margin { get; init; } = new ();

    public Padding Padding { get; init; } = new ();

    public SKPaint ForegroundToPaint()
    {
        var paint = new SKPaint
        {
            Color = ForegroundColor,
            IsAntialias = true,
        };

        return paint;
    }

    public SKPaint BackgroundToPaint()
    {
        var paint = new SKPaint
        {
            Color = BackgroundColor,
            IsAntialias = true,
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

    public SKRect GetBorderRect(SKRect rect)
    {
        return Border.GetRect(rect);
    }

    public SKRect GetContentRect(SKRect rect)
    {
        return Padding.GetRect(Border.GetRect(Margin.GetRect(rect)));
    }
}

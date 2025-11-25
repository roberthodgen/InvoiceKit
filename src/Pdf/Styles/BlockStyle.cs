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

    public BoxBorder Border { get; init; } = new ();

    public Margin Margin { get; init; } = new ();

    public Padding Padding { get; init; } = new ();

    public SKPaint ForegroundToPaint()
    {
        return new SKPaint
        {
            Color = ForegroundColor,
            IsAntialias = true,
        };
    }

    public SKPaint BackgroundToPaint()
    {
        return new SKPaint
        {
            Color = BackgroundColor,
            IsAntialias = true,
        };
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

    /// <summary>
    /// Returns a new SKSize for the available size after styling adjustments.
    /// </summary>
    public SKSize GetStyleSize()
    {
        var width = Padding.Left + Padding.Right + Margin.Left + Margin.Right + Border.Left.Width + Border.Right.Width;
        var height = Padding.Top + Padding.Bottom + Margin.Top + Margin.Bottom + Border.Top.Width + Border.Bottom.Width;
        return new SKSize(width, height);
    }

    /// <summary>
    /// Creates a drawable area after removing styling sizes.
    /// </summary>
    /// <param name="available">Available drawing size.</param>
    public SKRect GetContentRect(SKRect available)
    {
        return Padding.GetContentRect(Border.GetContentRect(Margin.GetContentRect(available)));
    }

    /// <summary>
    /// Creates a drawable area for the background by expanding from the contentRect.
    /// </summary>
    /// <param name="contentRect">The SKRect of the element that was allocated.</param>
    public SKRect GetBackgroundRect(SKRect contentRect)
    {
        return Padding.GetDrawableRect(contentRect);
    }

    /// <summary>
    /// Creates a drawable rect for the border by expanding from the contentRect.
    /// </summary>
    /// <param name="contentRect">The SKRect of the element that was allocated.</param>
    public SKRect GetBorderRect(SKRect contentRect)
    {
        return Border.GetDrawableRect(Padding.GetDrawableRect(contentRect));
    }

    /// <summary>
    /// Creates a rect to outline where the margin starts.
    /// </summary>
    /// <param name="contentRect">The allocated element rect.</param>
    public SKRect GetMarginDebugRect(SKRect contentRect)
    {
        return Margin.GetDrawableRect(Border.GetDrawableRect(Padding.GetDrawableRect(contentRect)));
    }

    public SKRect GetPaddingDebugRect(SKRect contentRect)
    {
        return Padding.GetDrawableRect(contentRect);
    }
}

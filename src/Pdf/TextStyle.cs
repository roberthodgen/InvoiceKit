namespace InvoiceKit.Pdf;

public readonly record struct TextStyle()
{
    public const float DefaultFontSize = 12f;

    /// <summary>
    /// Should be specified as <c>Font Name/Style</c>, e.g.: <c>Open Sans/SemiBold</c>.
    /// </summary>
    public string? FontPath { get; init; } = null;

    public float LineHeight { get; init; } = 1.1f;

    public float FontSize { get; init; } = DefaultFontSize;

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
}

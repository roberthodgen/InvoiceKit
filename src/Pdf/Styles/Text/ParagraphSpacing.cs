namespace InvoiceKit.Pdf.Styles.Text;

/// <summary>
/// Paragraph spacing relative to the font size.
/// </summary>
public readonly record struct ParagraphSpacing()
{
    /// <summary>
    /// The relative amount of spacing before paragraphs.
    /// </summary>
    public float Before { get; init; } = 1.25f;

    /// <summary>
    /// The relative amount of spacing after paragraphs.
    /// </summary>
    public float After { get; init; } = 1.25f;
}

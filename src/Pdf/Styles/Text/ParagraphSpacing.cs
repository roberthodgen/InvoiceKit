namespace InvoiceKit.Pdf.Styles.Text;

public record struct ParagraphSpacing
{
    public float Before { get; init; } = 8f;

    public float After { get; init; } = 8f;

    public float Total => Before + After;

    public ParagraphSpacing(float before, float after)
    {
        Before = before;
        After = after;
    }
}

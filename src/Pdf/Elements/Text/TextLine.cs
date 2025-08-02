namespace InvoiceKit.Pdf.Elements.Text;

using Styles.Text;

internal class TextLine
{
    public required string Text { get; init; }

    public required TextStyle Style { get; init; }
}

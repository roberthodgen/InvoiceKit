namespace InvoiceKit.Domain.Invoice;

public record InvoiceNumber
{
    public string Value { get; }
    private InvoiceNumber(string input)
    {
        Value = input;
    }

    internal static InvoiceNumber CreateNew(string input)
    {
        return new InvoiceNumber(input);
    }

    public override string ToString()
    {
        // Todo: Create the string format for displaying an invoice number.
        return Value;
    }
}
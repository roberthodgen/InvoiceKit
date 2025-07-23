namespace InvoiceKit.Domain.Invoice;

public sealed record InvoiceNumber
{
    public string Value { get; }
    
    private InvoiceNumber(string input)
    {
        Value = input;
    }

    public static InvoiceNumber CreateNew(string input)
    {
        return new InvoiceNumber(input);
    }

    public override string ToString()
    {
        // Todo: Create the string format for displaying an invoice number.
        return Value;
    }
}
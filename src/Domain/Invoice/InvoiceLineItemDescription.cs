namespace InvoiceKit.Domain.Invoice;

public sealed record InvoiceLineItemDescription
{
    public string Value { get; }
    
    private InvoiceLineItemDescription(string input)
    {
        Value = input;
    }

    public static InvoiceLineItemDescription CreateNew(string input)
    {
        return new InvoiceLineItemDescription(input);
    }

    public override string ToString()
    {
        // Todo: Implement the string format for a description.
        return Value;
    }
}
namespace InvoiceKit.Domain.Shared.Kernel;

public abstract record Phone
{
    public string Value { get; }

    protected Phone(string input)
    {
        Value = input;   
    }
    
    /// <summary>Outputs a formatted phone number</summary>
    /// <returns> (###)###-#### </returns>
    public sealed override string ToString()
    {
        // Return early if there is no string
        if (string.IsNullOrEmpty(Value))
            return string.Empty;
        
        var digitsOnly = new string(Value.Where(char.IsDigit).ToArray());
        
        // Returns the original string if not valid phone number length
        if(digitsOnly.Length != 10){return Value;}
        
        // Returns formatted phone number
        return $"({digitsOnly[..3]}){digitsOnly[3..6]}-{digitsOnly[6..]}";

        // Todo: Could add a country code
    }
}
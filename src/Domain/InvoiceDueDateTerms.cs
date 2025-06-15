namespace InvoiceKit.Domain;

/// <summary>
/// E.g.: "30-days"
/// </summary>
public readonly record struct InvoiceDueDateTerms(int Days)
{
    public override string ToString()
    {
        if (Days == 0)
        {
            return "Due Upon Receipt";
        }
        
        return $"{Days}-days";
    }

    /// <summary>
    /// Creates a new <see cref="InvoiceDueDate"/> from the current local date.
    /// </summary>
    /// <returns></returns>
    public InvoiceDueDate ToDueDateFromNow()
    {
        return new InvoiceDueDate(this, DateOnly.FromDateTime(DateTime.Now));
    }

    public InvoiceDueDate ToDueDateFrom(DateOnly from)
    {
        return new InvoiceDueDate(this, from.AddDays(Days));
    }
}

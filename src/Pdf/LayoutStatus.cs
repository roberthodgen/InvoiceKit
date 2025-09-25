namespace InvoiceKit.Pdf;

public readonly record struct LayoutStatus
{
    public static LayoutStatus NeedsNewPage = new (1);

    public static LayoutStatus IsFullyDrawn = new (2);

    private int Value { get; }

    private LayoutStatus(int value)
    {
        Value = value;
    }
}

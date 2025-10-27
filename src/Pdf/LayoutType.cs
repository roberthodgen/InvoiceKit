namespace InvoiceKit.Pdf;

public readonly record struct LayoutType
{
    /// <summary>
    /// Indicates that layout is a repeating element.
    /// </summary>
    public static LayoutType RepeatingElement = new (1);

    /// <summary>
    /// Indicates that the layout should be drawn only once.
    /// </summary>
    public static LayoutType DrawOnceElement = new (2);

    private int Value { get; }

    private LayoutType(int value)
    {
        Value = value;
    }
}

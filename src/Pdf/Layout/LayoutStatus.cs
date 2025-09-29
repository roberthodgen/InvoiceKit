namespace InvoiceKit.Pdf.Layout;

/// <summary>
/// The status of a <see cref="Layout"/> object after its layout method was called.
/// </summary>
public readonly record struct LayoutStatus
{
    /// <summary>
    /// Indicates the <see cref="Layout"/> has more <see cref="IDrawable"/>s to layout.
    /// </summary>
    public static LayoutStatus NeedsNewPage = new (1);

    /// <summary>
    /// Indicates the <see cref="Layout"/> is complete.
    /// </summary>
    public static LayoutStatus IsFullyDrawn = new (2);

    private int Value { get; }

    private LayoutStatus(int value)
    {
        Value = value;
    }
}

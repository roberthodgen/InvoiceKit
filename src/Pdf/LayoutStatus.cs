namespace InvoiceKit.Pdf;

/// <summary>
/// The status of a <see cref="ILayout"/> object after its layout method was called.
/// </summary>
public readonly record struct LayoutStatus
{
    /// <summary>
    /// Indicates the <see cref="ILayout"/> has more <see cref="IDrawable"/>s to layout.
    /// </summary>
    public static LayoutStatus NeedsNewPage = new (1);

    /// <summary>
    /// Indicates the <see cref="ILayout"/> is complete.
    /// </summary>
    public static LayoutStatus IsFullyDrawn = new (2);

    /// <summary>
    /// The layout status is deferred to its children.
    /// </summary>
    /// <remarks>
    /// For use by layouts that don't directly return drawables like: VStacks and HStacks.
    /// </remarks>
    public static LayoutStatus Deferred = new (3);

    private int Value { get; }

    private LayoutStatus(int value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value switch
        {
            1 => "Needs New Page",
            2 => "Is Fully Drawn",
            3 => "Deferred",
            _ => "Unknown"
        };
    }
}

namespace InvoiceKit.Pdf;

/// <summary>
/// Describes how columns are laid out for an HStackLayout
/// </summary>
public readonly record struct ColumnType
{
    /// <summary>
    /// Each column will be an equal portion of the available width.
    /// </summary>
    /// <remarks>
    /// See <see cref="ColumnWidthEqual"/>
    /// </remarks>
    public static ColumnType Equal => new (0);

    /// <summary>
    /// Column is given a percentage of the available width.
    /// </summary>
    public static ColumnType Percent => new (1);

    /// <summary>
    /// Column is given a fixed width in points.
    /// </summary>
    public static ColumnType Points => new (2);

    private int Value { get; }

    private ColumnType(int value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value switch
        {
            0 => "Equal Width",
            1 => "Percent",
            2 => "Points",
            _ => "Unknown"
        };
    }

    /// <summary>
    /// Checks that a given column type can be converted to another type.
    /// </summary>
    /// <param name="other">The type to be converted to.</param>
    /// <returns>True when possible, false otherwise.</returns>
    public bool CanBeConvertedTo(ColumnType other)
    {
        if (this == Equal)
        {
            return true;
        }

        if (this == other)
        {
            return true;
        }

        return false;
    }
}

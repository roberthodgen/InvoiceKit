namespace InvoiceKit.Pdf.Layouts.Tables;

public readonly record struct ColumnWidthPercent
{
    public float Percent { get; init; }

    private ColumnWidthPercent(float percent)
    {
        Percent = percent;
    }

    /// <summary>
    /// Takes a value between 0 and 100.
    /// </summary>
    public static ColumnWidthPercent FromPercent(float percent)
    {
        return new (percent / 100f);
    }

    public static ColumnWidthPercent FromFraction(float value)
    {
        return new (value);
    }
}

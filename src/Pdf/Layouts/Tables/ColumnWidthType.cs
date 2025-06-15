namespace InvoiceKit.Pdf.Layouts.Tables;

/// <summary>
/// Specifies how a <see cref="ColumnWidthPercent"/> object computes its width.
/// </summary>
public enum ColumnSizing
{
    /// <summary>
    /// The column will be equally sized with all others.
    /// </summary>
    Equal,

    /// <summary>
    /// The column's width will be specified as a percentage.
    /// </summary>
    FixedPercentage,

    /// <summary>
    /// The column's width will be specified in exact points.
    /// </summary>
    FixedPoints,

    /// <summary>
    /// The column's width will be automatically computed.
    /// </summary>
    Auto,
}

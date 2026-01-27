namespace InvoiceKit.Pdf.Containers.Tables;

/// <summary>
/// Specifies how a <see cref="ColumnWidthPercent"/> object computes its width.
/// </summary>
public enum ColumnWidthType
{
    /// <summary>
    /// The column will be equally sized with all others.
    /// </summary>
    Equal,

    /// <summary>
    /// The column's width will be specified as a percentage.
    /// </summary>
    Percentage,

    /// <summary>
    /// The column's width will be specified in exact points.
    /// </summary>
    Points,

    /// <summary>
    /// The column's width will be automatically computed.
    /// </summary>
    Auto,
}

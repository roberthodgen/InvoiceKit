namespace InvoiceKit.Pdf.Views;

public interface IRow
{
    /// <summary>
    /// Adds custom column widths to all hStacks.
    /// </summary>
    IRow WithColumnWidths(Action<ColumnBuilder> configureColumns);
}

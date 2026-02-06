namespace InvoiceKit.Pdf;

using Geometry;

public interface IColumnWidth
{
    /// <summary>
    /// Creates an outer size for a column based on the provided layout context.
    /// </summary>
    /// <param name="context">The parent layout context.</param>
    /// <returns>The outer size for the column.</returns>
    OuterSize GetColumnSize(ILayoutContext context);
}

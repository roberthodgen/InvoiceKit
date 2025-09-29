namespace InvoiceKit.Pdf;

using Layout;

/// <summary>
/// A view build describes content to be added to a document.
/// </summary>
public interface IViewBuilder
{
    /// <summary>
    /// Gets a layout object from the view builder.
    /// </summary>
    internal ILayout ToLayout();
}

namespace InvoiceKit.Pdf;

public interface ILayout : IMeasurable
{
    /// <summary>
    /// Used to lay out drawables across multiple pages.
    /// </summary>
    void LayoutPages(MultiPageContext context, bool debug);
}

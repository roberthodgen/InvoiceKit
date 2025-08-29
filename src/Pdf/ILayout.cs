namespace InvoiceKit.Pdf;

public interface ILayout : IMeasurable
{
    /// <summary>
    /// Used to lay out drawables across multiple pages.
    /// </summary>
    /// <param name="context"></param>
    void LayoutPages(MultiPageContext context);
}

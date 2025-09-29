namespace InvoiceKit.Pdf.Layout;

/// <summary>
/// A layout result object contains the drawables assigned to page as well as the status of the layout.
/// </summary>
public sealed class LayoutResult(List<IDrawable> drawables, LayoutStatus status)
{
    /// <summary>
    /// The drawables assigned to the page.
    /// </summary>
    public IReadOnlyCollection<IDrawable> Drawables { get; } = drawables;

    /// <summary>
    /// The status of the layout.
    /// </summary>
    public LayoutStatus Status { get; } = status;
}

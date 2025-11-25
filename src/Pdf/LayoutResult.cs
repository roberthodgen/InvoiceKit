namespace InvoiceKit.Pdf;

/// <summary>
/// A layout result object contains the drawables assigned to page as well as the status of the layout.
/// </summary>
public sealed class LayoutResult
{
    /// <summary>
    /// The drawables assigned to the page.
    /// </summary>
    public IReadOnlyCollection<IDrawable> Drawables { get; }

    /// <summary>
    /// The status of the layout.
    /// </summary>
    public LayoutStatus Status { get; }

    /// <summary>
    /// The layout's children that still need to be laid out.
    /// </summary>
    public IReadOnlyCollection<ChildLayout> Children { get; }

    public LayoutResult(List<IDrawable> drawables, LayoutStatus status)
    {
        Drawables = drawables.AsReadOnly();
        Status = status;
        Children = [];
    }

    public LayoutResult(LayoutStatus status, List<ChildLayout> children)
    {
        Drawables = [];
        Status = status;
        Children = children.AsReadOnly();
    }

}

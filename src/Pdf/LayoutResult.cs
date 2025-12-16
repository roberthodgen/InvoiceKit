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

    /// <summary>
    /// Constructor for <see cref="NeedsNewPage"/> and <see cref="FullyDrawn"/>.
    /// </summary>
    /// <param name="drawables"></param>
    /// <param name="status"></param>
    private LayoutResult(List<IDrawable> drawables, LayoutStatus status)
    {
        Drawables = drawables.AsReadOnly();
        Status = status;
        Children = [];
    }

    /// <summary>
    /// Constructor for <see cref="Deferred"/>.
    /// </summary>
    /// <param name="status"></param>
    /// <param name="children"></param>
    private LayoutResult(LayoutStatus status, List<ChildLayout> children)
    {
        Drawables = [];
        Status = status;
        Children = children.AsReadOnly();
    }

    /// <summary>
    /// Returns a fully drawn layout result with laid-out drawables.
    /// </summary>
    public static LayoutResult FullyDrawn(List<IDrawable> drawables) => new (drawables, LayoutStatus.IsFullyDrawn);

    /// <summary>
    /// Returns a partial layout result with drawables assigned to the page. The layout status indicates the layout
    /// has additional drawables to assign on the next page.
    /// </summary>
    public static LayoutResult NeedsNewPage(List<IDrawable> drawables) => new (drawables, LayoutStatus.NeedsNewPage);

    /// <summary>
    /// Returns a layout result with deferred children that need to be laid out.
    /// </summary>
    public static LayoutResult Deferred(List<ChildLayout> children) => new (LayoutStatus.Deferred, children);
}

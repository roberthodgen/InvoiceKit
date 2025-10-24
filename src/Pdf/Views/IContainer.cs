namespace InvoiceKit.Pdf.Views;

using Containers.Tables;
using Styles.Text;

/// <summary>
/// A layout is a drawable component that contains one or more children.
/// Layouts compose and create complex PDF layouts.
/// </summary>
public interface IContainer : IViewBuilder
{
    TextStyle DefaultTextStyle { get; }

    /// <summary>
    /// Adds a new text block.
    /// </summary>
    IContainer AddText(Func<TextViewBuilder, IViewBuilder> builder);

    /// <summary>
    /// Adds a new image.
    /// </summary>
    IContainer AddImage(Func<ImageViewBuilder, IViewBuilder> builder);

    /// <summary>
    /// Adds a new horizontal rule.
    /// </summary>
    IContainer AddHorizontalRule();

    /// <summary>
    /// Adds a stack of columns.
    /// </summary>
    IContainer AddHStack(Action<HStack> configureColumnStack);

    /// <summary>
    /// Adds a stack of columns.
    /// </summary>
    IContainer AddVStack(Action<VStack> configureRowStack);

    /// <summary>
    /// Adds spacing between blocks.
    /// </summary>
    /// <param name="height">Float for the amount of spacing. Default of 5f.</param>
    IContainer AddSpacing(float height = 5f);

    /// <summary>
    /// Adds a new table block.
    /// </summary>
    IContainer AddTable(Action<TableViewBuilder> configureTableBlock);

    /// <summary>
    /// Fills the rest of the page with blank space and starts a new page.
    /// </summary>
    IContainer AddPageBreak();
}

namespace InvoiceKit.Pdf.Views;

using Containers.Tables;
using Styles;

/// <summary>
/// A layout is a drawable component that contains one or more children.
/// Layouts compose and create complex PDF layouts.
/// </summary>
public interface IContainer : IViewBuilder
{
    BlockStyle DefaultStyle { get; }

    /// <summary>
    /// Adds a new text block.
    /// </summary>
    IContainer AddText(string text);

    /// <summary>
    /// Adds a new text block with custom styling.
    /// </summary>
    IContainer AddText(string text, Func<BlockStyle, BlockStyle> configureStyle);

    /// <summary>
    /// Adds a new image.
    /// </summary>
    IContainer AddImage(Func<ImageViewBuilder, IViewBuilder> builder);

    /// <summary>
    /// Adds a new image.
    /// </summary>
    IContainer AddImage(Func<ImageViewBuilder, IViewBuilder> builder, Func<BlockStyle, BlockStyle> configureStyle);

    /// <summary>
    /// Adds a new horizontal rule.
    /// </summary>
    IContainer AddHorizontalRule();

    /// <summary>
    /// Adds a new horizontal rule with custom styling.
    /// </summary>
    IContainer AddHorizontalRule(Func<BlockStyle, BlockStyle> configureStyle);

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

    /// <summary>
    /// Configures the view's style.
    /// </summary>
    /// <param name="configureStyle">The base/parent style which may be modified as needed.</param>
    /// <returns>The style to be applied to this view.</returns>
    /// <remarks>
    /// Will override document style if it is the first stack on the document.<br />
    /// Margin, padding, and border will be reset for all children.
    /// </remarks>
    IContainer WithDefaultStyle(Func<BlockStyle, BlockStyle> configureStyle);
}

namespace InvoiceKit.Pdf;

using Layouts.Images;
using Layouts.Stacks;
using Layouts.Tables;
using Layouts.Text;
using Styles.Text;

public interface ILayoutBuilder
{
    TextStyle DefaultTextStyle { get; }

    /// <summary>
    /// Adds a new text block.
    /// </summary>
    ILayoutBuilder AddTextBlock(Action<TextBlock> configureTextBlock);

    /// <summary>
    /// Adds a new image block.
    /// </summary>
    ILayoutBuilder AddImageBlock(string imagePath);

    /// <summary>
    /// Adds a new horizontal rule.
    /// </summary>
    ILayoutBuilder AddHorizontalRule();

    /// <summary>
    /// Adds a stack of columns.
    /// </summary>
    ILayoutBuilder AddColumnStack(Action<HStack> configureColumnStack);

    /// <summary>
    /// Adds a stack of columns.
    /// </summary>
    ILayoutBuilder AddRowStack(Action<VStack> configureRowStack);

    /// <summary>
    /// Adds spacing between blocks.
    /// </summary>
    /// <param name="height">Float for the amount of spacing. Default of 5f.</param>
    ILayoutBuilder AddSpacingBlock(float height = 5f);

    /// <summary>
    /// Adds a new table block.
    /// </summary>
    ILayoutBuilder AddTableBlock(Action<TableLayoutBuilder> configureTableBlock);
}

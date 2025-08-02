namespace InvoiceKit.Pdf;

using Elements;
using Elements.Images;
using Layouts.Stacks;
using Layouts.Tables;
using Styles.Text;

/// <summary>
/// A layout is a drawable component that contains one or more children. Layouts are used to compose and create complex
/// PDF layouts.
/// </summary>
public interface ILayout : IDrawable
{
    TextStyle DefaultTextStyle { get; }

    /// <summary>
    /// Adds a new text block.
    /// </summary>
    ILayout AddText(Func<TextBuilder, IDrawable> builder);

    /// <summary>
    /// Adds a new image.
    /// </summary>
    ILayout AddImage(Func<ImageBuilder, IDrawable> builder);

    /// <summary>
    /// Adds a new horizontal rule.
    /// </summary>
    ILayout AddHorizontalRule();

    /// <summary>
    /// Adds a stack of columns.
    /// </summary>
    ILayout AddHStack(Action<HStack> configureColumnStack);

    /// <summary>
    /// Adds a stack of columns.
    /// </summary>
    ILayout AddVStack(Action<VStack> configureRowStack);

    /// <summary>
    /// Adds spacing between blocks.
    /// </summary>
    /// <param name="height">Float for the amount of spacing. Default of 5f.</param>
    ILayout AddSpacingBlock(float height = 5f);

    /// <summary>
    /// Adds a new table block.
    /// </summary>
    ILayout AddTableBlock(Action<TableLayoutBuilder> configureTableBlock);
}

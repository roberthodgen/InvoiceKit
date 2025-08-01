namespace InvoiceKit.Pdf;

using Layouts.Images;
using Layouts.Stacks;
using Layouts.Tables;
using Layouts.Text;
using Styles.Text;

public abstract class LayoutBuilderBase : ILayoutBuilder
{
    protected IDrawable? Child { get; set; }
    public TextStyle DefaultTextStyle { get; protected set; }

    protected LayoutBuilderBase(TextStyle defaultTextStyle)
    {
        DefaultTextStyle = defaultTextStyle;
    }

    public ILayoutBuilder AddTextBlock(Action<TextBlock> configure)
    {
        var child = new TextBlock(DefaultTextStyle);
        configure(child);
        Child = child;
        return this;
    }

    // Todo: make sure we can make the png block
    public ILayoutBuilder AddImageBlock(string imagePath)
    {
        var child = ImageBlock.CreateSvg(imagePath);
        Child = child;
        return this;
    }

    public ILayoutBuilder AddHorizontalRule()
    {
        var child = new HorizontalRule();
        Child = child;
        return this;
    }

    public ILayoutBuilder AddColumnStack(Action<HStack> configure)
    {
        var child = new HStack(DefaultTextStyle);
        configure(child);
        Child = child;
        return this;
    }

    public ILayoutBuilder AddRowStack(Action<VStack> configure)
    {
        var child = new VStack(DefaultTextStyle);
        configure(child);
        Child = child;
        return this;
    }

    public ILayoutBuilder AddSpacingBlock(float height = 5)
    {
        var child = new SpacingBlock(height);
        Child = child;
        return this;
    }

    public ILayoutBuilder AddTableBlock(Action<TableLayoutBuilder> configure)
    {
        var child = new TableLayoutBuilder(DefaultTextStyle);
        configure(child);
        Child = child;
        return this;
    }
}

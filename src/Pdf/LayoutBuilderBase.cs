namespace InvoiceKit.Pdf;

using Elements;
using Elements.Text;
using Elements.Images;
using Layouts.Stacks;
using Layouts.Tables;
using SkiaSharp;
using Styles.Text;
using Layouts;

public abstract class LayoutBuilderBase : ILayoutBuilder
{
    protected List<IDrawable> Children { get; } = [];

    public TextStyle DefaultTextStyle { get; protected set; }

    protected LayoutBuilderBase(TextStyle defaultTextStyle)
    {
        DefaultTextStyle = defaultTextStyle;
    }

    public ILayoutBuilder AddText(Func<TextViewBuilder, IDrawable> builder)
    {
        var child = builder(new TextViewBuilder(DefaultTextStyle));
        Children.Add(child);
        return this;
    }

    public ILayoutBuilder AddImage(Func<ImageBuilder, IDrawable> builder)
    {
        var child = builder(new ImageBuilder());
        Children.Add(child);
        return this;
    }

    public ILayoutBuilder AddHorizontalRule()
    {
        var child = new HorizontalRuleDrawable();
        Children.Add(child);
        return this;
    }

    public ILayoutBuilder AddHStack(Action<HStack> configure)
    {
        var child = new HStack(DefaultTextStyle);
        configure(child);
        Children.Add(child);
        return this;
    }

    public ILayoutBuilder AddVStack(Action<VStack> configure)
    {
        var child = new VStack(DefaultTextStyle);
        configure(child);
        Children.Add(child);
        return this;
    }

    public ILayoutBuilder AddSpacingBlock(float height = 5)
    {
        var child = new SpacingBlock(height);
        Children.Add(child);
        return this;
    }

    public ILayoutBuilder AddTableBlock(Action<TableLayoutBuilder> configure)
    {
        var child = new TableLayoutBuilder(DefaultTextStyle);
        configure(child);
        Children.Add(child);
        return this;
    }

    public ILayoutBuilder AddPageBreak()
    {
        var child = new PageBreak();
        Children.Add(child);
        return this;
    }

    public void Dispose()
    {
        foreach (var child in Children)
        {
            child.Dispose();
        }
    }
}

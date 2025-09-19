namespace InvoiceKit.Pdf;

using Elements.Text;
using Elements.Images;
using Containers.Stacks;
using Containers.Tables;
using Containers.SpacingBlock;
using Containers.PageBreak;
using Elements.HorizontalRule;
using Styles.Text;

public abstract class ContainerBase : IContainer
{
    protected List<IViewBuilder> Children { get; } = [];

    public TextStyle DefaultTextStyle { get; protected set; }

    protected ContainerBase(TextStyle defaultTextStyle)
    {
        DefaultTextStyle = defaultTextStyle;
    }

    public IContainer AddText(Func<TextViewBuilder, IViewBuilder> builder)
    {
        var child = builder(new TextViewBuilder(DefaultTextStyle));
        Children.Add(child);
        return this;
    }

    public IContainer AddImage(Func<ImageViewBuilder, IViewBuilder> builder)
    {
        var child = builder(new ImageViewBuilder());
        Children.Add(child);
        return this;
    }

    public IContainer AddHorizontalRule()
    {
        var child = new HorizontalRuleViewBuilder();
        Children.Add(child);
        return this;
    }

    public IContainer AddHStack(Action<HStack> configure)
    {
        var child = new HStack(DefaultTextStyle);
        configure(child);
        Children.Add(child);
        return this;
    }

    public IContainer AddVStack(Action<VStack> configure)
    {
        var child = new VStack(DefaultTextStyle);
        configure(child);
        Children.Add(child);
        return this;
    }

    public IContainer AddSpacing(float height = 5)
    {
        var child = new SpacingBlock(height);
        Children.Add(child);
        return this;
    }

    public IContainer AddTable(Action<TableViewBuilder> configure)
    {
        var child = new TableViewBuilder(DefaultTextStyle);
        configure(child);
        Children.Add(child);
        return this;
    }

    public IContainer AddPageBreak()
    {
        var child = new PageBreakViewBuilder();
        Children.Add(child);
        return this;
    }

    public abstract ILayout ToLayout();
}

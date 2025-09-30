namespace InvoiceKit.Pdf.Views;

using Containers.Tables;
using Styles.Text;

public abstract class ContainerBase : IContainer
{
    private readonly List<IViewBuilder> _children = [];

    public TextStyle DefaultTextStyle { get; protected set; }

    protected IReadOnlyCollection<IViewBuilder> Children => _children.AsReadOnly();

    protected ContainerBase(TextStyle defaultTextStyle)
    {
        DefaultTextStyle = defaultTextStyle;
    }

    public IContainer AddText(Func<TextViewBuilder, IViewBuilder> builder)
    {
        var child = builder(new TextViewBuilder(DefaultTextStyle));
        _children.Add(child);
        return this;
    }

    public IContainer AddImage(Func<ImageViewBuilder, IViewBuilder> builder)
    {
        var child = builder(new ImageViewBuilder());
        _children.Add(child);
        return this;
    }

    public IContainer AddHorizontalRule()
    {
        var child = new HorizontalRuleViewBuilder();
        _children.Add(child);
        return this;
    }

    public IContainer AddHStack(Action<HStack> configure)
    {
        var child = new HStack(DefaultTextStyle);
        configure(child);
        _children.Add(child);
        return this;
    }

    public IContainer AddVStack(Action<VStack> configure)
    {
        var child = new VStack(DefaultTextStyle);
        configure(child);
        _children.Add(child);
        return this;
    }

    public IContainer AddSpacing(float height = 5f)
    {
        var child = new SpacingBlockViewBuilder(height);
        _children.Add(child);
        return this;
    }

    public IContainer AddTable(Action<TableViewBuilder> configure)
    {
        var child = new TableViewBuilder(DefaultTextStyle);
        configure(child);
        _children.Add(child);
        return this;
    }

    public IContainer AddPageBreak()
    {
        var child = new PageBreakViewBuilder();
        _children.Add(child);
        return this;
    }

    public abstract ILayout ToLayout();
}

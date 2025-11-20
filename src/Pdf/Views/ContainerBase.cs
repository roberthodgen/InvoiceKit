namespace InvoiceKit.Pdf.Views;

using Containers.Tables;

public abstract class ContainerBase : IContainer
{
    private readonly List<IViewBuilder> _children = [];

    public BlockStyle DefaultStyle { get; protected set; }

    protected IReadOnlyCollection<IViewBuilder> Children => _children.AsReadOnly();

    protected ContainerBase(BlockStyle defaultStyle)
    {
        DefaultStyle = defaultStyle;
    }

    public IContainer AddText(string text)
    {
        var child = new TextViewBuilder(text, DefaultStyle);
        _children.Add(child);
        return this;
    }

    public IContainer AddText(string text, Func<BlockStyle, BlockStyle> configureTextStyle)
    {
        var child = new TextViewBuilder(text, configureTextStyle(DefaultStyle));
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
        var child = new HorizontalRuleViewBuilder(DefaultStyle);
        _children.Add(child);
        return this;
    }

    public IContainer AddHorizontalRule(Func<BlockStyle, BlockStyle> configureTextStyle)
    {
        var child = new HorizontalRuleViewBuilder(configureTextStyle(DefaultStyle));
        _children.Add(child);
        return this;
    }

    public IContainer AddHStack(Action<HStack> configure)
    {
        var child = new HStack(DefaultStyle);
        configure(child);
        _children.Add(child);
        return this;
    }

    public IContainer AddVStack(Action<VStack> configure)
    {
        var child = new VStack(DefaultStyle);
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
        var child = new TableViewBuilder(DefaultStyle);
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

    public IContainer WithDefaultStyle(Func<BlockStyle, BlockStyle> configureStyle)
    {
        DefaultStyle = configureStyle(DefaultStyle);
        return this;
    }

    public abstract ILayout ToLayout();
}

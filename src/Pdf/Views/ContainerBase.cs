namespace InvoiceKit.Pdf.Views;

using Containers.Tables;
using Styles;

public abstract class ContainerBase(BlockStyle defaultStyle) : IContainer
{
    private readonly List<IViewBuilder> _children = [];

    public BlockStyle DefaultStyle { get; private set; } = defaultStyle;

    protected IReadOnlyCollection<IViewBuilder> Children => _children.AsReadOnly();

    private BlockStyle ChildStyle => DefaultStyle.CopyForChild();

    public IContainer AddText(string text)
    {
        var child = new TextViewBuilder(text, ChildStyle with
        {
            Padding = new Padding()
            {
                Top = ChildStyle.FontSize,
                Bottom = ChildStyle.FontSize,
            }
        });
        _children.Add(child);
        return this;
    }

    public IContainer AddText(string text, Func<BlockStyle, BlockStyle> configureTextStyle)
    {
        var child = new TextViewBuilder(text, configureTextStyle(ChildStyle));
        _children.Add(child);
        return this;
    }

    public IContainer AddImage(Func<ImageViewBuilder, IViewBuilder> builder)
    {
        var child = builder(new ImageViewBuilder(ChildStyle));
        _children.Add(child);
        return this;
    }

    public IContainer AddImage(Func<ImageViewBuilder, IViewBuilder> builder,
        Func<BlockStyle, BlockStyle> configureStyle)
    {
        var child = builder(new ImageViewBuilder(configureStyle(ChildStyle)));
        _children.Add(child);
        return this;
    }

    public IContainer AddHorizontalRule()
    {
        var child = new HorizontalRuleViewBuilder(ChildStyle);
        _children.Add(child);
        return this;
    }

    public IContainer AddHorizontalRule(Func<BlockStyle, BlockStyle> configureTextStyle)
    {
        var child = new HorizontalRuleViewBuilder(configureTextStyle(ChildStyle));
        _children.Add(child);
        return this;
    }

    public IContainer AddHStack(Action<HStack> configure)
    {
        var child = new HStack(ChildStyle);
        configure(child);
        _children.Add(child);
        return this;
    }

    public IContainer AddVStack(Action<VStack> configure)
    {
        var child = new VStack(ChildStyle);
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
        var child = new TableViewBuilder(ChildStyle);
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

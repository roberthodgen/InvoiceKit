namespace InvoiceKit.Pdf.Views;

using Layouts;

/// <summary>
/// Renders content vertically. Each row is rendered on a new line.
/// </summary>
public sealed class VStack : ContainerBase, ITable
{
    private VStack? _header;

    private VStack? _footer;

    private readonly bool _headerOrFooter;

    internal VStack(BlockStyle defaultStyle)
        : base(defaultStyle)
    {
    }

    private VStack(BlockStyle defaultStyle, bool headerOrFooter)
        : base(defaultStyle)
    {
        _headerOrFooter = headerOrFooter;
    }

    public override ILayout ToLayout()
    {
        var childrenLayouts = Children.Select(child => child.ToLayout()).ToList();
        if (_header is not null)
        {
            var header = new RepeatingLayout(_header.ToLayout());
            return new VStackLayout(
                childrenLayouts.Select(ILayout (child) =>
                        new VStackLayout([header, child,]))
                    .ToList());
        }

        return new VStackLayout(childrenLayouts);
    }

    public ITable WithColumnWidths(Action<ColumnBuilder> configureColumns)
    {
        var builder = new ColumnBuilder();
        configureColumns(builder);
        ColumnType = builder.ColumnType;
        ColumnWidths = builder.ColumnWidths;
        return this;
    }

    public ITable WithHeader(Action<VStack> configure)
    {
        if (_headerOrFooter)
        {
            throw new Exception("Cannot add a header to another header or footer.");
        }

        _header = new VStack(DefaultStyle.CopyForChild(), true);
        _header.ColumnType = ColumnType;
        _header.ColumnWidths = ColumnWidths;
        configure(_header);
        return this;
    }

    public ITable WithHeader(Action<VStack> configure, Func<BlockStyle, BlockStyle> configureStyle)
    {
        if (_headerOrFooter)
        {
            throw new Exception("Cannot add a header to another header or footer.");
        }

        _header = new VStack(configureStyle(DefaultStyle.CopyForChild()), true);
        _header.ColumnType = ColumnType;
        _header.ColumnWidths = ColumnWidths;
        configure(_header);
        return this;
    }

    public ITable WithFooter(Action<VStack> configure)
    {
        if (_headerOrFooter)
        {
            throw new Exception("Cannot add a footer to another header or footer.");
        }

        _footer = new VStack(DefaultStyle.CopyForChild(), true);
        _footer.ColumnType = ColumnType;
        _footer.ColumnWidths = ColumnWidths;
        configure(_footer);
        return this;
    }

    public ITable WithFooter(Action<VStack> configure, Func<BlockStyle, BlockStyle> configureStyle)
    {
        if (_headerOrFooter)
        {
            throw new Exception("Cannot add a a footer to another header or footer.");
        }

        _footer = new VStack(configureStyle(DefaultStyle.CopyForChild()), true);
        _footer.ColumnType = ColumnType;
        _footer.ColumnWidths = ColumnWidths;
        configure(_footer);
        return this;
    }
}

namespace InvoiceKit.Pdf.Views;

using Layouts;

/// <summary>
/// Renders content vertically. Each row is rendered on a new line.
/// </summary>
public sealed class VStack : ContainerBase
{
    private VStack? _header;

    private VStack? _footer;

    private readonly bool _repeating;

    internal VStack(BlockStyle defaultStyle)
        : base(defaultStyle)
    {
    }

    private VStack(BlockStyle defaultStyle, bool repeating)
        : base(defaultStyle)
    {
        _repeating = repeating;
    }

    public override ILayout ToLayout()
    {
        var childrenLayouts = Children.Select(child => child.ToLayout()).ToList();
        if (_repeating)
        {
            return new VStackRepeatingLayout(childrenLayouts, DefaultStyle);
        }

        return new VStackLayout(childrenLayouts, _header?.ToLayout(), _footer?.ToLayout(), DefaultStyle);
    }

    public VStack WithHeader(Action<VStack> configure)
    {
        if (_repeating)
        {
            throw new Exception("Cannot add a repeating stack to another repeating stack.");
        }

        _header = new VStack(DefaultStyle.CopyForChild(), true);
        configure(_header);
        return this;
    }

    public VStack WithHeader(Action<VStack> configure, Func<BlockStyle, BlockStyle> configureStyle)
    {
        if (_repeating)
        {
            throw new Exception("Cannot add a repeating stack to another repeating stack.");
        }

        _header = new VStack(configureStyle(DefaultStyle.CopyForChild()), true);
        configure(_header);
        return this;
    }

    public VStack WithFooter(Action<VStack> configure)
    {
        if (_repeating)
        {
            throw new Exception("Cannot add a repeating stack to another repeating stack.");
        }

        _footer = new VStack(DefaultStyle.CopyForChild(), true);
        configure(_footer);
        return this;
    }

    public VStack WithFooter(Action<VStack> configure, Func<BlockStyle, BlockStyle> configureStyle)
    {
        if (_repeating)
        {
            throw new Exception("Cannot add a repeating stack to another repeating stack.");
        }

        _footer = new VStack(configureStyle(DefaultStyle.CopyForChild()), true);
        configure(_footer);
        return this;
    }
}

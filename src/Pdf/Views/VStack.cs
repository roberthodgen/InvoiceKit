namespace InvoiceKit.Pdf.Views;

using Layouts;
using Styles.Text;

/// <summary>
/// Renders content vertically. Each row is rendered on a new line.
/// </summary>
public sealed class VStack : ContainerBase
{
    private VStack? _header;

    private VStack? _footer;

    private readonly bool _repeating;

    internal VStack(TextStyle defaultTextStyle)
        : base(defaultTextStyle)
    {
    }

    private VStack(TextStyle defaultTextStyle, bool repeating)
        : base(defaultTextStyle)
    {
        _repeating = repeating;
    }

    public override ILayout ToLayout()
    {
        var childrenLayouts = Children.Select(child => child.ToLayout()).ToList();
        return new VStackLayout(childrenLayouts, _header?.ToLayout(), _footer?.ToLayout(), _repeating);
    }

    public VStack WithHeader(Action<VStack> configure)
    {
        if (_repeating)
        {
            throw new Exception("Cannot add a repeating stack to another repeating stack.");
        }
        _header = new VStack(DefaultTextStyle, true);
        configure(_header);
        return this;
    }

    public VStack WithFooter(Action<VStack> configure)
    {
        if (_repeating)
        {
            throw new Exception("Cannot add a repeating stack to another repeating stack.");
        }
        _footer = new VStack(DefaultTextStyle, true);
        configure(_footer);
        return this;
    }
}

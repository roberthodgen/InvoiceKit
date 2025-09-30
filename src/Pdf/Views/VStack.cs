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

    internal VStack(TextStyle defaultTextStyle)
        : base(defaultTextStyle)
    {
    }

    public override ILayout ToLayout()
    {
        var childrenLayouts = Children.Select(child => child.ToLayout()).ToList();
        return new VStackLayout(childrenLayouts, _header?.ToLayout(), _footer?.ToLayout());
    }

    public VStack WithHeader(Action<VStack> configure)
    {
        _header = new VStack(DefaultTextStyle);
        configure(_header);
        return this;
    }
    public VStack WithFooter(Action<VStack> configure)
    {
        _footer = new VStack(DefaultTextStyle);
        configure(_footer);
        return this;
    }
}

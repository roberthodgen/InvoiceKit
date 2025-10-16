namespace InvoiceKit.Pdf.Views;

using Layouts;
using Styles.Text;

/// <summary>
/// Renders content vertically. Each row is rendered on a new line.
/// </summary>
public sealed class VStack : ContainerBase
{
    internal VStack(TextStyle defaultTextStyle)
        : base(defaultTextStyle)
    {
    }

    public override ILayout ToLayout()
    {
        var childrenLayouts = Children.Select(child => child.ToLayout()).ToList();
        return new VStackLayout(childrenLayouts);
    }
}

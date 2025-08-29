namespace InvoiceKit.Pdf.Containers.Stacks;

using Styles.Text;

/// <summary>
/// Renders content vertically. Each row is rendered on a new line.
/// </summary>
public class VStack : ContainerBase
{
    internal VStack(TextStyle defaultTextStyle)
        : base(defaultTextStyle)
    {
    }

    public override ILayout ToLayout()
    {
        var childrenLayouts = Children.Select(child => child.ToLayout()).ToList();
        var vStackLayout = new VStackLayout(childrenLayouts);
        return vStackLayout;
    }
}

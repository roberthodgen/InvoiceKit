namespace InvoiceKit.Pdf.Containers.Stacks;

using Styles.Text;

/// <summary>
/// Renders content horizontally. Each column is rendered side-by-side.
/// </summary>
public class HStack : ContainerBase
{
    internal HStack(TextStyle defaultTextStyle)
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

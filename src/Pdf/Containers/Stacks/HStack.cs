namespace InvoiceKit.Pdf.Containers.Stacks;

using Layout;
using Styles.Text;

/// <summary>
/// Renders content horizontally. Each column is rendered side-by-side.
/// </summary>
/// <remarks>If you need more than one element in a column, use a <see cref="VStack"/> inside of this.</remarks>
public sealed class HStack : ContainerBase
{
    internal HStack(TextStyle defaultTextStyle)
        : base(defaultTextStyle)
    {
    }

    public override ILayout ToLayout()
    {
        var childrenLayouts = Children.Select(child => child.ToLayout()).ToList();
        return new HStackLayout(childrenLayouts);
    }
}

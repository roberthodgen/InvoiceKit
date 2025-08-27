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
        throw new NotImplementedException();
    }
}

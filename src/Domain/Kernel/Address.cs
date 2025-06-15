namespace InvoiceKit.Domain.Kernel;

using System.Text;

public sealed record Address
{
    public required string Street { get; init; }

    public string? Street2 { get; init; }

    public required string City { get; init; }

    public required string State { get; init; }

    public required string Zip { get; init; }

    public string? Country { get; init; }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine(Street);
        if (Street2 is not null)
        {
            builder.AppendLine(Street2);
        }

        builder.Append($"{City}, {State} {Zip}");
        if (Country is not null)
        {
            builder.AppendLine();
            builder.AppendLine(Country);
        }

        return builder.ToString();
    }
}

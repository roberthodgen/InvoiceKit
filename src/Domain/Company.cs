namespace InvoiceKit.Domain;

using Kernel;

/// <summary>
/// Represents the issuer of the invoice.
/// </summary>
public sealed record Company
{
    public required string Name { get; init; }

    public Address? Address { get; init; }

    public string? EmailAddress { get; init; }

    public string? PhoneNumber { get; init; }

    public string? TaxId { get; init; }
}

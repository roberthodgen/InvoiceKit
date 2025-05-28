namespace InvoiceKit.Domain;

using Kernel;

/// <summary>
/// Represents the recipient of the invoice.
/// </summary>
public sealed record Customer
{
    /// <summary>
    /// A contact person's name. Optional.
    /// </summary>
    public string? Contact { get; init; }

    public required string Name { get; init; }

    public Address? BillingAddress { get; init; }

    public Address? ShippingAddress { get; init; }

    public string? PhoneNumber { get; init; }
    
    public string? EmailAddress { get; init; }
}

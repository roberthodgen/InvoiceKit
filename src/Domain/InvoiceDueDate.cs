namespace InvoiceKit.Domain;

public sealed record InvoiceDueDate(InvoiceDueDateTerms Terms, DateOnly Date);

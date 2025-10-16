namespace SIEGFiscal.Application.DTOs;

public class FiscalDocumentDto
{
    public FiscalDocumentDto(object id, object key, object emitCnpj, object recipientCnpj, object uf, object emissionDate, object totalValue)
    {
    }

    public Guid Id { get; set; }
    public string? Key { get; set; }
    public string XmlHash { get; set; } = null!;
    public DateTime EmissionDate { get; set; }
    public string? EmitCnpj { get; set; }
    public string? RecipientCnpj { get; set; }
    public string? Uf { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

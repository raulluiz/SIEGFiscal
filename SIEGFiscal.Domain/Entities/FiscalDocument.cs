using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIEGFiscal.Domain.Entities;

public class FiscalDocument
{
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIEGFiscal.Application.DTOs;

public class DocumentDto
{
    public Guid Id { get; set; }
    public string? Key { get; set; }
    public string XmlHash { get; set; } = null!;
    public DateTime EmissionDate { get; set; }
    public string? EmitCnpj { get; set; }
    public string? Uf { get; set; }
    public decimal TotalValue { get; set; }
}

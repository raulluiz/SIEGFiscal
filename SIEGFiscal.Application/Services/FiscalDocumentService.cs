using SIEGFiscal.Application.DTOs;
using SIEGFiscal.Domain.Entities;
using SIEGFiscal.Domain.Interfaces;
using System.Xml.Linq;

namespace SIEGFiscal.Application.Services;

public class FiscalDocumentService
{
    private readonly IFiscalDocumentRepository _fiscalDocumentRepository;
    public FiscalDocumentService(IFiscalDocumentRepository fiscalDocumentRepository)
    {
        _fiscalDocumentRepository = fiscalDocumentRepository;
    }

    public async Task<IEnumerable<FiscalDocumentDto>> GetAllFiscalDocumentsAsync()
    {
        return (IEnumerable<FiscalDocumentDto>)await _fiscalDocumentRepository.GetAllAsync();
    }

    public async Task<bool> ProcessXmlAsync(Stream xmlStream)
    {

        // Load an XDocument from a file
        XDocument loadedDoc = XDocument.Load(xmlStream);

        // Query the XDocument using LINQ
        var items = loadedDoc.Descendants("Item")
                            .Select(e => new
                            {
                                Key = e.Attribute("chNFe")?.Value,
                                XmlHash = e.Attribute("xmlHash")?.Value,
                                EmissionDate = e.Attribute("emissionDate")?.Value,
                                EmitCnpj = e.Attribute("emitCnpj")?.Value,
                                RecipientCnpj = e.Attribute("recipientCnpj")?.Value,
                                Uf = e.Attribute("cUF")?.Value,
                                TotalValue = e.Attribute("totalValue")?.Value,
                                Content = e.Value
                            });

        foreach (var item in items)
        {
            FiscalDocument dto = new FiscalDocument
            {
                Id = Guid.NewGuid(),
                Key = item.Key,
                XmlHash = item.XmlHash ?? string.Empty,
                EmissionDate = DateTime.Parse(item.EmissionDate ?? DateTime.MinValue.ToString()),
                EmitCnpj = item.EmitCnpj,
                RecipientCnpj = item.RecipientCnpj,
                Uf = item.Uf,
                TotalValue = decimal.Parse(item.TotalValue ?? "0"),
            };
            await _fiscalDocumentRepository.AddAsync(dto);
        }

        return true;
    }
}
//public Guid Id { get; set; }
//    public string? Key { get; set; }
//    public string XmlHash { get; set; } = null!;
//    public DateTime EmissionDate { get; set; }
//    public string? EmitCnpj { get; set; }
//    public string? RecipientCnpj { get; set; }
//    public string? Uf { get; set; }
//    public decimal TotalValue { get; set; }
//    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
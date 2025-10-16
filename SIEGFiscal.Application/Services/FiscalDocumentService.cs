using MassTransit;
using SIEGFiscal.Application.DTOs;
using SIEGFiscal.Application.Events;
using SIEGFiscal.Domain.Entities;
using SIEGFiscal.Domain.Interfaces;
using System.Xml.Linq;

namespace SIEGFiscal.Application.Services;

public class FiscalDocumentService(IFiscalDocumentRepository fiscalDocumentRepository, IPublishEndpoint publishEndpoint)
{
    private readonly IFiscalDocumentRepository _fiscalDocumentRepository = fiscalDocumentRepository;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public async Task<IEnumerable<FiscalDocumentDto>> GetAllFiscalDocumentsAsync()
    {
        return (IEnumerable<FiscalDocumentDto>)await _fiscalDocumentRepository.GetAllAsync();
    }

    public async Task<ProcessResultDto> ProcessXmlAsync(Stream xmlStream)
    {
        XDocument loadedDoc = XDocument.Load(xmlStream);

        XNamespace ns = "http://www.portalfiscal.inf.br/nfe";

        var infNFe = loadedDoc.Descendants(ns + "infNFe").FirstOrDefault();
        if (infNFe == null)
            return new ProcessResultDto { IsSuccess = false, Message = "Documento inválido!", DocumentId = null };

        var chaveNFe = (string)infNFe.Attribute("Id") ?? string.Empty;
        var dhEmi = (string)infNFe.Element(ns + "ide")?.Element(ns + "dhEmi") ?? string.Empty;
        var emitCnpj = (string)infNFe.Element(ns + "emit")?.Element(ns + "CNPJ") ?? string.Empty;
        var destCnpj = (string)infNFe.Element(ns + "dest")?.Element(ns + "CNPJ") ?? string.Empty;
        var cUF = (string)infNFe.Element(ns + "ide")?.Element(ns + "cUF") ?? string.Empty;
        var totalValue = (string)infNFe.Element(ns + "total")?
                                     .Element(ns + "ICMSTot")?
                                     .Element(ns + "vNF") ?? string.Empty;

        FiscalDocument fiscalDocument = new FiscalDocument
        {
            Id = Guid.NewGuid(),
            Key = chaveNFe.Replace("NFe", ""),
            XmlHash = string.Empty,
            EmissionDate = !string.IsNullOrEmpty(dhEmi) ? DateTime.Parse(dhEmi) : DateTime.MinValue,
            EmitCnpj = emitCnpj,
            RecipientCnpj = destCnpj,
            Uf = cUF,
            TotalValue = decimal.TryParse(totalValue, out var v) ? v : 0m
        };

        var existingDocument = await _fiscalDocumentRepository.GetByKeyAsync(fiscalDocument.Key);
        if (existingDocument != null)
            return new ProcessResultDto { IsSuccess = false, Message = "Documento já processado.", DocumentId = existingDocument.Id };

        await _fiscalDocumentRepository.AddAsync(fiscalDocument);

        var newEvent = new DocumentProcessedEvent(fiscalDocument.Id, fiscalDocument.Key, DateTime.UtcNow);
        await _publishEndpoint.Publish(newEvent);

        return new ProcessResultDto { IsSuccess = true, Message = "Documento incluido com sucesso!.", DocumentId = fiscalDocument.Id }; ;
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
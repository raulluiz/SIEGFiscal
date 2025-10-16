using SIEGFiscal.Domain.Entities;

namespace SIEGFiscal.Domain.Interfaces;

public interface IFiscalDocumentRepository : IGenericRepository<FiscalDocument>
{
    Task<FiscalDocument?> GetByKeyAsync(string key);
    IQueryable<FiscalDocument> Query();
}

using Microsoft.EntityFrameworkCore;
using SIEGFiscal.Domain.Entities;
using SIEGFiscal.Domain.Interfaces;
using SIEGFiscal.Infrastructure.Persistence;

namespace SIEGFiscal.Infrastructure.Repository;

public class FiscalDocumentRepository : GenericRepository<FiscalDocument>, IFiscalDocumentRepository
{
    private readonly AppDbContext _context;
    public FiscalDocumentRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<FiscalDocument?> GetByKeyAsync(string key)
    {
        return await _context.FiscalDocuments.FirstOrDefaultAsync(fd => fd.Key == key);
    }
}

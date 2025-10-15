using Microsoft.EntityFrameworkCore;
using SIEGFiscal.Domain.Entities;
using SIEGFiscal.Domain.Interfaces;

namespace SIEGFiscal.Infrastructure.Repository;

public class FiscalDocumentRepository : GenericRepository<FiscalDocument>, IFiscalDocumentRepository
{
    private readonly DbContext _context;
    public FiscalDocumentRepository(DbContext context) : base(context)
    {
        _context = context;
    }
}

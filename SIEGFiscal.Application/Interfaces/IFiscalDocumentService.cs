using SIEGFiscal.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIEGFiscal.Application.Interfaces;

public interface IFiscalDocumentService
{
    Task<PagedResult<FiscalDocumentDto>> GetPagedAsync(int page, int pageSize, string? cnpj = null, string? uf = null, DateTime? startDate = null, DateTime? endDate = null);
    Task<ProcessResultDto> ProcessXmlAsync(Stream xmlStream);
    Task<IEnumerable<FiscalDocumentDto>> GetAllFiscalDocumentsAsync();
    //Task UpdateAsync(Guid id, UpdateFiscalDocumentDto dto);
    //Task DeleteAsync(Guid id);
}

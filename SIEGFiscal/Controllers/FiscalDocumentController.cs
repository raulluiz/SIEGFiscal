using Microsoft.AspNetCore.Mvc;
using SIEGFiscal.Application.DTOs;
using SIEGFiscal.Application.Services;

namespace SIEGFiscal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FiscalDocumentController(FiscalDocumentService fiscalDocumentService) : ControllerBase
{
    private readonly FiscalDocumentService _fiscalDocumentService = fiscalDocumentService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FiscalDocumentDto>>> GetAll()
    {
        var fiscalDocuments = await _fiscalDocumentService.GetAllFiscalDocumentsAsync();
        return Ok(fiscalDocuments);
    }
}

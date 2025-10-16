using Microsoft.AspNetCore.Mvc;
using SIEGFiscal.Application.DTOs;
using SIEGFiscal.Application.Interfaces;
using SIEGFiscal.Application.Services;

namespace SIEGFiscal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FiscalDocumentController(IFiscalDocumentService fiscalDocumentService) : ControllerBase
{
    private readonly IFiscalDocumentService _fiscalDocumentService = fiscalDocumentService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FiscalDocumentDto>>> GetAll()
    {
        var fiscalDocuments = await _fiscalDocumentService.GetAllFiscalDocumentsAsync();
        return Ok(fiscalDocuments);
    }

    [HttpPost("upload")]
    [ProducesResponseType(typeof(ProcessResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadDocument(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Nenhum arquivo enviado.");
        }

        using (var stream = file.OpenReadStream())
        {
            var result = await _fiscalDocumentService.ProcessXmlAsync(stream);
            // Adicione lógica para retornar Created ou outro status com base no resultado
            return Ok(result);
        }
    }

    [HttpGet("{keyNFe}")]
    public async Task<IActionResult> GetDocumentById(string keyNFe)
    {
        var documents = await _fiscalDocumentService.GetAllFiscalDocumentsAsync();

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? cnpj = null,
            [FromQuery] string? uf = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
    {
        var result = await _fiscalDocumentService.GetPagedAsync(page, pageSize, cnpj, uf, startDate, endDate);
        return Ok(result);
    }

    //[HttpGet]
    //public async Task<IActionResult> List([FromQuery] DocumentListQuery q)
    //{
    //    var paged = await _fiscalDocumentService.ListAsync(q);
    //    return Ok(paged);
    //}

    //[HttpPut("{id:guid}")]
    //public async Task<IActionResult> Update(Guid id, [FromBody] DocumentUpdateDto dto)
    //{
    //    await _fiscalDocumentService.UpdateAsync(id, dto);
    //    return NoContent();
    //}

    //[HttpDelete("{id:guid}")]
    //public async Task<IActionResult> Delete(Guid id)
    //{
    //    await _fiscalDocumentService.DeleteAsync(id);
    //    return NoContent();
    //}
}

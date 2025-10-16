namespace SIEGFiscal.Application.DTOs;

public class ProcessResultDto
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public Guid? DocumentId { get; set; }

}

namespace SIEGFiscal.Application.DTOs;

public class ProcessResultDto
{
    public bool IsDuplicate { get; set; }
    public Guid? Id { get; set; }
    public Guid? ExistingId { get; set; }
}

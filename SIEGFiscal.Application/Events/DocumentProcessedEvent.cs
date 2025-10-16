namespace SIEGFiscal.Application.Events;

public record DocumentProcessedEvent(Guid DocumentId, string AccessKey, DateTime ProcessedAt)
{
}


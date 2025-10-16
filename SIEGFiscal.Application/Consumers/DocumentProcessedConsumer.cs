using MassTransit;
using Microsoft.Extensions.Logging;
using SIEGFiscal.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIEGFiscal.Application.Consumers;

public class DocumentProcessedConsumer : IConsumer<DocumentProcessedEvent>
{
    private readonly ILogger<DocumentProcessedConsumer> _logger;

    public DocumentProcessedConsumer(ILogger<DocumentProcessedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<DocumentProcessedEvent> context)
    {
        _logger.LogInformation("Evento recebido! Documento ID: {DocumentId}, Chave: {AccessKey}",
            context.Message.DocumentId,
            context.Message.AccessKey);

        // Faça algo útil aqui: criar um resumo, logar em outra tabela, etc.
        return Task.CompletedTask;
    }
}

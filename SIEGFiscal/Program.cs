using MassTransit;
using Microsoft.OpenApi.Models;
using SIEGFiscal.Application.Consumers;
using SIEGFiscal.Application.Events;
using SIEGFiscal.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SIEGFiscal API", Version = "v1" });
});

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMassTransit(x =>
{
    // Futuramente, seu consumidor será registrado aqui.
     x.AddConsumer<DocumentProcessedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("documento-processado", e =>
        {
            // Política de retry: 5 tentativas com intervalo exponencial
            e.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(5)));
            e.ConfigureConsumer<DocumentProcessedConsumer>(context);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

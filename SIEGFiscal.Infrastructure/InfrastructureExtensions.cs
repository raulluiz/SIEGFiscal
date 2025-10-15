using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIEGFiscal.Application.Services;
using SIEGFiscal.Domain.Interfaces;
using SIEGFiscal.Infrastructure.Persistence;
using SIEGFiscal.Infrastructure.Repository;

namespace SIEGFiscal.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        // SQL Server
        var cs = config.GetConnectionString("SqlServer") ?? throw new InvalidOperationException("SqlServer connection string not found");
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(cs));

        services.AddScoped<FiscalDocumentService>();

        //Repositories
        services.AddScoped<IFiscalDocumentRepository, FiscalDocumentRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        //// Mongo
        //var mongoConn = config["Mongo:Connection"] ?? "mongodb://localhost:27017";
        //services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoConn));
        //services.AddScoped<IRawDocumentStore, MongoRawDocumentStore>();

        //// RabbitMQ
        //var factory = new ConnectionFactory()
        //{
        //    HostName = config["RabbitMQ:Host"] ?? "localhost",
        //    UserName = config["RabbitMQ:User"] ?? "guest",
        //    Password = config["RabbitMQ:Pass"] ?? "guest",
        //    DispatchConsumersAsync = true
        //};
        //services.AddSingleton(factory.CreateConnection());
        //services.AddScoped<IDocumentPublisher, RabbitPublisher>();
        //services.AddHostedService<DocumentConsumerHostedService>();

        return services;
    }
}

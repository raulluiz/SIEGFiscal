

using Microsoft.EntityFrameworkCore;
using SIEGFiscal.Domain.Entities;

namespace SIEGFiscal.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<FiscalDocument> FiscalDocuments { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FiscalDocument>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.XmlHash).IsUnique();
            b.Property(x => x.EmitCnpj).HasMaxLength(30);
            b.Property(x => x.RecipientCnpj).HasMaxLength(30);
            b.Property(x => x.TotalValue).HasPrecision(18, 2);
            b.ToTable("FiscalDocuments");
        });
    }
}

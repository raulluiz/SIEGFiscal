using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIEGFiscal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FiscalDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XmlHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmitCnpj = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    RecipientCnpj = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Uf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalDocuments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalDocuments_XmlHash",
                table: "FiscalDocuments",
                column: "XmlHash",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FiscalDocuments");
        }
    }
}

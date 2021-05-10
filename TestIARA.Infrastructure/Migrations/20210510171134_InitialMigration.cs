using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace TestIARA.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TCotacao",
                columns: table => new
                {
                    NumeroCotacao = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    State = table.Column<bool>(nullable: false),
                    CNPJComprador = table.Column<string>(maxLength: 14, nullable: false),
                    CNPJFornecedor = table.Column<string>(maxLength: 14, nullable: false),
                    DataCotacao = table.Column<string>(maxLength: 6, nullable: false),
                    DataEntregaCotacao = table.Column<string>(maxLength: 6, nullable: false),
                    CEP = table.Column<string>(maxLength: 8, nullable: false),
                    Logradouro = table.Column<string>(nullable: true),
                    Complemento = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    UF = table.Column<string>(maxLength: 2, nullable: true),
                    Observacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCotacao", x => x.NumeroCotacao);
                });

            migrationBuilder.CreateTable(
                name: "TCotacaoItem",
                columns: table => new
                {
                    NumeroCotacaoItem = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    State = table.Column<bool>(nullable: false),
                    NumeroCotacao = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    NumeroItem = table.Column<int>(nullable: false),
                    Preco = table.Column<int>(nullable: true, defaultValue: 0),
                    Quantidade = table.Column<int>(nullable: false),
                    Marca = table.Column<string>(nullable: true),
                    Unidade = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCotacaoItem", x => x.NumeroCotacaoItem);
                    table.ForeignKey(
                        name: "FK_TCotacaoItem_TCotacao_NumeroCotacao",
                        column: x => x.NumeroCotacao,
                        principalTable: "TCotacao",
                        principalColumn: "NumeroCotacao");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TCotacaoItem_NumeroCotacao",
                table: "TCotacaoItem",
                column: "NumeroCotacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TCotacaoItem");

            migrationBuilder.DropTable(
                name: "TCotacao");
        }
    }
}

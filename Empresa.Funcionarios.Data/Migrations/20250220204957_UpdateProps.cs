using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Empresa.Funcionarios.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Telefones",
                table: "Funcionarios",
                newName: "Telefone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "Funcionarios",
                newName: "Telefones");
        }
    }
}

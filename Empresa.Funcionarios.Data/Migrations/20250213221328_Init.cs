//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace Empresa.Funcionarios.Data.Migrations
//{
//    /// <inheritdoc />
//    public partial class Init : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.CreateTable(
//                name: "Funcionarios",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uuid", nullable: false),
//                    Nome = table.Column<string>(type: "text", nullable: false),
//                    Sobrenome = table.Column<string>(type: "text", nullable: false),
//                    Email = table.Column<string>(type: "text", nullable: false),
//                    NumeroDocumento = table.Column<string>(type: "text", nullable: false),
//                    Telefones = table.Column<List<string>>(type: "text[]", nullable: false),
//                    GestorId = table.Column<Guid>(type: "uuid", nullable: true),
//                    DataNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
//                    SenhaHash = table.Column<string>(type: "text", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
//                });

//            migrationBuilder.CreateIndex(
//                name: "IX_Funcionarios_NumeroDocumento",
//                table: "Funcionarios",
//                column: "NumeroDocumento",
//                unique: true);
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "Funcionarios");
//        }
//    }
//}

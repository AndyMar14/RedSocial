using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RedSocial.Persistence.Migrations
{
    public partial class Amigos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "amigos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdAmigo = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amigos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_amigos_usuarios_IdAmigo",
                        column: x => x.IdAmigo,
                        principalTable: "usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_amigos_IdAmigo",
                table: "amigos",
                column: "IdAmigo");
        }
    }
}

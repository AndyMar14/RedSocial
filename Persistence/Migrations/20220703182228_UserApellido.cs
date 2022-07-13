using Microsoft.EntityFrameworkCore.Migrations;

namespace RedSocial.Persistence.Migrations
{
    public partial class UserApellido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "usuarios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "usuarios",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

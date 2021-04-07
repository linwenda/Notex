using Microsoft.EntityFrameworkCore.Migrations;

namespace Funzone.Services.Albums.Infrastructure.DataAccess.Migrations
{
    public partial class UpdateDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Albums",
                table: "Pictures",
                type: "varchar(512)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(512)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Albums",
                table: "Pictures",
                type: "varchar(512)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(512)",
                oldNullable: true);
        }
    }
}

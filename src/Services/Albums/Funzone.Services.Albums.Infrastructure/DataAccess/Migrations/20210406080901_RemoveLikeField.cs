using Microsoft.EntityFrameworkCore.Migrations;

namespace Funzone.Services.Albums.Infrastructure.DataAccess.Migrations
{
    public partial class RemoveLikeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Like",
                schema: "Albums",
                table: "Pictures");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Like",
                schema: "Albums",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

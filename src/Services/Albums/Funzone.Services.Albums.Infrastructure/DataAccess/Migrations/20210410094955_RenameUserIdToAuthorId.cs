using Microsoft.EntityFrameworkCore.Migrations;

namespace Funzone.Services.Albums.Infrastructure.DataAccess.Migrations
{
    public partial class RenameUserIdToAuthorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "Albums",
                table: "Pictures",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "Albums",
                table: "Albums",
                newName: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_AlbumId",
                schema: "Albums",
                table: "Pictures",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Albums_AlbumId",
                schema: "Albums",
                table: "Pictures",
                column: "AlbumId",
                principalSchema: "Albums",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Albums_AlbumId",
                schema: "Albums",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_AlbumId",
                schema: "Albums",
                table: "Pictures");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                schema: "Albums",
                table: "Pictures",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                schema: "Albums",
                table: "Albums",
                newName: "UserId");
        }
    }
}

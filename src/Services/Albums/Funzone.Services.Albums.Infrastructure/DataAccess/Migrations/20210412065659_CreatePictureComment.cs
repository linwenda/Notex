using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Funzone.Services.Albums.Infrastructure.DataAccess.Migrations
{
    public partial class CreatePictureComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PictureComments",
                schema: "Albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PictureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "varchar(1024)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PictureComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PictureComments_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalSchema: "Albums",
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PictureComments_PictureId",
                schema: "Albums",
                table: "PictureComments",
                column: "PictureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PictureComments",
                schema: "Albums");
        }
    }
}

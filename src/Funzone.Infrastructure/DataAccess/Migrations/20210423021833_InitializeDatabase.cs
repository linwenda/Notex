using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Funzone.Infrastructure.DataAccess.Migrations
{
    public partial class InitializeDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "varchar(255)", nullable: false),
                    PasswordSalt = table.Column<string>(type: "varchar(512)", nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(512)", nullable: false),
                    EmailAddress = table.Column<string>(type: "varchar(255)", nullable: true),
                    NickName = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", nullable: true),
                    AvatarUrl = table.Column<string>(type: "varchar(512)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZoneUsers",
                columns: table => new
                {
                    ZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "varchar(20)", nullable: true),
                    JoinedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsLeave = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneUsers", x => new { x.ZoneId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "ZoneRules",
                columns: table => new
                {
                    ZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "varchar(128)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneRules", x => new { x.ZoneId, x.Title });
                    table.ForeignKey(
                        name: "FK_ZoneRules_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ZoneRules");

            migrationBuilder.DropTable(
                name: "ZoneUsers");

            migrationBuilder.DropTable(
                name: "Zones");
        }
    }
}

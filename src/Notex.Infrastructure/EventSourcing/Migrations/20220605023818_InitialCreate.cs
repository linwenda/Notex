using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notex.Infrastructure.EventSourcing.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__events",
                columns: table => new
                {
                    sourced_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    version = table.Column<int>(type: "int", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    payload = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk___events", x => new { x.sourced_id, x.version });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__mementos",
                columns: table => new
                {
                    sourced_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    version = table.Column<int>(type: "int", nullable: false),
                    payload = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk___mementos", x => new { x.sourced_id, x.version });
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__events");

            migrationBuilder.DropTable(
                name: "__mementos");
        }
    }
}

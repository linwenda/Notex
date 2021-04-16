using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Funzone.Infrastructure.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

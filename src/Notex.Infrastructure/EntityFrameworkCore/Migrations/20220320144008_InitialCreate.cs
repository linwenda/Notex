using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notex.Infrastructure.EntityFrameworkCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    entity_type = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    entity_id = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    text = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    replied_comment_id = table.Column<Guid>(type: "uuid", nullable: true),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modification_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    aggregate_id = table.Column<Guid>(type: "uuid", nullable: false),
                    aggregate_version = table.Column<int>(type: "integer", nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    payload = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    type = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_events", x => new { x.aggregate_id, x.aggregate_version });
                });

            migrationBuilder.CreateTable(
                name: "mementos",
                columns: table => new
                {
                    aggregate_id = table.Column<Guid>(type: "uuid", nullable: false),
                    aggregate_version = table.Column<int>(type: "integer", nullable: false),
                    payload = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    type = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mementos", x => new { x.aggregate_id, x.aggregate_version });
                });

            migrationBuilder.CreateTable(
                name: "merge_requests",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    source_note_id = table.Column<Guid>(type: "uuid", nullable: false),
                    destination_note_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reviewer_id = table.Column<Guid>(type: "uuid", nullable: true),
                    review_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_merge_requests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "note_histories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    note_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    clone_form_id = table.Column<Guid>(type: "uuid", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_note_histories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    space_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    visibility = table.Column<int>(type: "integer", nullable: false),
                    clone_form_id = table.Column<Guid>(type: "uuid", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false),
                    read_count = table.Column<int>(type: "integer", nullable: false),
                    tags = table.Column<string[]>(type: "text[]", nullable: true),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "spaces",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    background_image = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    visibility = table.Column<int>(type: "integer", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modification_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_spaces", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    usage_count = table.Column<int>(type: "integer", nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tags", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    password = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    email = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    bio = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    avatar = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    roles = table.Column<string[]>(type: "text[]", nullable: true),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "mementos");

            migrationBuilder.DropTable(
                name: "merge_requests");

            migrationBuilder.DropTable(
                name: "note_histories");

            migrationBuilder.DropTable(
                name: "notes");

            migrationBuilder.DropTable(
                name: "spaces");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

namespace sl.infrastructure.migrations.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "log",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    message = table.Column<string>(nullable: true),
                    level = table.Column<string>(nullable: true),
                    system_id = table.Column<string>(nullable: true),
                    stack_trace = table.Column<string>(nullable: true),
                    registered_at = table.Column<DateTime>(nullable: false),
                    labels = table.Column<string[]>(type: "text[]", nullable: true),
                    s_vector = table.Column<NpgsqlTsVector>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_log_id", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_log_s_vector",
                table: "log",
                column: "s_vector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "log");
        }
    }
}

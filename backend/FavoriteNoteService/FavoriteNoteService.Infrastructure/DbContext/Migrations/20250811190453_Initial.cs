using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FavoriteNoteService.Infrastructure.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "favorite_note_service");

            migrationBuilder.CreateTable(
                name: "favorite_notes",
                schema: "favorite_note_service",
                columns: table => new
                {
                    note_id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_favorite_notes", x => new { x.note_id, x.account_id });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "favorite_notes",
                schema: "favorite_note_service");
        }
    }
}

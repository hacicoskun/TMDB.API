using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HC.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class note_score_properties_movie_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "note",
                table: "Movies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "score",
                table: "Movies",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "note",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "score",
                table: "Movies");
        }
    }
}

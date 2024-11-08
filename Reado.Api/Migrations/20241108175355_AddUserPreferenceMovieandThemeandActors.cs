using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reado.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPreferenceMovieandThemeandActors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreferredActors",
                table: "UserPreferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "PreferredMovies",
                table: "UserPreferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "PreferredThemes",
                table: "UserPreferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredActors",
                table: "UserPreferences");

            migrationBuilder.DropColumn(
                name: "PreferredMovies",
                table: "UserPreferences");

            migrationBuilder.DropColumn(
                name: "PreferredThemes",
                table: "UserPreferences");
        }
    }
}

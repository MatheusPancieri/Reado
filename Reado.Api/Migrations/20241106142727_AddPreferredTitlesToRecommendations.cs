using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reado.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPreferredTitlesToRecommendations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreferredTitles",
                table: "Recommendations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredTitles",
                table: "Recommendations");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reado.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPreferenceAndMovieListToRecommendation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MovieList",
                table: "Recommendations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<long>(
                name: "UserPreferenceId",
                table: "Recommendations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "UserPreferenceId1",
                table: "Recommendations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_UserPreferenceId1",
                table: "Recommendations",
                column: "UserPreferenceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_UserPreferences_UserPreferenceId1",
                table: "Recommendations",
                column: "UserPreferenceId1",
                principalTable: "UserPreferences",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_UserPreferences_UserPreferenceId1",
                table: "Recommendations");

            migrationBuilder.DropIndex(
                name: "IX_Recommendations_UserPreferenceId1",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "MovieList",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "UserPreferenceId",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "UserPreferenceId1",
                table: "Recommendations");
        }
    }
}

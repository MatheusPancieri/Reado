﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reado.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileName",
                table: "UserPreferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileName",
                table: "UserPreferences");
        }
    }
}

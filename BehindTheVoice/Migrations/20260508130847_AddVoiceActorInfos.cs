using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BehindTheVoice.Migrations
{
    /// <inheritdoc />
    public partial class AddVoiceActorInfos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BirthPlace",
                table: "VoiceActors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "VoiceActors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthPlace",
                table: "VoiceActors");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "VoiceActors");
        }
    }
}

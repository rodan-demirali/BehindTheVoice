using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BehindTheVoice.Migrations
{
    /// <inheritdoc />
    public partial class AddVoiceActorDesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "VoiceActors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "VoiceActors");
        }
    }
}

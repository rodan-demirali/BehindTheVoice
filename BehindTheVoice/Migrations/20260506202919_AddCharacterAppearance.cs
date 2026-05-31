using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BehindTheVoice.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterAppearance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CharacterAppearanceUrl",
                table: "VoiceCasts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharacterAppearanceUrl",
                table: "VoiceCasts");
        }
    }
}

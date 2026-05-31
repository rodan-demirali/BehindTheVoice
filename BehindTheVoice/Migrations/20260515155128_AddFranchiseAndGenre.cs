using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BehindTheVoice.Migrations
{
    /// <inheritdoc />
    public partial class AddFranchiseAndGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Productions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FranchiseId",
                table: "Productions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginalLanguage",
                table: "Productions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Productions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Runtime",
                table: "Productions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Studio",
                table: "Productions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Franchises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenreProduction",
                columns: table => new
                {
                    GenresId = table.Column<int>(type: "int", nullable: false),
                    ProductionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreProduction", x => new { x.GenresId, x.ProductionsId });
                    table.ForeignKey(
                        name: "FK_GenreProduction_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreProduction_Productions_ProductionsId",
                        column: x => x.ProductionsId,
                        principalTable: "Productions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productions_FranchiseId",
                table: "Productions",
                column: "FranchiseId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreProduction_ProductionsId",
                table: "GenreProduction",
                column: "ProductionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Franchises_FranchiseId",
                table: "Productions",
                column: "FranchiseId",
                principalTable: "Franchises",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Franchises_FranchiseId",
                table: "Productions");

            migrationBuilder.DropTable(
                name: "Franchises");

            migrationBuilder.DropTable(
                name: "GenreProduction");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Productions_FranchiseId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "FranchiseId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "OriginalLanguage",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "Runtime",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "Studio",
                table: "Productions");
        }
    }
}

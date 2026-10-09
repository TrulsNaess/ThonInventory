using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThonInventory.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsMissing = table.Column<bool>(type: "boolean", nullable: false),
                    MissingSinceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notes = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    IsOrdered = table.Column<bool>(type: "boolean", nullable: false),
                    OrderedAtDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "BRUS" },
                    { 2, 2, "SNACKS" },
                    { 3, 3, "ALKOHOL" },
                    { 4, 4, "KIOSK" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CategoryId", "DisplayOrder", "IsMissing", "IsOrdered", "MissingSinceDate", "Name", "Notes", "OrderedAtDate", "Price" },
                values: new object[,]
                {
                    { 1, 1, 1, false, false, null, "Coca-Cola", null, null, 0.0 },
                    { 2, 1, 2, false, false, null, "Coca-Cola Zero", null, null, 0.0 },
                    { 3, 1, 3, false, false, null, "Fanta", null, null, 0.0 },
                    { 4, 1, 4, false, false, null, "Sprite", null, null, 0.0 },
                    { 5, 1, 5, false, false, null, "Eplemost", null, null, 0.0 },
                    { 6, 1, 6, false, false, null, "Mineralvann", null, null, 0.0 },
                    { 7, 1, 7, false, false, null, "Mineralvann med kullsyre", null, null, 0.0 },
                    { 8, 1, 8, false, false, null, "Red Bull", null, null, 0.0 },
                    { 9, 1, 9, false, false, null, "Monster", null, null, 0.0 },
                    { 10, 2, 1, false, false, null, "Stratos", null, null, 0.0 },
                    { 11, 2, 2, false, false, null, "Melkesjokolade", null, null, 0.0 },
                    { 12, 2, 3, false, false, null, "Sørlandschips", null, null, 0.0 },
                    { 13, 2, 4, false, false, null, "Sørlandschips (små poser)", null, null, 0.0 },
                    { 14, 2, 5, false, false, null, "Chillinøtter", null, null, 0.0 },
                    { 15, 2, 6, false, false, null, "Peanøtter", null, null, 0.0 },
                    { 16, 2, 7, false, false, null, "Polly Nøtter", null, null, 0.0 },
                    { 17, 2, 8, false, false, null, "Extra tyggis", null, null, 0.0 },
                    { 18, 2, 9, false, false, null, "Små sulten", null, null, 0.0 },
                    { 19, 2, 10, false, false, null, "Is", null, null, 0.0 },
                    { 20, 3, 1, false, false, null, "Carlsberg", null, null, 0.0 },
                    { 21, 3, 2, false, false, null, "Alkoholfri øl", null, null, 0.0 },
                    { 22, 3, 3, false, false, null, "Gin-Tonic i boks", null, null, 0.0 },
                    { 23, 3, 4, false, false, null, "Bulmers Cider", null, null, 0.0 },
                    { 24, 3, 5, false, false, null, "Prosecco 200ml (små flaske)", null, null, 0.0 },
                    { 25, 3, 6, false, false, null, "Prosecco 750ml", null, null, 0.0 },
                    { 26, 3, 7, false, false, null, "Rødvin 37.5cl (små flaske)", null, null, 0.0 },
                    { 27, 3, 8, false, false, null, "Rødvin 750ml", null, null, 0.0 },
                    { 28, 3, 9, false, false, null, "Hvitvin 375ml (små flaske)", null, null, 0.0 },
                    { 29, 3, 10, false, false, null, "Hvitvin 750ml", null, null, 0.0 },
                    { 30, 3, 11, false, false, null, "Jameson", null, null, 0.0 },
                    { 31, 3, 12, false, false, null, "Rum (Captain Morgan)", null, null, 0.0 },
                    { 32, 3, 13, false, false, null, "Cognac", null, null, 0.0 },
                    { 33, 3, 14, false, false, null, "Gin", null, null, 0.0 },
                    { 34, 3, 15, false, false, null, "Pink Gin", null, null, 0.0 },
                    { 35, 3, 16, false, false, null, "Tonic Water", null, null, 0.0 },
                    { 36, 4, 1, false, false, null, "Tannbørste", null, null, 0.0 },
                    { 37, 4, 2, false, false, null, "Tannkrem", null, null, 0.0 },
                    { 38, 4, 3, false, false, null, "OB rosa", null, null, 0.0 },
                    { 39, 4, 4, false, false, null, "OB blå", null, null, 0.0 },
                    { 40, 4, 5, false, false, null, "Bind", null, null, 0.0 },
                    { 41, 4, 6, false, false, null, "Kortstokk", null, null, 0.0 },
                    { 42, 4, 7, false, false, null, "Yatzy", null, null, 0.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId_Name",
                table: "Items",
                columns: new[] { "CategoryId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

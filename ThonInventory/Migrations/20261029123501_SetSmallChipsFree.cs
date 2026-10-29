using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThonInventory.Migrations
{
    /// <inheritdoc />
    public partial class SetSmallChipsFree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 13,
                column: "Price",
                value: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 13,
                column: "Price",
                value: 59m);
        }
    }
}

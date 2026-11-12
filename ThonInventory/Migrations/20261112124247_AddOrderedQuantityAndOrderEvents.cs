using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThonInventory.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderedQuantityAndOrderEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderedQuantity",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "OrderedQuantity",
                value: 12);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                column: "OrderedQuantity",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 11,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 12,
                column: "OrderedQuantity",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 13,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 14,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 15,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 16,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 17,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 18,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 20,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 21,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 22,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 23,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 24,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 25,
                column: "OrderedQuantity",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 26,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 27,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 28,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 29,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 30,
                column: "OrderedQuantity",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 31,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 32,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 33,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 34,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 35,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 36,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 37,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 38,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 39,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 40,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 41,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 42,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 43,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 44,
                column: "OrderedQuantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 45,
                column: "OrderedQuantity",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderedQuantity",
                table: "Items");
        }
    }
}

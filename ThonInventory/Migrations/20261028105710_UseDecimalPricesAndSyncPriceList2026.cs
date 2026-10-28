using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThonInventory.Migrations
{
    /// <inheritdoc />
    public partial class UseDecimalPricesAndSyncPriceList2026 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "SalesLogEntries",
                type: "numeric(10,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Items",
                type: "numeric(10,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 58m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 58m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 58m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                column: "Price",
                value: 58m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                column: "Price",
                value: 89m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                column: "Price",
                value: 58m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                column: "Price",
                value: 58m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                column: "Price",
                value: 39m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                column: "Price",
                value: 68m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                column: "Price",
                value: 59m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 11,
                column: "Price",
                value: 59m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 12,
                column: "Price",
                value: 59m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 13,
                column: "Price",
                value: 59m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 14,
                column: "Price",
                value: 42m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 15,
                column: "Price",
                value: 42m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 16,
                column: "Price",
                value: 42m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 17,
                column: "Price",
                value: 32m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 18,
                column: "Price",
                value: 38m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 19,
                column: "Price",
                value: 45m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 20,
                column: "Price",
                value: 115m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 21,
                column: "Price",
                value: 79m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 22,
                column: "Price",
                value: 115m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 23,
                column: "Price",
                value: 159m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 24,
                column: "Price",
                value: 297m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 25,
                column: "Price",
                value: 624m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 26,
                column: "Price",
                value: 315m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 27,
                column: "Price",
                value: 572m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 28,
                column: "Price",
                value: 315m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 29,
                column: "Price",
                value: 572m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 30,
                column: "Price",
                value: 126m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 31,
                column: "Price",
                value: 126m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 32,
                column: "Price",
                value: 126m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 33,
                column: "Price",
                value: 126m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 34,
                column: "Price",
                value: 126m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 35,
                column: "Price",
                value: 39m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 36,
                column: "Price",
                value: 47m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 37,
                column: "Price",
                value: 42m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 38,
                column: "Price",
                value: 37m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 39,
                column: "Price",
                value: 37m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 40,
                column: "Price",
                value: 42m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 41,
                column: "Price",
                value: 49m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 42,
                column: "Price",
                value: 79m);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 1,
                column: "UnitPrice",
                value: 58m);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 2,
                column: "UnitPrice",
                value: 59m);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 3,
                column: "UnitPrice",
                value: 115m);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 4,
                column: "UnitPrice",
                value: 297m);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 5,
                column: "UnitPrice",
                value: 47m);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 6,
                column: "UnitPrice",
                value: 572m);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 7,
                column: "UnitPrice",
                value: 58m);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 8,
                column: "UnitPrice",
                value: 49m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "UnitPrice",
                table: "SalesLogEntries",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Items",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 39.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 39.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 39.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                column: "Price",
                value: 39.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                column: "Price",
                value: 42.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                column: "Price",
                value: 36.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                column: "Price",
                value: 36.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                column: "Price",
                value: 46.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                column: "Price",
                value: 48.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                column: "Price",
                value: 32.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 11,
                column: "Price",
                value: 34.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 12,
                column: "Price",
                value: 42.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 13,
                column: "Price",
                value: 28.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 14,
                column: "Price",
                value: 35.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 15,
                column: "Price",
                value: 35.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 16,
                column: "Price",
                value: 37.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 17,
                column: "Price",
                value: 24.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 18,
                column: "Price",
                value: 33.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 19,
                column: "Price",
                value: 45.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 20,
                column: "Price",
                value: 75.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 21,
                column: "Price",
                value: 62.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 22,
                column: "Price",
                value: 88.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 23,
                column: "Price",
                value: 86.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 24,
                column: "Price",
                value: 109.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 25,
                column: "Price",
                value: 359.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 26,
                column: "Price",
                value: 139.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 27,
                column: "Price",
                value: 399.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 28,
                column: "Price",
                value: 135.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 29,
                column: "Price",
                value: 389.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 30,
                column: "Price",
                value: 920.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 31,
                column: "Price",
                value: 890.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 32,
                column: "Price",
                value: 1140.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 33,
                column: "Price",
                value: 870.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 34,
                column: "Price",
                value: 890.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 35,
                column: "Price",
                value: 39.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 36,
                column: "Price",
                value: 45.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 37,
                column: "Price",
                value: 49.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 38,
                column: "Price",
                value: 62.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 39,
                column: "Price",
                value: 62.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 40,
                column: "Price",
                value: 64.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 41,
                column: "Price",
                value: 54.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 42,
                column: "Price",
                value: 59.0);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 1,
                column: "UnitPrice",
                value: 39.0);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 2,
                column: "UnitPrice",
                value: 32.0);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 3,
                column: "UnitPrice",
                value: 75.0);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 4,
                column: "UnitPrice",
                value: 109.0);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 5,
                column: "UnitPrice",
                value: 45.0);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 6,
                column: "UnitPrice",
                value: 399.0);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 7,
                column: "UnitPrice",
                value: 39.0);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 8,
                column: "UnitPrice",
                value: 54.0);
        }
    }
}

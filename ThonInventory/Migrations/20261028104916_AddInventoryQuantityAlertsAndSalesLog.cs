using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThonInventory.Migrations
{
    /// <inheritdoc />
    public partial class AddInventoryQuantityAlertsAndSalesLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectedDeliveryDate",
                table: "Items",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SalesLogEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    QuantitySold = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<double>(type: "double precision", nullable: false),
                    SoldAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesLogEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesLogEntries_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 39.0, 48 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 39.0, 44 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExpectedDeliveryDate", "IsOrdered", "OrderedAtDate", "Price", "Quantity" },
                values: new object[] { new DateTime(2026, 10, 30, 11, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2026, 10, 27, 9, 0, 0, 0, DateTimeKind.Utc), 39.0, 26 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 39.0, 31 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ExpectedDeliveryDate", "IsOrdered", "OrderedAtDate", "Price", "Quantity" },
                values: new object[] { new DateTime(2026, 10, 31, 12, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2026, 10, 27, 9, 5, 0, 0, DateTimeKind.Utc), 42.0, 14 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 36.0, 23 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 36.0, 19 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ExpectedDeliveryDate", "IsMissing", "MissingSinceDate", "Notes", "Price", "Quantity" },
                values: new object[] { null, true, new DateTime(2026, 10, 26, 8, 0, 0, 0, DateTimeKind.Utc), "Hylle tom i minibarrom", 46.0, 12 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ExpectedDeliveryDate", "IsMissing", "MissingSinceDate", "Price", "Quantity" },
                values: new object[] { null, true, new DateTime(2026, 10, 26, 8, 10, 0, 0, DateTimeKind.Utc), 48.0, 10 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 32.0, 51 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 34.0, 39 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ExpectedDeliveryDate", "IsOrdered", "OrderedAtDate", "Price", "Quantity" },
                values: new object[] { new DateTime(2026, 10, 29, 10, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2026, 10, 25, 13, 0, 0, 0, DateTimeKind.Utc), 42.0, 22 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 28.0, 28 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 35.0, 18 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 35.0, 16 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 37.0, 27 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 24.0, 60 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 33.0, 18 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 45.0, 40 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 75.0, 47 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 62.0, 36 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 88.0, 25 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 86.0, 17 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 109.0, 34 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "ExpectedDeliveryDate", "IsOrdered", "OrderedAtDate", "Price", "Quantity" },
                values: new object[] { new DateTime(2026, 10, 30, 14, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2026, 10, 24, 14, 30, 0, 0, DateTimeKind.Utc), 359.0, 12 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 139.0, 20 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 399.0, 15 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 135.0, 16 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 389.0, 11 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "ExpectedDeliveryDate", "IsOrdered", "OrderedAtDate", "Price", "Quantity" },
                values: new object[] { new DateTime(2026, 10, 31, 12, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2026, 10, 22, 15, 0, 0, 0, DateTimeKind.Utc), 920.0, 8 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 890.0, 9 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 1140.0, 4 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 870.0, 7 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 890.0, 6 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 39.0, 33 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 45.0, 14 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 49.0, 18 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 62.0, 22 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 62.0, 20 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 64.0, 13 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 54.0, 9 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "ExpectedDeliveryDate", "Price", "Quantity" },
                values: new object[] { null, 59.0, 7 });

            migrationBuilder.InsertData(
                table: "SalesLogEntries",
                columns: new[] { "Id", "ItemId", "QuantitySold", "SoldAt", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 3, new DateTime(2026, 10, 28, 8, 15, 0, 0, DateTimeKind.Utc), 39.0 },
                    { 2, 10, 2, new DateTime(2026, 10, 28, 10, 45, 0, 0, DateTimeKind.Utc), 32.0 },
                    { 3, 20, 4, new DateTime(2026, 10, 27, 20, 30, 0, 0, DateTimeKind.Utc), 75.0 },
                    { 4, 24, 1, new DateTime(2026, 10, 27, 19, 20, 0, 0, DateTimeKind.Utc), 109.0 },
                    { 5, 36, 1, new DateTime(2026, 10, 26, 22, 11, 0, 0, DateTimeKind.Utc), 45.0 },
                    { 6, 27, 2, new DateTime(2026, 10, 21, 21, 5, 0, 0, DateTimeKind.Utc), 399.0 },
                    { 7, 3, 5, new DateTime(2026, 10, 19, 15, 40, 0, 0, DateTimeKind.Utc), 39.0 },
                    { 8, 41, 1, new DateTime(2026, 10, 13, 17, 32, 0, 0, DateTimeKind.Utc), 54.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesLogEntries_ItemId",
                table: "SalesLogEntries",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesLogEntries_SoldAt",
                table: "SalesLogEntries",
                column: "SoldAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesLogEntries");

            migrationBuilder.DropColumn(
                name: "ExpectedDeliveryDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Items");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IsOrdered", "OrderedAtDate", "Price" },
                values: new object[] { false, null, 0.0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IsOrdered", "OrderedAtDate", "Price" },
                values: new object[] { false, null, 0.0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "IsMissing", "MissingSinceDate", "Notes", "Price" },
                values: new object[] { false, null, null, 0.0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "IsMissing", "MissingSinceDate", "Price" },
                values: new object[] { false, null, 0.0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 11,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "IsOrdered", "OrderedAtDate", "Price" },
                values: new object[] { false, null, 0.0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 13,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 14,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 15,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 16,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 17,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 18,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 19,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 20,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 21,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 22,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 23,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 24,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "IsOrdered", "OrderedAtDate", "Price" },
                values: new object[] { false, null, 0.0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 26,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 27,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 28,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 29,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "IsOrdered", "OrderedAtDate", "Price" },
                values: new object[] { false, null, 0.0 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 31,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 32,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 33,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 34,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 35,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 36,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 37,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 38,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 39,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 40,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 41,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 42,
                column: "Price",
                value: 0.0);
        }
    }
}

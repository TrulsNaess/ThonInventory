using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThonInventory.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionTypesRestockShrinkageAndDemoCheckout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SoldAt",
                table: "SalesLogEntries",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "QuantitySold",
                table: "SalesLogEntries",
                newName: "TransactionType");

            migrationBuilder.RenameIndex(
                name: "IX_SalesLogEntries_SoldAt",
                table: "SalesLogEntries",
                newName: "IX_SalesLogEntries_CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "SalesLogEntries",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "SalesLogEntries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "IsMissing", "MissingSinceDate", "Notes" },
                values: new object[] { false, null, null });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "IsMissing", "MissingSinceDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Note", "Quantity", "TransactionType" },
                values: new object[] { null, 3, 0 });

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Note", "Quantity", "TransactionType" },
                values: new object[] { null, 2, 0 });

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Note", "Quantity", "TransactionType" },
                values: new object[] { null, 4, 0 });

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Note", "Quantity", "TransactionType" },
                values: new object[] { null, 1, 0 });

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Note", "Quantity", "TransactionType" },
                values: new object[] { null, 1, 0 });

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Note", "Quantity", "TransactionType" },
                values: new object[] { null, 2, 0 });

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Note", "Quantity", "TransactionType" },
                values: new object[] { null, 5, 0 });

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Note", "Quantity", "TransactionType" },
                values: new object[] { null, 1, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "SalesLogEntries");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "SalesLogEntries");

            migrationBuilder.RenameColumn(
                name: "TransactionType",
                table: "SalesLogEntries",
                newName: "QuantitySold");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "SalesLogEntries",
                newName: "SoldAt");

            migrationBuilder.RenameIndex(
                name: "IX_SalesLogEntries_CreatedAt",
                table: "SalesLogEntries",
                newName: "IX_SalesLogEntries_SoldAt");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "IsMissing", "MissingSinceDate", "Notes" },
                values: new object[] { true, new DateTime(2026, 10, 26, 8, 0, 0, 0, DateTimeKind.Utc), "Hylle tom i minibarrom" });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "IsMissing", "MissingSinceDate" },
                values: new object[] { true, new DateTime(2026, 10, 26, 8, 10, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 1,
                column: "QuantitySold",
                value: 3);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 2,
                column: "QuantitySold",
                value: 2);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 3,
                column: "QuantitySold",
                value: 4);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 4,
                column: "QuantitySold",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 5,
                column: "QuantitySold",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 6,
                column: "QuantitySold",
                value: 2);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 7,
                column: "QuantitySold",
                value: 5);

            migrationBuilder.UpdateData(
                table: "SalesLogEntries",
                keyColumn: "Id",
                keyValue: 8,
                column: "QuantitySold",
                value: 1);
        }
    }
}

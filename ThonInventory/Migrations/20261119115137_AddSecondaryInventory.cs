using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThonInventory.Migrations
{
    /// <inheritdoc />
    public partial class AddSecondaryInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 5, 5, "RESEPSJON" },
                    { 6, 6, "BACKOFFICE" },
                    { 7, 7, "MAINTENANCE" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CategoryId", "DisplayOrder", "ExpectedDeliveryDate", "IsMissing", "IsOrdered", "MissingSinceDate", "Name", "Notes", "OrderedAtDate", "OrderedQuantity", "Price", "Quantity" },
                values: new object[,]
                {
                    { 100, 5, 1, null, false, false, null, "Varmtvannskokere", null, null, 0, 0m, 3 },
                    { 101, 5, 2, null, false, false, null, "Grønn te", null, null, 0, 0m, 48 },
                    { 102, 5, 3, null, false, false, null, "Rød te", null, null, 0, 0m, 42 },
                    { 103, 5, 4, null, false, false, null, "Gul te", null, null, 0, 0m, 28 },
                    { 104, 5, 5, null, false, false, null, "Oransje te", null, null, 0, 0m, 25 },
                    { 105, 5, 6, null, false, false, null, "Kakao", null, null, 0, 0m, 36 },
                    { 106, 5, 7, null, false, false, null, "Kaffe", null, null, 0, 0m, 60 },
                    { 107, 5, 8, null, false, false, null, "Sørlandschips", null, null, 0, 0m, 20 },
                    { 108, 5, 9, null, false, false, null, "Tørkepapir", null, null, 0, 0m, 12 },
                    { 109, 5, 10, null, false, false, null, "Hårfønere", null, null, 0, 0m, 7 },
                    { 110, 5, 11, null, false, false, null, "Vifter", null, null, 0, 0m, 5 },
                    { 111, 5, 12, null, false, false, null, "Kopper", null, null, 0, 0m, 80 },
                    { 112, 5, 13, null, false, false, null, "Ovner", null, null, 0, 0m, 2 },
                    { 113, 5, 14, null, false, false, null, "Poser", null, null, 0, 0m, 150 },
                    { 114, 5, 15, null, false, false, null, "Dopapir", null, null, 0, 0m, 30 },
                    { 115, 5, 16, null, false, false, null, "Ark", null, null, 0, 0m, 22 },
                    { 116, 5, 17, null, false, false, null, "Skjøteledninger", null, null, 0, 0m, 6 },
                    { 117, 5, 18, null, false, false, null, "Notatark", null, null, 0, 0m, 35 },
                    { 118, 5, 19, null, false, false, null, "Kulepenner", null, null, 0, 0m, 70 },
                    { 120, 6, 1, null, false, false, null, "Håndkler (store)", null, null, 0, 0m, 45 },
                    { 121, 6, 2, null, false, false, null, "Håndkler (små)", null, null, 0, 0m, 70 },
                    { 122, 6, 3, null, false, false, null, "Ørepropper", null, null, 0, 0m, 120 },
                    { 123, 6, 4, null, false, false, null, "Såpe", null, null, 0, 0m, 90 },
                    { 124, 6, 5, null, false, false, null, "Puter", null, null, 0, 0m, 16 },
                    { 125, 6, 6, null, false, false, null, "Dopapir", null, null, 0, 0m, 64 },
                    { 126, 6, 7, null, false, false, null, "Klesruller", null, null, 0, 0m, 18 },
                    { 127, 6, 8, null, false, false, null, "Tørkepapir", null, null, 0, 0m, 25 },
                    { 128, 6, 9, null, false, false, null, "Flipflopper / sandaler", null, null, 0, 0m, 22 },
                    { 129, 6, 10, null, false, false, null, "Tannkremer", null, null, 0, 0m, 50 },
                    { 130, 6, 11, null, false, false, null, "Antibac", null, null, 0, 0m, 14 },
                    { 131, 6, 12, null, false, false, null, "Blekk til printer", null, null, 0, 0m, 9 },
                    { 132, 6, 13, null, false, false, null, "Ansiktsmasker", null, null, 0, 0m, 40 },
                    { 133, 6, 14, null, false, false, null, "Ekstra ovn", null, null, 0, 0m, 2 },
                    { 134, 6, 15, null, false, false, null, "Fluesmekker", null, null, 0, 0m, 11 },
                    { 140, 7, 1, null, false, false, null, "Batteri AA", null, null, 0, 0m, 42 },
                    { 141, 7, 2, null, false, false, null, "Batteri AAA", null, null, 0, 0m, 38 },
                    { 142, 7, 3, null, false, false, null, "Fjernkontroller", null, null, 0, 0m, 6 },
                    { 143, 7, 4, null, false, false, null, "Ørepropper", null, null, 0, 0m, 20 },
                    { 144, 7, 5, null, false, false, null, "Hammer", null, null, 0, 0m, 0 },
                    { 145, 7, 6, null, false, false, null, "Skrutrekker", null, null, 0, 0m, 0 },
                    { 146, 7, 7, null, false, false, null, "Vanlig verktøy", null, null, 0, 0m, 4 },
                    { 147, 7, 8, null, false, false, null, "Spikere", null, null, 0, 0m, 300 },
                    { 148, 7, 9, null, false, false, null, "Lyspærer", null, null, 0, 0m, 28 },
                    { 149, 7, 10, null, false, false, null, "Nøkkelutstyr", null, null, 0, 0m, 14 },
                    { 150, 7, 11, null, false, false, null, "Bånd", null, null, 0, 0m, 16 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}

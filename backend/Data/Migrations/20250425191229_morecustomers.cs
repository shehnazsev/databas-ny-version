using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class morecustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Kunder",
                keyColumn: "Kundnummer",
                keyValue: 1,
                columns: new[] { "Namn", "Telefonnummer" },
                values: new object[] { "Kalle", "0732758912" });

            migrationBuilder.InsertData(
                table: "Kunder",
                columns: new[] { "Kundnummer", "Namn", "Telefonnummer" },
                values: new object[,]
                {
                    { 2, "Anna", "0708190298" },
                    { 3, "Pelle", "08-1272489" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Kunder",
                keyColumn: "Kundnummer",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Kunder",
                keyColumn: "Kundnummer",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Kunder",
                keyColumn: "Kundnummer",
                keyValue: 1,
                columns: new[] { "Namn", "Telefonnummer" },
                values: new object[] { "Kund Kundsson", "0123456789" });
        }
    }
}

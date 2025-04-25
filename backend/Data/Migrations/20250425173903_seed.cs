using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tjanster",
                keyColumn: "TjanstId",
                keyValue: 1,
                column: "Namn",
                value: "IT-konsult");

            migrationBuilder.InsertData(
                table: "Tjanster",
                columns: new[] { "TjanstId", "Namn", "PrisPerTimme" },
                values: new object[] { 2, "HR-konsult", 2000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tjanster",
                keyColumn: "TjanstId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Tjanster",
                keyColumn: "TjanstId",
                keyValue: 1,
                column: "Namn",
                value: "Tjänst");
        }
    }
}

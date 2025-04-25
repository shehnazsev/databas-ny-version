using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class kundprojekt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projekt_Kunder_Kundnummer",
                table: "Projekt");

            migrationBuilder.DropIndex(
                name: "IX_Projekt_Kundnummer",
                table: "Projekt");

            migrationBuilder.CreateTable(
                name: "KundProjekt",
                columns: table => new
                {
                    KunderKundnummer = table.Column<int>(type: "int", nullable: false),
                    Projektnummer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KundProjekt", x => new { x.KunderKundnummer, x.Projektnummer });
                    table.ForeignKey(
                        name: "FK_KundProjekt_Kunder_KunderKundnummer",
                        column: x => x.KunderKundnummer,
                        principalTable: "Kunder",
                        principalColumn: "Kundnummer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KundProjekt_Projekt_Projektnummer",
                        column: x => x.Projektnummer,
                        principalTable: "Projekt",
                        principalColumn: "Projektnummer",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KundProjekt_Projektnummer",
                table: "KundProjekt",
                column: "Projektnummer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KundProjekt");

            migrationBuilder.CreateIndex(
                name: "IX_Projekt_Kundnummer",
                table: "Projekt",
                column: "Kundnummer");

            migrationBuilder.AddForeignKey(
                name: "FK_Projekt_Kunder_Kundnummer",
                table: "Projekt",
                column: "Kundnummer",
                principalTable: "Kunder",
                principalColumn: "Kundnummer",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

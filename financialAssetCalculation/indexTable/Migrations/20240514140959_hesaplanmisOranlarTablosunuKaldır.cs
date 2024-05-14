using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace indexTable.Migrations
{
    /// <inheritdoc />
    public partial class hesaplanmisOranlarTablosunuKaldır : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hesaplanmisOranlars");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hesaplanmisOranlars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    degisimOrani = table.Column<double>(type: "float", nullable: false),
                    dolarizasyonEtkisiYüzde = table.Column<double>(type: "float", nullable: false),
                    dolarizasyonOncekiAyaGoreArtis = table.Column<double>(type: "float", nullable: false),
                    dolarizasyonVarlikDegisimOrani = table.Column<double>(type: "float", nullable: false),
                    dolarizasyonVarlikTutari = table.Column<double>(type: "float", nullable: false),
                    endeksOran = table.Column<double>(type: "float", nullable: false),
                    enflasyonEtkisiYüzde = table.Column<double>(type: "float", nullable: false),
                    enflasyonOncekiAyaGorevarlikArtis = table.Column<double>(type: "float", nullable: false),
                    enflasyonVarlikDegisimOrani = table.Column<double>(type: "float", nullable: false),
                    enflasyonVarlikTutari = table.Column<double>(type: "float", nullable: false),
                    oncekiAyaGoreArtis = table.Column<double>(type: "float", nullable: false),
                    varlikTarihiDolarKuru = table.Column<double>(type: "float", nullable: false),
                    varlikTutari = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hesaplanmisOranlars", x => x.Id);
                });
        }
    }
}

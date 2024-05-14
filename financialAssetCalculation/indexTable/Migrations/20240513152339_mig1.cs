using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace indexTable.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hesaplanmisOranlars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    varlikTutari = table.Column<double>(type: "float", nullable: false),
                    oncekiAyaGoreArtis = table.Column<double>(type: "float", nullable: false),
                    degisimOrani = table.Column<double>(type: "float", nullable: false),
                    varlikTarihiDolarKuru = table.Column<double>(type: "float", nullable: false),
                    dolarizasyonVarlikTutari = table.Column<double>(type: "float", nullable: false),
                    dolarizasyonOncekiAyaGoreArtis = table.Column<double>(type: "float", nullable: false),
                    dolarizasyonVarlikDegisimOrani = table.Column<double>(type: "float", nullable: false),
                    dolarizasyonEtkisiYüzde = table.Column<double>(type: "float", nullable: false),
                    endeksOran = table.Column<double>(type: "float", nullable: false),
                    enflasyonVarlikTutari = table.Column<double>(type: "float", nullable: false),
                    enflasyonOncekiAyaGorevarlikArtis = table.Column<double>(type: "float", nullable: false),
                    enflasyonVarlikDegisimOrani = table.Column<double>(type: "float", nullable: false),
                    enflasyonEtkisiYüzde = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hesaplanmisOranlars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kurBilgis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kurBilgis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tufeEndeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    endeksOrani = table.Column<double>(type: "float", nullable: false),
                    dolarKuru = table.Column<double>(type: "float", nullable: false),
                    Tarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tufeEndeks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "varlikBilgis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tutari = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_varlikBilgis", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hesaplanmisOranlars");

            migrationBuilder.DropTable(
                name: "kurBilgis");

            migrationBuilder.DropTable(
                name: "tufeEndeks");

            migrationBuilder.DropTable(
                name: "varlikBilgis");
        }
    }
}

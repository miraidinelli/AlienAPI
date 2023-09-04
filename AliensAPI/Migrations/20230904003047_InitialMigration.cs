using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AliensAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NativeSpecies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Powers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Powers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aliens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NativePlanetId = table.Column<int>(type: "int", nullable: false),
                    OriginPlanetId = table.Column<int>(type: "int", nullable: false),
                    OriginArrival = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OriginDeparture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentPlanetId = table.Column<int>(type: "int", nullable: false),
                    CurrentArrival = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentDeparture = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aliens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aliens_Planets_CurrentPlanetId",
                        column: x => x.CurrentPlanetId,
                        principalTable: "Planets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aliens_Planets_NativePlanetId",
                        column: x => x.NativePlanetId,
                        principalTable: "Planets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aliens_Planets_OriginPlanetId",
                        column: x => x.OriginPlanetId,
                        principalTable: "Planets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AliensPowers",
                columns: table => new
                {
                    AlienId = table.Column<int>(type: "int", nullable: false),
                    PowerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AliensPowers", x => new { x.AlienId, x.PowerId });
                    table.ForeignKey(
                        name: "FK_AliensPowers_Aliens_AlienId",
                        column: x => x.AlienId,
                        principalTable: "Aliens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AliensPowers_Powers_PowerId",
                        column: x => x.PowerId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aliens_CurrentPlanetId",
                table: "Aliens",
                column: "CurrentPlanetId");

            migrationBuilder.CreateIndex(
                name: "IX_Aliens_NativePlanetId",
                table: "Aliens",
                column: "NativePlanetId");

            migrationBuilder.CreateIndex(
                name: "IX_Aliens_OriginPlanetId",
                table: "Aliens",
                column: "OriginPlanetId");

            migrationBuilder.CreateIndex(
                name: "IX_AliensPowers_PowerId",
                table: "AliensPowers",
                column: "PowerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AliensPowers");

            migrationBuilder.DropTable(
                name: "Aliens");

            migrationBuilder.DropTable(
                name: "Powers");

            migrationBuilder.DropTable(
                name: "Planets");
        }
    }
}

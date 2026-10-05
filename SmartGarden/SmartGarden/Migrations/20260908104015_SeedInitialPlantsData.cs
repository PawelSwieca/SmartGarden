using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartGarden.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialPlantsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PlantProfiles",
                columns: new[] { "Id", "DaysToHarvest", "Description", "Name", "Species", "WateringFrequencyDays" },
                values: new object[,]
                {
                    { 1, 80, "Wysoka odmiana, wymaga palikowania.", "Pomidor Malinowy", 0, 2 },
                    { 2, 45, "Odmiana wczesna, bardzo słodka.", "Truskawka", 1, 1 },
                    { 3, 30, "Aromatyczne zioło, idealne do sałatek.", "Bazylia", 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "PlantedCrops",
                columns: new[] { "Id", "Area", "EstimatedHarvestDate", "GardenId", "IsHarvested", "LastWateredDate", "PlantProfileId", "PlantingDate", "YieldWeight" },
                values: new object[,]
                {
                    { 1, 5.0, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 2.5, new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlantProfiles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PlantedCrops",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PlantedCrops",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PlantProfiles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PlantProfiles",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}

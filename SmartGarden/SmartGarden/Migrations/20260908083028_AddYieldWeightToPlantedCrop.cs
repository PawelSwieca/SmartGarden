using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartGarden.Migrations
{
    /// <inheritdoc />
    public partial class AddYieldWeightToPlantedCrop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "YieldWeight",
                table: "PlantedCrops",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YieldWeight",
                table: "PlantedCrops");
        }
    }
}

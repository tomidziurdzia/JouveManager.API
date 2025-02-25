using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JouveManager.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShipmentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delivered",
                table: "TravelShipments");

            migrationBuilder.AddColumn<int>(
                name: "ShipmentStatus",
                table: "TravelShipments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipmentStatus",
                table: "TravelShipments");

            migrationBuilder.AddColumn<bool>(
                name: "Delivered",
                table: "TravelShipments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

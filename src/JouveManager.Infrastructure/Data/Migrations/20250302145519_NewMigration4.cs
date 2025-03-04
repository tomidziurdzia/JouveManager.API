using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JouveManager.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelShipments",
                table: "TravelShipments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelShipments",
                table: "TravelShipments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TravelShipments_TravelId",
                table: "TravelShipments",
                column: "TravelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelShipments",
                table: "TravelShipments");

            migrationBuilder.DropIndex(
                name: "IX_TravelShipments_TravelId",
                table: "TravelShipments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelShipments",
                table: "TravelShipments",
                columns: new[] { "TravelId", "ShipmentId" });
        }
    }
}

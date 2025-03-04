using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JouveManager.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class HistoryShipmentDelete2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelShipmentHistories_TravelShipments_TravelShipmentId",
                table: "TravelShipmentHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelShipmentHistories_TravelShipments_TravelShipmentId",
                table: "TravelShipmentHistories",
                column: "TravelShipmentId",
                principalTable: "TravelShipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelShipmentHistories_TravelShipments_TravelShipmentId",
                table: "TravelShipmentHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelShipmentHistories_TravelShipments_TravelShipmentId",
                table: "TravelShipmentHistories",
                column: "TravelShipmentId",
                principalTable: "TravelShipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

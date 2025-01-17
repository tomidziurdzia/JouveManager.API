using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JouveManager.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelShipment_Shipments_ShipmentId",
                table: "TravelShipment");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelShipment_Travels_TravelId",
                table: "TravelShipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelShipment",
                table: "TravelShipment");

            migrationBuilder.RenameTable(
                name: "TravelShipment",
                newName: "TravelShipments");

            migrationBuilder.RenameIndex(
                name: "IX_TravelShipment_ShipmentId",
                table: "TravelShipments",
                newName: "IX_TravelShipments_ShipmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelShipments",
                table: "TravelShipments",
                columns: new[] { "TravelId", "ShipmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TravelShipments_Shipments_ShipmentId",
                table: "TravelShipments",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelShipments_Travels_TravelId",
                table: "TravelShipments",
                column: "TravelId",
                principalTable: "Travels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelShipments_Shipments_ShipmentId",
                table: "TravelShipments");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelShipments_Travels_TravelId",
                table: "TravelShipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelShipments",
                table: "TravelShipments");

            migrationBuilder.RenameTable(
                name: "TravelShipments",
                newName: "TravelShipment");

            migrationBuilder.RenameIndex(
                name: "IX_TravelShipments_ShipmentId",
                table: "TravelShipment",
                newName: "IX_TravelShipment_ShipmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelShipment",
                table: "TravelShipment",
                columns: new[] { "TravelId", "ShipmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TravelShipment_Shipments_ShipmentId",
                table: "TravelShipment",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelShipment_Travels_TravelId",
                table: "TravelShipment",
                column: "TravelId",
                principalTable: "Travels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

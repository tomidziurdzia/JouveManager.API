using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JouveManager.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelShipmentHistories_TravelShipments_TravelShipmentId",
                table: "TravelShipmentHistories");

            migrationBuilder.DropIndex(
                name: "IX_TravelShipmentHistories_TravelShipmentId",
                table: "TravelShipmentHistories");

            migrationBuilder.DropColumn(
                name: "TravelShipmentId",
                table: "TravelShipmentHistories");

            migrationBuilder.AddColumn<Guid>(
                name: "ShipmentId",
                table: "TravelShipmentHistories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TravelShipmentHistories_ShipmentId",
                table: "TravelShipmentHistories",
                column: "ShipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelShipmentHistories_Shipments_ShipmentId",
                table: "TravelShipmentHistories",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelShipmentHistories_Shipments_ShipmentId",
                table: "TravelShipmentHistories");

            migrationBuilder.DropIndex(
                name: "IX_TravelShipmentHistories_ShipmentId",
                table: "TravelShipmentHistories");

            migrationBuilder.DropColumn(
                name: "ShipmentId",
                table: "TravelShipmentHistories");

            migrationBuilder.AddColumn<Guid>(
                name: "TravelShipmentId",
                table: "TravelShipmentHistories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TravelShipmentHistories_TravelShipmentId",
                table: "TravelShipmentHistories",
                column: "TravelShipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelShipmentHistories_TravelShipments_TravelShipmentId",
                table: "TravelShipmentHistories",
                column: "TravelShipmentId",
                principalTable: "TravelShipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipmentService.Migrations
{
    /// <inheritdoc />
    public partial class shipmentUpdated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE `ShipmentItems` CHANGE COLUMN `OverallWeight` `Weight` DECIMAL(18,2);");
            migrationBuilder.Sql("ALTER TABLE `ShipmentItems` CHANGE COLUMN `OverallVolume` `Volume` DECIMAL(18,2);");
            migrationBuilder.Sql("ALTER TABLE `ShipmentItems` CHANGE COLUMN `OverallCharge` `Charge` DECIMAL(18,2);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "ShipmentItems",
                newName: "OverallWeight");

            migrationBuilder.RenameColumn(
                name: "Volume",
                table: "ShipmentItems",
                newName: "OverallVolume");

            migrationBuilder.RenameColumn(
                name: "Charge",
                table: "ShipmentItems",
                newName: "OverallCharge");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipmentService.Migrations
{
    /// <inheritdoc />
    public partial class shipmentUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE `ShipmentItems` CHANGE `Distance` `ItemType` VARCHAR(50);");

            
            // migrationBuilder.RenameColumn(
            //     name: "Distance",
            //     table: "ShipmentItems",
            //     newName: "ItemType");

            migrationBuilder.AddColumn<string>(
                name: "Distance",
                table: "Shipments",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OverallCharge",
                table: "Shipments",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OverallVolume",
                table: "Shipments",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OverallWeight",
                table: "Shipments",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "OverallCharge",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "OverallVolume",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "OverallWeight",
                table: "Shipments");

            // migrationBuilder.RenameColumn(
            //     name: "ItemType",
            //     table: "ShipmentItems",
            //     newName: "Distance");
            
            migrationBuilder.Sql("ALTER TABLE `ShipmentItems` CHANGE `ItemType` `Distance` VARCHAR(50);");

        }
    }
}

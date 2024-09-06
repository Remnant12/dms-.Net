using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipmentService.Migrations
{
    /// <inheritdoc />
    public partial class trying2 : Migration
    {
        
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Priority1",
                table: "Shipments",
                newName: "Priority3");

            migrationBuilder.AddColumn<int>(
                name: "Priority2",
                table: "Shipments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority2",
                table: "Shipments");

            migrationBuilder.RenameColumn(
                name: "Priority3",
                table: "Shipments",
                newName: "Priority1");
        }
    }
}

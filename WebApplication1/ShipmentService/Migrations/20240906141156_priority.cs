using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipmentService.Migrations
{
    /// <inheritdoc />
    public partial class priority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add the new Priority column to the existing Shipments table
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Shipments",
                type: "int",
                nullable: false,
                defaultValue: 0); // You can choose a default value if needed
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the Priority column if rolling back
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Shipments");
        }
    }
}

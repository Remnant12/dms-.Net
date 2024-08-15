using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAuthenticateService.Migrations
{
    /// <inheritdoc />
    public partial class Role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            // migrationBuilder.CreateTable(
            //     name: "Roles",
            //     columns: table => new
            //     {
            //         RoleId = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //         RoleName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
            //             .Annotation("MySql:CharSet", "utf8mb4")
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Roles", x => x.RoleId);
            //     })
            //     .Annotation("MySql:CharSet", "utf8mb4");

            // migrationBuilder.CreateTable(
            //     name: "TokenBlacklist",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //         Token = table.Column<string>(type: "longtext", nullable: false)
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         ExpirationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
            //         BlacklistedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_TokenBlacklist", x => x.Id);
            //     })
            //     .Annotation("MySql:CharSet", "utf8mb4");

            // migrationBuilder.CreateTable(
            //     name: "Users",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //         FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         Address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         Email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         Country = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         PostalCode = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         Province = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         City = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
            //             .Annotation("MySql:CharSet", "utf8mb4"),
            //         Password = table.Column<string>(type: "longtext", nullable: false)
            //             .Annotation("MySql:CharSet", "utf8mb4")
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Users", x => x.Id);
            //     })
            //     .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TokenBlacklist");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

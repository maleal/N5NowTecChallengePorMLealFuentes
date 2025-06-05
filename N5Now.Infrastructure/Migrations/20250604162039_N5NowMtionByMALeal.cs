using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N5Now.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class N5NowMtionByMALeal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "permissionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeForeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeSurName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermissionsDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PermissionTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_permissions_permissionTypes_PermissionTypeId",
                        column: x => x.PermissionTypeId,
                        principalTable: "permissionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_permissions_PermissionTypeId",
                table: "permissions",
                column: "PermissionTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "permissionTypes");
        }
    }
}

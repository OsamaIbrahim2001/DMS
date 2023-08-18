using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DMS.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceCategories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PropertyItems",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyItems", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceCategoryID = table.Column<int>(type: "int", nullable: false),
                    AcquisitionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Devices_DeviceCategories_DeviceCategoryID",
                        column: x => x.DeviceCategoryID,
                        principalTable: "DeviceCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCategoryPropertyItem",
                columns: table => new
                {
                    DeviceCategoriesID = table.Column<int>(type: "int", nullable: false),
                    PropertiesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCategoryPropertyItem", x => new { x.DeviceCategoriesID, x.PropertiesID });
                    table.ForeignKey(
                        name: "FK_DeviceCategoryPropertyItem_DeviceCategories_DeviceCategoriesID",
                        column: x => x.DeviceCategoriesID,
                        principalTable: "DeviceCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceCategoryPropertyItem_PropertyItems_PropertiesID",
                        column: x => x.PropertiesID,
                        principalTable: "PropertyItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyValues",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DevicesID = table.Column<int>(type: "int", nullable: false),
                    PropertyItemID = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyValues", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertyValues_Devices_DevicesID",
                        column: x => x.DevicesID,
                        principalTable: "Devices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyValues_PropertyItems_PropertyItemID",
                        column: x => x.PropertyItemID,
                        principalTable: "PropertyItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DeviceCategories",
                columns: new[] { "ID", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Printer" },
                    { 2, "Laptop" },
                    { 3, "Switch" }
                });

            migrationBuilder.InsertData(
                table: "PropertyItems",
                columns: new[] { "ID", "PropertyDescription" },
                values: new object[,]
                {
                    { 1, "HD" },
                    { 3, "Processor" },
                    { 5, "Dimensions" },
                    { 6, "MAC Address" },
                    { 7, "IP Address" },
                    { 8, "Allow USB" },
                    { 9, "Current User" },
                    { 10, "Network Speed" },
                    { 11, "Operating System" },
                    { 12, "Ports" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCategoryPropertyItem_PropertiesID",
                table: "DeviceCategoryPropertyItem",
                column: "PropertiesID");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceCategoryID",
                table: "Devices",
                column: "DeviceCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyValues_DevicesID",
                table: "PropertyValues",
                column: "DevicesID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyValues_PropertyItemID",
                table: "PropertyValues",
                column: "PropertyItemID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceCategoryPropertyItem");

            migrationBuilder.DropTable(
                name: "PropertyValues");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "PropertyItems");

            migrationBuilder.DropTable(
                name: "DeviceCategories");
        }
    }
}

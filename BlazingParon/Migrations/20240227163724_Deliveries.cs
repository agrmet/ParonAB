using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazingParon.Migrations
{
    /// <inheritdoc />
    public partial class Deliveries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    SendingWarehouseId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReceivingWarehouseId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => new { x.ProductId, x.ReceivingWarehouseId, x.SendingWarehouseId });
                    table.ForeignKey(
                        name: "FK_Deliveries_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deliveries_Warehouses_ReceivingWarehouseId",
                        column: x => x.ReceivingWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deliveries_Warehouses_SendingWarehouseId",
                        column: x => x.SendingWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_ReceivingWarehouseId",
                table: "Deliveries",
                column: "ReceivingWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_SendingWarehouseId",
                table: "Deliveries",
                column: "SendingWarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deliveries");
        }
    }
}

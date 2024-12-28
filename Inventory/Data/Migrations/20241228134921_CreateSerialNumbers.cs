using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Migrations
{
    /// <inheritdoc />
    public partial class CreateSerialNumbers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Assets");

            migrationBuilder.AddColumn<string>(
                name: "SerialNumberId",
                table: "StockTurnovers",
                type: "nvarchar(36)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumberId",
                table: "StockBalances",
                type: "nvarchar(36)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumberId",
                table: "OrderItems",
                type: "nvarchar(36)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SerialNumbers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    AssetId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Number = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerialNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SerialNumbers_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockTurnovers_SerialNumberId",
                table: "StockTurnovers",
                column: "SerialNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_StockBalances_SerialNumberId",
                table: "StockBalances",
                column: "SerialNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_SerialNumberId",
                table: "OrderItems",
                column: "SerialNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_AssetId",
                table: "SerialNumbers",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_SerialNumbers_SerialNumberId",
                table: "OrderItems",
                column: "SerialNumberId",
                principalTable: "SerialNumbers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockBalances_SerialNumbers_SerialNumberId",
                table: "StockBalances",
                column: "SerialNumberId",
                principalTable: "SerialNumbers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTurnovers_SerialNumbers_SerialNumberId",
                table: "StockTurnovers",
                column: "SerialNumberId",
                principalTable: "SerialNumbers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_SerialNumbers_SerialNumberId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_StockBalances_SerialNumbers_SerialNumberId",
                table: "StockBalances");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTurnovers_SerialNumbers_SerialNumberId",
                table: "StockTurnovers");

            migrationBuilder.DropTable(
                name: "SerialNumbers");

            migrationBuilder.DropIndex(
                name: "IX_StockTurnovers_SerialNumberId",
                table: "StockTurnovers");

            migrationBuilder.DropIndex(
                name: "IX_StockBalances_SerialNumberId",
                table: "StockBalances");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_SerialNumberId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "SerialNumberId",
                table: "StockTurnovers");

            migrationBuilder.DropColumn(
                name: "SerialNumberId",
                table: "StockBalances");

            migrationBuilder.DropColumn(
                name: "SerialNumberId",
                table: "OrderItems");

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Assets",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}

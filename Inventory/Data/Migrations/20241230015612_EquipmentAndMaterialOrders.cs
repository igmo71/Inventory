using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Migrations
{
    /// <inheritdoc />
    public partial class EquipmentAndMaterialOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_SerialNumbers_SerialNumberId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_AssigneeFromId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_AssigneeToId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Locations_LocationFromId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Locations_LocationToId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AssigneeFromId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_LocationFromId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_SerialNumberId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "AssigneeFromId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LocationFromId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SerialNumberId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "LocationToId",
                table: "Orders",
                newName: "LocationId");

            migrationBuilder.RenameColumn(
                name: "AssigneeToId",
                table: "Orders",
                newName: "AssigneeId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_LocationToId",
                table: "Orders",
                newName: "IX_Orders_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_AssigneeToId",
                table: "Orders",
                newName: "IX_Orders_AssigneeId");

            migrationBuilder.AddColumn<string>(
                name: "AssetId",
                table: "Orders",
                type: "nvarchar(36)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Orders",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SerialNumberId",
                table: "Orders",
                type: "nvarchar(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AssetId",
                table: "Orders",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SerialNumberId",
                table: "Orders",
                column: "SerialNumberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_AssigneeId",
                table: "Orders",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Assets_AssetId",
                table: "Orders",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Locations_LocationId",
                table: "Orders",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_SerialNumbers_SerialNumberId",
                table: "Orders",
                column: "SerialNumberId",
                principalTable: "SerialNumbers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_AssigneeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Assets_AssetId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Locations_LocationId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_SerialNumbers_SerialNumberId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AssetId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SerialNumberId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SerialNumberId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Orders",
                newName: "LocationToId");

            migrationBuilder.RenameColumn(
                name: "AssigneeId",
                table: "Orders",
                newName: "AssigneeToId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_LocationId",
                table: "Orders",
                newName: "IX_Orders_LocationToId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_AssigneeId",
                table: "Orders",
                newName: "IX_Orders_AssigneeToId");

            migrationBuilder.AddColumn<string>(
                name: "AssigneeFromId",
                table: "Orders",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationFromId",
                table: "Orders",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumberId",
                table: "OrderItems",
                type: "nvarchar(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AssigneeFromId",
                table: "Orders",
                column: "AssigneeFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LocationFromId",
                table: "Orders",
                column: "LocationFromId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_SerialNumberId",
                table: "OrderItems",
                column: "SerialNumberId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_SerialNumbers_SerialNumberId",
                table: "OrderItems",
                column: "SerialNumberId",
                principalTable: "SerialNumbers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_AssigneeFromId",
                table: "Orders",
                column: "AssigneeFromId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_AssigneeToId",
                table: "Orders",
                column: "AssigneeToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Locations_LocationFromId",
                table: "Orders",
                column: "LocationFromId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Locations_LocationToId",
                table: "Orders",
                column: "LocationToId",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}

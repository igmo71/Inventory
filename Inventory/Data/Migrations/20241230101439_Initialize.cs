using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsFolder = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipments_Equipments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Equipments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsFolder = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Locations_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsFolder = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_Materials_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SerialNumbers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    EquipmentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Number = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerialNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SerialNumbers_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialBalances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    AssigneeId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    LocationId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    MaterialId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Balance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialBalances_AspNetUsers_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialBalances_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialBalances_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentHistories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    AssigneeId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    LocationId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    EquipmentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    SerialNumberId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    DateBegin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentHistories_AspNetUsers_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EquipmentHistories_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EquipmentHistories_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EquipmentHistories_SerialNumbers_SerialNumberId",
                        column: x => x.SerialNumberId,
                        principalTable: "SerialNumbers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    IsAssigned = table.Column<bool>(type: "bit", nullable: false),
                    AssigneeId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    LocationId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    EquipmentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    SerialNumberId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_SerialNumbers_SerialNumberId",
                        column: x => x.SerialNumberId,
                        principalTable: "SerialNumbers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MaterialOrderItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    MayerialId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Count = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialOrderItems_Materials_MayerialId",
                        column: x => x.MayerialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialOrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialTurnovers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    AssigneeId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    LocationId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    MaterialId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    OrderId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OpeningBalance = table.Column<double>(type: "float", nullable: false),
                    Receipt = table.Column<double>(type: "float", nullable: false),
                    Expense = table.Column<double>(type: "float", nullable: false),
                    ClosingBalance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTurnovers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialTurnovers_AspNetUsers_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialTurnovers_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialTurnovers_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialTurnovers_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentHistories_AssigneeId",
                table: "EquipmentHistories",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentHistories_EquipmentId",
                table: "EquipmentHistories",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentHistories_LocationId",
                table: "EquipmentHistories",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentHistories_SerialNumberId",
                table: "EquipmentHistories",
                column: "SerialNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_ParentId",
                table: "Equipments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ParentId",
                table: "Locations",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialBalances_AssigneeId",
                table: "MaterialBalances",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialBalances_LocationId",
                table: "MaterialBalances",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialBalances_MaterialId",
                table: "MaterialBalances",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialOrderItems_MayerialId",
                table: "MaterialOrderItems",
                column: "MayerialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialOrderItems_OrderId",
                table: "MaterialOrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_ParentId",
                table: "Materials",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTurnovers_AssigneeId",
                table: "MaterialTurnovers",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTurnovers_LocationId",
                table: "MaterialTurnovers",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTurnovers_MaterialId",
                table: "MaterialTurnovers",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTurnovers_OrderId",
                table: "MaterialTurnovers",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AssigneeId",
                table: "Orders",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AuthorId",
                table: "Orders",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EquipmentId",
                table: "Orders",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LocationId",
                table: "Orders",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SerialNumberId",
                table: "Orders",
                column: "SerialNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_EquipmentId",
                table: "SerialNumbers",
                column: "EquipmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentHistories");

            migrationBuilder.DropTable(
                name: "MaterialBalances");

            migrationBuilder.DropTable(
                name: "MaterialOrderItems");

            migrationBuilder.DropTable(
                name: "MaterialTurnovers");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "SerialNumbers");

            migrationBuilder.DropTable(
                name: "Equipments");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Migrations
{
    /// <inheritdoc />
    public partial class CreateEquipmentOrderFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentOrderFiles",
                columns: table => new
                {
                    EquipmentOrderId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TrustedFileName = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentOrderFiles", x => new { x.EquipmentOrderId, x.TrustedFileName });
                    table.ForeignKey(
                        name: "FK_EquipmentOrderFiles_Orders_EquipmentOrderId",
                        column: x => x.EquipmentOrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentOrderFiles");
        }
    }
}

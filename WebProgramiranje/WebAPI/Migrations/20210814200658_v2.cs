using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Table_Quantity_Quantities",
                table: "Table");

            migrationBuilder.DropIndex(
                name: "IX_Table_Quantities",
                table: "Table");

            migrationBuilder.DropColumn(
                name: "Quantities",
                table: "Table");

            migrationBuilder.CreateIndex(
                name: "IX_Quantity_Quantities",
                table: "Quantity",
                column: "Quantities");

            migrationBuilder.AddForeignKey(
                name: "FK_Quantity_Table_Quantities",
                table: "Quantity",
                column: "Quantities",
                principalTable: "Table",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quantity_Table_Quantities",
                table: "Quantity");

            migrationBuilder.DropIndex(
                name: "IX_Quantity_Quantities",
                table: "Quantity");

            migrationBuilder.AddColumn<int>(
                name: "Quantities",
                table: "Table",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Table_Quantities",
                table: "Table",
                column: "Quantities",
                unique: true,
                filter: "[Quantities] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Table_Quantity_Quantities",
                table: "Table",
                column: "Quantities",
                principalTable: "Quantity",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

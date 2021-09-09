using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quantity_Product_productName",
                table: "Quantity");

            migrationBuilder.DropForeignKey(
                name: "FK_Quantity_Table_Quantities",
                table: "Quantity");

            migrationBuilder.DropIndex(
                name: "IX_Quantity_Quantities",
                table: "Quantity");

            migrationBuilder.RenameColumn(
                name: "productName",
                table: "Quantity",
                newName: "ProductName");

            migrationBuilder.RenameIndex(
                name: "IX_Quantity_productName",
                table: "Quantity",
                newName: "IX_Quantity_ProductName");

            migrationBuilder.AddColumn<int>(
                name: "TableID",
                table: "Quantity",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quantity_TableID",
                table: "Quantity",
                column: "TableID");

            migrationBuilder.AddForeignKey(
                name: "FK_Quantity_Product_ProductName",
                table: "Quantity",
                column: "ProductName",
                principalTable: "Product",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quantity_Table_TableID",
                table: "Quantity",
                column: "TableID",
                principalTable: "Table",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quantity_Product_ProductName",
                table: "Quantity");

            migrationBuilder.DropForeignKey(
                name: "FK_Quantity_Table_TableID",
                table: "Quantity");

            migrationBuilder.DropIndex(
                name: "IX_Quantity_TableID",
                table: "Quantity");

            migrationBuilder.DropColumn(
                name: "TableID",
                table: "Quantity");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Quantity",
                newName: "productName");

            migrationBuilder.RenameIndex(
                name: "IX_Quantity_ProductName",
                table: "Quantity",
                newName: "IX_Quantity_productName");

            migrationBuilder.CreateIndex(
                name: "IX_Quantity_Quantities",
                table: "Quantity",
                column: "Quantities");

            migrationBuilder.AddForeignKey(
                name: "FK_Quantity_Product_productName",
                table: "Quantity",
                column: "productName",
                principalTable: "Product",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quantity_Table_Quantities",
                table: "Quantity",
                column: "Quantities",
                principalTable: "Table",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

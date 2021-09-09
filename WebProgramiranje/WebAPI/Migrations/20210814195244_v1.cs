using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NumberOfRows = table.Column<int>(type: "int", nullable: false),
                    NumberOfColumns = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Quantity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantities = table.Column<int>(type: "int", nullable: false),
                    productName = table.Column<string>(type: "nvarchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quantity", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Table",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantID = table.Column<int>(type: "int", nullable: false),
                    Quantities = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Table_Quantity_Quantities",
                        column: x => x.Quantities,
                        principalTable: "Quantity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Table_Restaurant_RestaurantID",
                        column: x => x.RestaurantID,
                        principalTable: "Restaurant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    RestaurantID = table.Column<int>(type: "int", nullable: true),
                    TableID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Product_Restaurant_RestaurantID",
                        column: x => x.RestaurantID,
                        principalTable: "Restaurant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Table_TableID",
                        column: x => x.TableID,
                        principalTable: "Table",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_RestaurantID",
                table: "Product",
                column: "RestaurantID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_TableID",
                table: "Product",
                column: "TableID");

            migrationBuilder.CreateIndex(
                name: "IX_Quantity_productName",
                table: "Quantity",
                column: "productName");

            migrationBuilder.CreateIndex(
                name: "IX_Table_Quantities",
                table: "Table",
                column: "Quantities",
                unique: true,
                filter: "[Quantities] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Table_RestaurantID",
                table: "Table",
                column: "RestaurantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Quantity_Product_productName",
                table: "Quantity",
                column: "productName",
                principalTable: "Product",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Restaurant_RestaurantID",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_Restaurant_RestaurantID",
                table: "Table");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Table_TableID",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Restaurant");

            migrationBuilder.DropTable(
                name: "Table");

            migrationBuilder.DropTable(
                name: "Quantity");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}

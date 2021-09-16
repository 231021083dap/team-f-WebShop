using Microsoft.EntityFrameworkCore.Migrations;

namespace team_f_WebShop.API.Migrations
{
    public partial class Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Desciption = table.Column<string>(type: "nvarchar(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Desciption", "Name", "Price", "Quantity" },
                values: new object[] { 1, "LED-skærm", "GIGABYTE FI32U", 8575, 6 });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Desciption", "Name", "Price", "Quantity" },
                values: new object[] { 2, "3840 x 2160 (4K)", "GIGABYTE M28U", 5999, 13 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}

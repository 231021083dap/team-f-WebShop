using Microsoft.EntityFrameworkCore.Migrations;

namespace team_f_WebShop.API.Migrations
{
    public partial class fraPendingMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryName = table.Column<string>(type: "nvarchar(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Desciption = table.Column<string>(type: "nvarchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Desciption", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "LED-skærm", "GIGABYTE FI32U", 8575, 6 },
                    { 2, "3840 x 2160 (4K)", "GIGABYTE M28U", 5999, 13 }
                });

            migrationBuilder.InsertData(
                table: "category",
                columns: new[] { "Id", "categoryName" },
                values: new object[,]
                {
                    { 1, "Computer" },
                    { 2, "Screen" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}

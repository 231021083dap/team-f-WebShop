using Microsoft.EntityFrameworkCore.Migrations;

namespace team_f_WebShop.API.Migrations
{
    public partial class Categorys : Migration
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

            migrationBuilder.InsertData(
                table: "category",
                columns: new[] { "Id", "categoryName" },
                values: new object[] { 1, "Computer" });

            migrationBuilder.InsertData(
                table: "category",
                columns: new[] { "Id", "categoryName" },
                values: new object[] { 2, "Screen" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "category");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace ODY.Cihazkapinda.Migrations
{
    public partial class Product_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppProductProperty_ProductId",
                table: "AppProductProperty");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductProperty_ProductId",
                table: "AppProductProperty",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppProductProperty_ProductId",
                table: "AppProductProperty");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductProperty_ProductId",
                table: "AppProductProperty",
                column: "ProductId",
                unique: true);
        }
    }
}

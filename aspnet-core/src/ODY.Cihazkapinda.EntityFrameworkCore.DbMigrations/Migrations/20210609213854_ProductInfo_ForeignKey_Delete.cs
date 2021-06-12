using Microsoft.EntityFrameworkCore.Migrations;

namespace ODY.Cihazkapinda.Migrations
{
    public partial class ProductInfo_ForeignKey_Delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProductInfo_AppProduct_ProductId",
                table: "AppProductInfo");

            migrationBuilder.DropIndex(
                name: "IX_AppProductInfo_ProductId",
                table: "AppProductInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppProductInfo_ProductId",
                table: "AppProductInfo",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductInfo_AppProduct_ProductId",
                table: "AppProductInfo",
                column: "ProductId",
                principalTable: "AppProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

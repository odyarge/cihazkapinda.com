using Microsoft.EntityFrameworkCore.Migrations;

namespace ODY.Cihazkapinda.Migrations
{
    public partial class ProductInfo_ForeignKey_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProductInfo_AppProductInfoTemplate_ProductInfoTemplateId",
                table: "AppProductInfo");

            migrationBuilder.DropIndex(
                name: "IX_AppProductInfo_ProductInfoTemplateId",
                table: "AppProductInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppProductInfo_ProductInfoTemplateId",
                table: "AppProductInfo",
                column: "ProductInfoTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductInfo_AppProductInfoTemplate_ProductInfoTemplateId",
                table: "AppProductInfo",
                column: "ProductInfoTemplateId",
                principalTable: "AppProductInfoTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ODY.Cihazkapinda.Migrations
{
    public partial class ProductInfo_ProductInfoTemplate_ForeignKey_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductInfoId",
                table: "AppProductInfoTemplate",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppProductInfoTemplate_ProductInfoId",
                table: "AppProductInfoTemplate",
                column: "ProductInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductInfoTemplate_AppProductInfo_ProductInfoId",
                table: "AppProductInfoTemplate",
                column: "ProductInfoId",
                principalTable: "AppProductInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProductInfoTemplate_AppProductInfo_ProductInfoId",
                table: "AppProductInfoTemplate");

            migrationBuilder.DropIndex(
                name: "IX_AppProductInfoTemplate_ProductInfoId",
                table: "AppProductInfoTemplate");

            migrationBuilder.DropColumn(
                name: "ProductInfoId",
                table: "AppProductInfoTemplate");
        }
    }
}

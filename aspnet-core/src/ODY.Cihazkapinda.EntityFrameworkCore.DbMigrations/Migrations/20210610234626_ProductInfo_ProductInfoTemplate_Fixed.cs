using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ODY.Cihazkapinda.Migrations
{
    public partial class ProductInfo_ProductInfoTemplate_Fixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProductInfo_AppProductInfoTemplate_ProductInfoTemplateId",
                table: "AppProductInfo");

            migrationBuilder.DropIndex(
                name: "IX_AppProductInfo_ProductInfoTemplateId",
                table: "AppProductInfo");

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
    }
}

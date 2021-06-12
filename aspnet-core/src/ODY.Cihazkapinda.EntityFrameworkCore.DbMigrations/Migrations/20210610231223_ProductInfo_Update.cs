using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ODY.Cihazkapinda.Migrations
{
    public partial class ProductInfo_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AppProductInfo");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "AppProductInfo");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AppProductInfo");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductInfoTemplateId",
                table: "AppProductInfo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductInfoTemplateId",
                table: "AppProductInfo");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AppProductInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AppProductInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AppProductInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

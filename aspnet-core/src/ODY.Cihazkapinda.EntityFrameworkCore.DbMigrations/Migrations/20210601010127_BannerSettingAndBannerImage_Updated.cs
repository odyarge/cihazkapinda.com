using Microsoft.EntityFrameworkCore.Migrations;

namespace ODY.Cihazkapinda.Migrations
{
    public partial class BannerSettingAndBannerImage_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WelcomeMessage",
                table: "AppBannerSetting");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "AppBannerImage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AppBannerImage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "AppBannerImage");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AppBannerImage");

            migrationBuilder.AddColumn<string>(
                name: "WelcomeMessage",
                table: "AppBannerSetting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfectSelf.WebAPI.Migrations
{
    public partial class migrationV15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IntroBucketName",
                table: "ReaderProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IntroVideoKey",
                table: "ReaderProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntroBucketName",
                table: "ReaderProfile");

            migrationBuilder.DropColumn(
                name: "IntroVideoKey",
                table: "ReaderProfile");
        }
    }
}

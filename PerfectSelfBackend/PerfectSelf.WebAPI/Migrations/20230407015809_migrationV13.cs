using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfectSelf.WebAPI.Migrations
{
    public partial class migrationV13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccpted",
                table: "Book",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ReaderReview",
                table: "Book",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "ReaderScore",
                table: "Book",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "IsStandBy",
                table: "Availability",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Repeat",
                table: "Availability",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccpted",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "ReaderReview",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "ReaderScore",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "IsStandBy",
                table: "Availability");

            migrationBuilder.DropColumn(
                name: "Repeat",
                table: "Availability");
        }
    }
}

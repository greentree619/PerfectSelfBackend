using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfectSelf.WebAPI.Migrations
{
    public partial class migrationV7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookTime",
                table: "Book",
                newName: "BookStartTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookEndTime",
                table: "Book",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookEndTime",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "BookStartTime",
                table: "Book",
                newName: "BookTime");
        }
    }
}

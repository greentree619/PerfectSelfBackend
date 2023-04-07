using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfectSelf.WebAPI.Migrations
{
    public partial class migrationV16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReaderReviewDate",
                table: "Book",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReaderReviewDate",
                table: "Book");
        }
    }
}

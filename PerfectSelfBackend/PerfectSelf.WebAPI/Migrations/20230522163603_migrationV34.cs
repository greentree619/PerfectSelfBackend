using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfectSelf.WebAPI.Migrations
{
    public partial class migrationV34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordNo",
                table: "Tape");

            migrationBuilder.AddColumn<string>(
                name: "TapeId",
                table: "Tape",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TapeId",
                table: "Tape");

            migrationBuilder.AddColumn<int>(
                name: "RecordNo",
                table: "Tape",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfectSelf.WebAPI.Migrations
{
    public partial class migrationV14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAccpted",
                table: "Book",
                newName: "IsAccept");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAccept",
                table: "Book",
                newName: "IsAccpted");
        }
    }
}

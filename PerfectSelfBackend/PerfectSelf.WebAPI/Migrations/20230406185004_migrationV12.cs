using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfectSelf.WebAPI.Migrations
{
    public partial class migrationV12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewCount",
                table: "ReaderProfile",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Score",
                table: "ReaderProfile",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "ReviewCount",
                table: "ActorProfile",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Score",
                table: "ActorProfile",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewCount",
                table: "ReaderProfile");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "ReaderProfile");

            migrationBuilder.DropColumn(
                name: "ReviewCount",
                table: "ActorProfile");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "ActorProfile");
        }
    }
}

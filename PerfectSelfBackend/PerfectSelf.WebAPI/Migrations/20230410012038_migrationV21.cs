using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfectSelf.WebAPI.Migrations
{
    public partial class migrationV21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_MessageChannel",
            //    table: "MessageChannel");

            //migrationBuilder.RenameTable(
            //    name: "MessageChannel",
            //    newName: "MessageChannelView");

            migrationBuilder.AddColumn<int>(
                name: "AuditionType",
                table: "ReaderProfile",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsExplicitRead",
                table: "ReaderProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_MessageChannelView",
            //    table: "MessageChannelView",
            //    column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_MessageChannelView",
            //    table: "MessageChannelView");

            migrationBuilder.DropColumn(
                name: "AuditionType",
                table: "ReaderProfile");

            migrationBuilder.DropColumn(
                name: "IsExplicitRead",
                table: "ReaderProfile");

            //migrationBuilder.RenameTable(
            //    name: "MessageChannelView",
            //    newName: "MessageChannel");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_MessageChannel",
            //    table: "MessageChannel",
            //    column: "Id");
        }
    }
}

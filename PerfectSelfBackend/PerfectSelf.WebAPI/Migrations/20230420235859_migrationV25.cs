using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfectSelf.WebAPI.Migrations
{
    public partial class migrationV25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_MessageChannelHistorys",
            //    table: "MessageChannelHistorys");

            //migrationBuilder.RenameTable(
            //    name: "MessageChannelHistorys",
            //    newName: "MessageChannelView");

            //migrationBuilder.RenameColumn(
            //    name: "RepeatFlag1234",
            //    table: "Availability",
            //    newName: "RepeatFlag");

            migrationBuilder.AlterColumn<bool>(
                name: "IsExplicitRead",
                table: "ReaderProfile",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

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

            //migrationBuilder.RenameTable(
            //    name: "MessageChannelView",
            //    newName: "MessageChannelHistorys");

            //migrationBuilder.RenameColumn(
            //    name: "RepeatFlag",
            //    table: "Availability",
            //    newName: "RepeatFlag1234");

            migrationBuilder.AlterColumn<bool>(
                name: "IsExplicitRead",
                table: "ReaderProfile",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_MessageChannelHistorys",
            //    table: "MessageChannelHistorys",
            //    column: "Id");
        }
    }
}

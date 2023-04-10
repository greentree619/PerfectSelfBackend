using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfectSelf.WebAPI.Migrations
{
    public partial class migrationV20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "RoomUid",
                table: "MessageHistory",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "MessageChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SendTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HadRead = table.Column<bool>(type: "bit", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderAvatarBucket = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderAvatarKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverAvatarBucket = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverAvatarKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderIsOnline = table.Column<bool>(type: "bit", nullable: false),
                    ReceiverIsOnline = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageChannel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageChannel");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomUid",
                table: "MessageHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}

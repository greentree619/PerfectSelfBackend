using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfectSelf.WebAPI.Migrations
{
    public partial class migrationV26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "MessageChannelView");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "MessageChannelView",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        HadRead = table.Column<bool>(type: "bit", nullable: false),
            //        Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ReceiverAvatarBucket = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ReceiverAvatarKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ReceiverIsOnline = table.Column<bool>(type: "bit", nullable: false),
            //        ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ReceiverUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        RoomUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        SendTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        SenderAvatarBucket = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        SenderAvatarKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        SenderIsOnline = table.Column<bool>(type: "bit", nullable: false),
            //        SenderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        SenderUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MessageChannelView", x => x.Id);
            //    });
        }
    }
}

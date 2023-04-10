using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfectSelf.WebAPI.Migrations
{
    public partial class migrationV22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE OR ALTER VIEW [dbo].[ReaderList]
AS
SELECT dbo.[User].UserName, dbo.[User].UserType, dbo.[User].Email, dbo.[User].FirstName, dbo.[User].LastName, dbo.[User].Gender
, dbo.[User].IsLogin, dbo.ReaderProfile.HourlyPrice, dbo.ReaderProfile.Title, dbo.ReaderProfile.IsSponsored, dbo.ReaderProfile.IsExplicitRead, dbo.ReaderProfile.AuditionType
, dbo.[User].Uid
, dbo.ReaderProfile.ReviewCount, dbo.ReaderProfile.Score, dbo.ReaderProfile.CreatedTime, dbo.SoonOneAvailability.Date, dbo.SoonOneAvailability.FromTime
, dbo.SoonOneAvailability.ToTime, dbo.SoonOneAvailability.IsStandBy, DATEDIFF(mi, dbo.SoonOneAvailability.FromTime, dbo.SoonOneAvailability.ToTime) as TimeSlot
FROM     dbo.[User] LEFT OUTER JOIN
                  dbo.ReaderProfile ON dbo.[User].Uid = dbo.ReaderProfile.ReaderUid LEFT OUTER JOIN
                  dbo.SoonOneAvailability ON (dbo.[User].Uid = dbo.SoonOneAvailability.ReaderUid)
WHERE  (dbo.[User].UserType = 4 )
GO
");
            //migrationBuilder.DropColumn(
            //    name: "HadRead",
            //    table: "MessageChannelView");

            //migrationBuilder.DropColumn(
            //    name: "Message",
            //    table: "MessageChannelView");

            //migrationBuilder.DropColumn(
            //    name: "ReceiverAvatarBucket",
            //    table: "MessageChannelView");

            //migrationBuilder.DropColumn(
            //    name: "ReceiverAvatarKey",
            //    table: "MessageChannelView");

            //migrationBuilder.DropColumn(
            //    name: "ReceiverIsOnline",
            //    table: "MessageChannelView");

            //migrationBuilder.DropColumn(
            //    name: "ReceiverName",
            //    table: "MessageChannelView");

            //migrationBuilder.DropColumn(
            //    name: "ReceiverUid",
            //    table: "MessageChannelView");

            //migrationBuilder.DropColumn(
            //    name: "RoomUid",
            //    table: "MessageChannelView");

            //migrationBuilder.DropColumn(
            //    name: "SendTime",
            //    table: "MessageChannelView");

            //migrationBuilder.DropColumn(
            //    name: "SenderAvatarBucket",
            //    table: "MessageChannelView");

            //migrationBuilder.DropColumn(
            //    name: "SenderAvatarKey",
            //    table: "MessageChannelView");

            //migrationBuilder.DropColumn(
            //    name: "SenderIsOnline",
            //    table: "MessageChannelView");

            //migrationBuilder.DropColumn(
            //    name: "SenderName",
            //    table: "MessageChannelView");

            //migrationBuilder.DropColumn(
            //    name: "SenderUid",
            //    table: "MessageChannelView");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
drop view MessageChannelView;
");
            //migrationBuilder.AddColumn<bool>(
            //    name: "HadRead",
            //    table: "MessageChannelView",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<string>(
            //    name: "Message",
            //    table: "MessageChannelView",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "ReceiverAvatarBucket",
            //    table: "MessageChannelView",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "ReceiverAvatarKey",
            //    table: "MessageChannelView",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<bool>(
            //    name: "ReceiverIsOnline",
            //    table: "MessageChannelView",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<string>(
            //    name: "ReceiverName",
            //    table: "MessageChannelView",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<Guid>(
            //    name: "ReceiverUid",
            //    table: "MessageChannelView",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<Guid>(
            //    name: "RoomUid",
            //    table: "MessageChannelView",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "SendTime",
            //    table: "MessageChannelView",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.AddColumn<string>(
            //    name: "SenderAvatarBucket",
            //    table: "MessageChannelView",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "SenderAvatarKey",
            //    table: "MessageChannelView",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<bool>(
            //    name: "SenderIsOnline",
            //    table: "MessageChannelView",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<string>(
            //    name: "SenderName",
            //    table: "MessageChannelView",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<Guid>(
            //    name: "SenderUid",
            //    table: "MessageChannelView",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}

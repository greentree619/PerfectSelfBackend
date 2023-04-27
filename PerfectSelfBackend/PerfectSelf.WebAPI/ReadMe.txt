________________________________________________________________________________________
[ViewTable][ViewTable][ViewTable][ViewTable][ViewTable][ViewTable][ViewTable][ViewTable]

CREATE OR ALTER VIEW [dbo].[SoonOneAvailability]
AS
WITH added_row_number AS (
  SELECT
    ReaderUid, IsStandBy, RepeatFlag, Date ,FromTime, ToTime,
    ROW_NUMBER() OVER(PARTITION BY ReaderUid order by Date) AS row_number
  FROM Availability WHERE Date >= CURRENT_TIMESTAMP
)
SELECT
  *
FROM added_row_number
WHERE row_number = 1;
GO

CREATE OR ALTER VIEW [dbo].[ReaderList]
AS
SELECT dbo.[User].UserName, dbo.[User].UserType, dbo.[User].Email, dbo.[User].FirstName, dbo.[User].LastName, dbo.[User].Gender
, dbo.[User].IsLogin, dbo.[User].AvatarBucketName, dbo.[User].AvatarKey, dbo.ReaderProfile.HourlyPrice, dbo.ReaderProfile.Title, dbo.ReaderProfile.IsSponsored, dbo.ReaderProfile.IsExplicitRead, dbo.ReaderProfile.AuditionType
, dbo.[User].Uid
, dbo.ReaderProfile.ReviewCount, dbo.ReaderProfile.Score, dbo.ReaderProfile.CreatedTime, dbo.SoonOneAvailability.Date, dbo.SoonOneAvailability.FromTime
, dbo.SoonOneAvailability.ToTime, dbo.SoonOneAvailability.IsStandBy, DATEDIFF(mi, dbo.SoonOneAvailability.FromTime, dbo.SoonOneAvailability.ToTime) as TimeSlot
FROM     dbo.[User] LEFT OUTER JOIN
                  dbo.ReaderProfile ON dbo.[User].Uid = dbo.ReaderProfile.ReaderUid LEFT OUTER JOIN
                  dbo.SoonOneAvailability ON (dbo.[User].Uid = dbo.SoonOneAvailability.ReaderUid)
WHERE  (dbo.[User].UserType = 4 )
GO

Drop VIEW [dbo].[BookList]
Go
CREATE OR ALTER VIEW [dbo].[BookList]
AS
SELECT dbo.Book.Id, dbo.Book.RoomUid, dbo.Book.BookStartTime, dbo.Book.ScriptBucket, dbo.Book.ScriptKey, dbo.Book.ScriptFile, dbo.Book.BookEndTime, dbo.Book.IsAccept, dbo.Book.ReaderScore, dbo.Book.ReaderReview, dbo.Book.ReaderReviewDate, User_1.UserName AS ReaderName, User_1.IsLogin AS ReaderIsLogin, dbo.ReaderProfile.Title, dbo.ReaderProfile.HourlyPrice, 
                  dbo.ReaderProfile.VoiceType, dbo.ReaderProfile.Others, dbo.Book.ActorUid, dbo.Book.ReaderUid, dbo.ReaderProfile.About, dbo.ReaderProfile.Skills, dbo.[User].UserName AS ActorName, dbo.[User].AvatarBucketName AS ActorBucketName,dbo.[User].AvatarKey AS ActorAvatarKey,User_1.AvatarBucketName AS ReaderBucketName,User_1.AvatarKey As ReaderAvatarKey
FROM     dbo.Book INNER JOIN
                  dbo.[User] ON dbo.Book.ActorUid = dbo.[User].Uid INNER JOIN
                  dbo.[User] AS User_1 ON dbo.Book.ReaderUid = User_1.Uid INNER JOIN
                  dbo.ReaderProfile ON User_1.Uid = dbo.ReaderProfile.ReaderUid
GO

CREATE OR ALTER VIEW [dbo].[MessageChannelUnread]
AS
	SELECT dbo.MessageHistory.RoomUid AS RoomUid, dbo.MessageHistory.ReceiverUid, sum(case when HadRead = 0 then 1 else 0 end) AS UnreadCount
	FROM     dbo.MessageHistory
	GROUP BY dbo.MessageHistory.RoomUid, dbo.MessageHistory.ReceiverUid
GO

CREATE OR ALTER VIEW [dbo].[MessageChannelView]
AS
WITH added_row_number AS (
  SELECT dbo.MessageHistory.Id, dbo.MessageHistory.RoomUid, dbo.MessageHistory.SenderUid AS SenderUid, dbo.[User].UserName AS SenderName, dbo.MessageHistory.ReceiverUid AS ReceiverUid, User_1.UserName AS ReceiverName, dbo.[User].AvatarBucketName AS SenderAvatarBucket, 
                  dbo.[User].AvatarKey AS SenderAvatarKey, User_1.AvatarBucketName AS ReceiverAvatarBucket, User_1.AvatarKey AS ReceiverAvatarKey, dbo.[User].IsLogin AS SenderIsOnline, User_1.IsLogin AS ReceiverIsOnline, 
                  dbo.MessageHistory.Message, dbo.MessageHistory.SendTime, dbo.MessageHistory.HadRead, dbo.MessageChannelUnread.UnreadCount, ROW_NUMBER() OVER(PARTITION BY dbo.MessageHistory.RoomUid order by SendTime desc) AS row_number
  FROM     dbo.MessageHistory INNER JOIN
                  dbo.[User] ON dbo.MessageHistory.SenderUid = dbo.[User].Uid INNER JOIN
                  dbo.[User] AS User_1 ON dbo.MessageHistory.ReceiverUid = User_1.Uid LEFT JOIN
				  MessageChannelUnread ON MessageChannelUnread.RoomUid = dbo.MessageHistory.RoomUid and MessageChannelUnread.ReceiverUid = dbo.MessageHistory.ReceiverUid
)
SELECT
  *
FROM added_row_number
WHERE row_number = 1;
GO


________________________________________________________________________________________
[Procedure][Procedure][Procedure][Procedure][Procedure][Procedure][Procedure][Procedure]

CREATE OR ALTER PROCEDURE [dbo].[GetBookCount]
(@uid nvarchar(250))
AS
BEGIN
DECLARE @book_count INT;
SET @book_count = (
SELECT count(Id)  FROM 
Book
WHERE ReaderUid = @uid);

RETURN @book_count;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[GetUnreadMessageCount]
(@roomUid nvarchar(250))
AS
BEGIN
DECLARE @unread_count INT;
SET @unread_count = (
SELECT count(Id)  FROM 
MessageHistory
WHERE roomUid = @roomUid and HadRead = 0);

RETURN @unread_count;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[GetChatHistoryEx]  
    (@SenderUid NVARCHAR(128), @ReceiverUid NVARCHAR(128))
AS  
BEGIN  
	DECLARE @RoomUid NVARCHAR(128);
	DECLARE @RoomCount Int;
    -- SET NOCOUNT ON added to prevent extra result sets from  
    -- interfering with SELECT statements.  
    SET NOCOUNT ON;
	SET @RoomUid = (
		SELECT top 1 RoomUid FROM 
		MessageHistory
		WHERE (SenderUid = @SenderUid and ReceiverUid = @ReceiverUid) or (ReceiverUid = @SenderUid and SenderUid = @ReceiverUid));
	-- SET NOCOUNT ON;
    With recent_message as (SELECT top 10 * from MessageHistory where RoomUid=@RoomUid order by SendTime desc) SELECT
	  *
	FROM recent_message order by Id;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[SetAllReadMessage]  
    (@ReceiverUid NVARCHAR(128), @SenderUid NVARCHAR(128))
AS  
BEGIN
	If @SenderUid = '-1' -- All message to ReceiverUid
	Begin
		SET NOCOUNT ON;
		UPDATE [MessageHistory] SET [HadRead] = 1 WHERE ReceiverUid = @ReceiverUid;
	End
	Else -- Only from @SenderUid
	Begin
		SET NOCOUNT ON;
		UPDATE MessageHistory SET HadRead = 1 WHERE ReceiverUid = @ReceiverUid and SenderUid = @SenderUid;
	End
END
GO

________________________________________________________________________________________
[Trigger][Trigger][Trigger][Trigger][Trigger][Trigger][Trigger][Trigger][Trigger][Trigger]
CREATE OR ALTER TRIGGER TR_Add_DefaultProfile ON [dbo].[User]
    FOR INSERT
AS
	BEGIN
	    SET NOCOUNT ON
	    If (SELECT UserType FROM INSERTED) = 3 --Actor
	    Begin
		INSERT  INTO dbo.ActorProfile
            ( Title,
				ActorUid,
				AgeRange,
				Height,
				Weight,
				Country,
				State,
				City,
				AgencyCountry,
				VaccinationStatus,
				CreatedTime,
				UpdatedTime,
				DeletedTime,
				IsDeleted
            )
            SELECT  '',
				I.Uid,
				'',
				0,
				0,
				'',
				'',
				'',
				'',
				0,
				CURRENT_TIMESTAMP,
				CURRENT_TIMESTAMP,
				CURRENT_TIMESTAMP,
				0
            FROM    Inserted I
		END

		If (SELECT UserType FROM INSERTED) = 4 --Reader
	    Begin
		INSERT  INTO dbo.ReaderProfile
            ( Title,
				ReaderUid,
				HourlyPrice,
				VoiceType,
				Others,
				About,
				Skills,
				CreatedTime,
				UpdatedTime,
				DeletedTime,
				IsDeleted
            )
            SELECT  '',
				I.Uid,
				0,
				0,
				0,
				'',
				'',
				CURRENT_TIMESTAMP,
				CURRENT_TIMESTAMP,
				CURRENT_TIMESTAMP,
				0
            FROM    Inserted I
		END
     End
GO

CREATE OR ALTER TRIGGER TR_Delete_DefaultProfile ON [dbo].[User]
    FOR DELETE
AS
BEGIN
	    SET NOCOUNT ON
	    If (SELECT UserType FROM DELETED) = 3 --Actor
	    Begin
			DELETE FROM dbo.ActorProfile
				WHERE ActorUid = (SELECT DELETED.Uid FROM DELETED)
		END

		If (SELECT UserType FROM DELETED) = 4 --Reader
	    Begin
			DELETE FROM dbo.ReaderProfile
				WHERE ReaderUid = (SELECT DELETED.Uid FROM DELETED)		
		END
     End
GO
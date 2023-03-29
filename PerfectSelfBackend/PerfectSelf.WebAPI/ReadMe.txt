________________________________________________________________________________________
[ViewTable][ViewTable][ViewTable][ViewTable][ViewTable][ViewTable][ViewTable][ViewTable]

CREATE OR ALTER VIEW [dbo].[ReaderList]
AS
SELECT dbo.[User].UserName, dbo.[User].UserType, dbo.[User].Email, dbo.[User].FirstName, dbo.[User].LastName, dbo.[User].Gender, dbo.[User].IsLogin, dbo.ReaderProfile.HourlyPrice, dbo.ReaderProfile.Title, dbo.[User].Uid, min(dbo.Availability.Date) as Date
FROM     dbo.[User] LEFT OUTER JOIN
                  dbo.ReaderProfile ON dbo.[User].Uid = dbo.ReaderProfile.ReaderUid LEFT JOIN
                  dbo.Availability ON (dbo.[User].Uid = dbo.Availability.ReaderUid and (dbo.Availability.Date >= '3/20/2023 4:28:42 AM'))
WHERE  (dbo.[User].UserType = 4 ) group by dbo.[User].UserName, dbo.[User].UserType, dbo.[User].Email, dbo.[User].FirstName, dbo.[User].LastName, dbo.[User].Gender, dbo.[User].IsLogin, dbo.ReaderProfile.HourlyPrice, dbo.ReaderProfile.Title, dbo.[User].Uid
GO

Drop VIEW [dbo].[BookList]
Go
CREATE OR ALTER VIEW [dbo].[BookList]
AS
SELECT dbo.Book.Id, dbo.Book.RoomUid, dbo.Book.BookStartTime, dbo.Book.ScriptFile, dbo.Book.BookEndTime, User_1.UserName AS ReaderName, User_1.IsLogin AS ReaderIsLogin, dbo.ReaderProfile.Title, dbo.ReaderProfile.HourlyPrice, 
                  dbo.ReaderProfile.VoiceType, dbo.ReaderProfile.Others, dbo.Book.ActorUid, dbo.Book.ReaderUid, dbo.ReaderProfile.About, dbo.ReaderProfile.Skills, dbo.[User].UserName AS ActorName
FROM     dbo.Book INNER JOIN
                  dbo.[User] ON dbo.Book.ActorUid = dbo.[User].Uid INNER JOIN
                  dbo.[User] AS User_1 ON dbo.Book.ReaderUid = User_1.Uid INNER JOIN
                  dbo.ReaderProfile ON User_1.Uid = dbo.ReaderProfile.ReaderUid
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
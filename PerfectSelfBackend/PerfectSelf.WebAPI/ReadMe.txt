Drop VIEW [dbo].[ReaderList]
Go
Create VIEW [dbo].[ReaderList]
AS
SELECT dbo.[User].UserName, dbo.[User].UserType, dbo.[User].Email, dbo.[User].FirstName, dbo.[User].LastName, dbo.[User].Gender, dbo.[User].IsLogin, dbo.ReaderProfile.HourlyPrice, dbo.ReaderProfile.Title, dbo.[User].Uid, min(dbo.Availability.Date) as Date
FROM     dbo.[User] LEFT OUTER JOIN
                  dbo.ReaderProfile ON dbo.[User].Uid = dbo.ReaderProfile.ReaderUid LEFT JOIN
                  dbo.Availability ON (dbo.[User].Uid = dbo.Availability.ReaderUid and (dbo.Availability.Date >= '3/20/2023 4:28:42 AM'))
WHERE  (dbo.[User].UserType = 4 ) group by dbo.[User].UserName, dbo.[User].UserType, dbo.[User].Email, dbo.[User].FirstName, dbo.[User].LastName, dbo.[User].Gender, dbo.[User].IsLogin, dbo.ReaderProfile.HourlyPrice, dbo.ReaderProfile.Title, dbo.[User].Uid
GO

Drop VIEW [dbo].[BookList]
Go
CREATE VIEW [dbo].[BookList]
AS
SELECT dbo.Book.Id, dbo.Book.BookStartTime, dbo.Book.ScriptFile, dbo.Book.BookEndTime, User_1.UserName as ReaderName, User_1.IsLogin as ReaderIsLogin, dbo.ReaderProfile.Title, dbo.ReaderProfile.HourlyPrice, dbo.ReaderProfile.Skills, dbo.ReaderProfile.About, dbo.ReaderProfile.VoiceType, dbo.ReaderProfile.Others
FROM     dbo.Book INNER JOIN
                  dbo.[User] ON dbo.Book.ActorUid = dbo.[User].Uid INNER JOIN
                  dbo.[User] AS User_1 ON dbo.Book.ReaderUid = User_1.Uid INNER JOIN
                  dbo.ReaderProfile ON User_1.Uid = dbo.ReaderProfile.ReaderUid
GO
USE [ScoopenDB]
GO

/****** Object:  StoredProcedure [dbo].[spGetAllLocakedUserAccounts]    Script Date: 12-09-2020 14:29:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create proc [dbo].[spGetAllLocakedUserAccounts]
as
Begin
 Select UserName, Email, LockedDateTime,
 DATEDIFF(hour, LockedDateTime, GETDATE()) as HoursElapsed
 from tblUsers
 where IsLocked = 1
End 


GO



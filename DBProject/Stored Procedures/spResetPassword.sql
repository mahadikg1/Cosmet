USE [ScoopenDB]
GO

/****** Object:  StoredProcedure [dbo].[spResetPassword]    Script Date: 12-09-2020 14:30:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create proc [dbo].[spResetPassword]
@UserName nvarchar(100)
as
Begin
 Declare @UserId int
 Declare @Email nvarchar(100)
 
 Select @UserId = Id, @Email = Email 
 from tblUsers
 where UserName = @UserName
 
 if(@UserId IS NOT NULL)
 Begin
  --If username exists
  Declare @GUID UniqueIdentifier
  Set @GUID = NEWID()
  
  Insert into tblResetPasswordRequests
  (Id, UserId, ResetRequestDateTime)
  Values(@GUID, @UserId, GETDATE())
  
  Select 1 as ReturnCode, @GUID as UniqueId, @Email as Email
 End
 Else
 Begin
  --If username does not exist
  SELECT 0 as ReturnCode, NULL as UniqueId, NULL as Email
 End
End


GO



USE [ScoopenDB]
GO

/****** Object:  StoredProcedure [dbo].[spChangePassword]    Script Date: 12-09-2020 14:28:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[spChangePassword]
@GUID uniqueidentifier,
@Password nvarchar(100)
as
Begin
 Declare @UserId int
 
 Select @UserId = UserId 
 from tblResetPasswordRequests
 where Id= @GUID
 
 if(@UserId is null)
 Begin
  -- If UserId does not exist
  Select 0 as IsPasswordChanged
 End
 Else
 Begin
  -- If UserId exists, Update with new password
  Update tblUsers set
  [Password] = @Password
  where Id = @UserId
  
  -- Delete the password reset request row 
  Delete from tblResetPasswordRequests
  where Id = @GUID
  
  Select 1 as IsPasswordChanged
 End
End


GO



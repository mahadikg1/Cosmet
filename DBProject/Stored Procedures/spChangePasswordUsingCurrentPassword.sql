USE [ScoopenDB]
GO

/****** Object:  StoredProcedure [dbo].[spChangePasswordUsingCurrentPassword]    Script Date: 12-09-2020 14:28:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[spChangePasswordUsingCurrentPassword]
@UserName nvarchar(100),
@CurrentPassword nvarchar(100),
@NewPassword nvarchar(100)
as
Begin
 if(Exists(Select Id from tblUsers 
     where UserName = @UserName
     and [Password] = @CurrentPassword))
 Begin
  Update tblUsers
  Set [Password] = @NewPassword
  where UserName = @UserName
  
  Select 1 as IsPasswordChanged
 End
 Else
 Begin
  Select 0 as IsPasswordChanged
 End
End 


GO



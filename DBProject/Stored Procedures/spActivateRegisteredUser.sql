USE [ScoopenDB]
GO

/****** Object:  StoredProcedure [dbo].[spActivateRegisteredUser]    Script Date: 12-09-2020 14:28:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[spActivateRegisteredUser]
@Mobile nvarchar(10),
@Password nvarchar(50),
@Email varchar(100),
@Otp varchar(6)
as  
Begin  
 Declare @Count int  
 Declare @ReturnCode int  
   
 Select @Count = COUNT(UserName)   
 from tblUsers where Mobile = @Mobile  
 If @Count > 0  
 Begin  
  declare @IsMobileConfirmed bit, @IsEmailConfirmed bit
  exec usp_ConfirmMobile @Mobile, @Otp, @IsMobileConfirmed output
  exec usp_ConfirmEmail @Email, @Otp, @IsEmailConfirmed output

  if @IsEmailConfirmed = 1 or @IsMobileConfirmed = 1
	begin
		Set @ReturnCode = 1
		update tblusers set IsActive = 1, Password = @Password where Mobile = @Mobile  
	end 
   else
	begin
		Set @ReturnCode = -1
	end
 End  
 Else  
 Begin  
  Set @ReturnCode = -1  
 End  
 Select @ReturnCode as ReturnValue  
End  

GO



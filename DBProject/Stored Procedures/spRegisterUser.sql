USE [ScoopenDB]
GO

/****** Object:  StoredProcedure [dbo].[spRegisterUser]    Script Date: 12-09-2020 14:29:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[spRegisterUser]  
@FirstName nvarchar(100),  
@LastName nvarchar (200),  
@Mobile nvarchar(10),
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
  Set @ReturnCode = -1  
 End  
 Else  
 Begin  
  Set @ReturnCode = 1  
  --Change: Column list specified while inserting
  Insert into tblUsers([UserName], Mobile, Email, RegistrationOtp) 
  values  (@Mobile, @Mobile, @Email, @Otp)  
  insert into tblUserInfo (UserId, FirstName, LastName) values(scope_identity(), @FirstName, @LastName)
 End  
 Select @ReturnCode as ReturnValue  
End  


GO



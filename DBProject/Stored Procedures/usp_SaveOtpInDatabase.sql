USE [ScoopenDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_SaveOtpInDatabase]    Script Date: 12-09-2020 14:31:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[usp_SaveOtpInDatabase]
@Mobile varchar(10), @Email varchar(100),@Otp varchar(6)
as
begin
   if exists (select Mobile, Email from tblUsers where Email = @Email and Mobile = @Mobile)
	begin
		update tblusers set RegistrationOtp = @Otp where Email = @Email and Mobile = @Mobile
	end
end


GO



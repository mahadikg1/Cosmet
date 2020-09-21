USE [ScoopenDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetOtpFromDatabase]    Script Date: 12-09-2020 14:30:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[usp_GetOtpFromDatabase]
@Mobile varchar(10), @Email varchar(100),@Otp varchar(6) out
as
begin
   if exists (select Mobile, Email from tblUsers where Email = @Email and Mobile = @Mobile)
	begin
		select @otp = RegistrationOtp from tblUsers where Email = @Email and Mobile = @Mobile
	end
end

GO



USE [ScoopenDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_ConfirmEmail]    Script Date: 12-09-2020 14:30:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[usp_ConfirmEmail]
@Email varchar(10), @Otp varchar(6), @IsSuccess bit out
as
begin
if exists (select Email, RegistrationOtp from tblUsers where Email = @Email and RegistrationOtp = @Otp)
	begin
		update tblUsers set IsConfirmedEmail = 1 where Email = @Email and RegistrationOtp = @Otp
		set @IsSuccess = 1
	end
else
	begin
	set @IsSuccess = 0
	end
end


GO



USE [ScoopenDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_ConfirmMobile]    Script Date: 12-09-2020 14:30:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[usp_ConfirmMobile]
@Mobile varchar(10), @Otp varchar(6), @IsSuccess bit out
as
begin
if exists (select Mobile, RegistrationOtp from tblUsers where Mobile = @Mobile and RegistrationOtp = @Otp)
	begin
		update tblUsers set IsConfirmedMobile = 1 where Mobile = @Mobile and RegistrationOtp = @Otp
		set @IsSuccess = 1
	end
else
	begin
	set @IsSuccess = 0
	end
end

GO



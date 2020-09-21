USE [ScoopenDB]
GO

/****** Object:  Table [dbo].[tblUsers]    Script Date: 12-09-2020 14:25:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NULL,
	[Password] [nvarchar](200) NULL,
	[Email] [nvarchar](100) NULL,
	[RetryAttempts] [int] NULL,
	[IsLocked] [bit] NULL,
	[LockedDateTime] [datetime] NULL,
	[Mobile] [varchar](10) NULL,
	[IsActive] [bit] NULL,
	[IsConfirmedMobile] [bit] NULL,
	[IsConfirmedEmail] [bit] NULL,
	[RegistrationOtp] [varchar](6) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



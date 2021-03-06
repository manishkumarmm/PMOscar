
/****** Object:  Table [dbo].[user_20180413]    Script Date: 4/19/2018 12:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_20180413](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[MiddleName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[EmailId] [varchar](50) NULL,
	[CreatedBy] [int] NOT NULL,
	[Createddate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[UserRoleID] [int] NULL,
	[IsActive] [bit] NULL,
	[EmployeeCode] [varchar](25) NULL
) ON [PRIMARY]
GO

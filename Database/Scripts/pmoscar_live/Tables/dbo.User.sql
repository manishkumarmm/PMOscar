if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[User]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[User]
GO

CREATE TABLE [dbo].[User] (
	[UserId] [int] IDENTITY (1, 1) NOT NULL ,
	[UserName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Password] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[FirstName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[MiddleName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[LastName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[EmailId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[CreatedBy] [int] NOT NULL ,
	[Createddate] [datetime] NOT NULL ,
	[UpdatedBy] [int] NULL ,
	[UpdatedDate] [datetime] NULL ,
	[UserRoleID] [int] NULL ,
	[IsActive] [bit] NULL ,
	CONSTRAINT [PK_User] PRIMARY KEY  CLUSTERED 
	(
		[UserId]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO



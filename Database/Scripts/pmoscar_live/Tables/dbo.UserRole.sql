if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UserRole]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[UserRole]
GO

CREATE TABLE [dbo].[UserRole] (
	[UserRoleID] [int] NOT NULL ,
	[UserRole] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	CONSTRAINT [PK_UserRole1] PRIMARY KEY  CLUSTERED 
	(
		[UserRoleID]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO



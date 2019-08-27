if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Role]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Role]
GO

CREATE TABLE [dbo].[Role] (
	[RoleId] [int] IDENTITY (1, 1) NOT NULL ,
	[Role] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ShortName] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Desription] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[EstimationPercentage] [decimal](18, 3) NULL ,
	[EstimationRoleID] [int] NULL ,
	CONSTRAINT [PK_Role] PRIMARY KEY  CLUSTERED 
	(
		[RoleId]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO



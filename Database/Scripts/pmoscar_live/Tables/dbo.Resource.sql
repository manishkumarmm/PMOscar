if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Resource]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Resource]
GO

CREATE TABLE [dbo].[Resource] (
	[ResourceId] [int] IDENTITY (1, 1) NOT NULL ,
	[ResourceName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[CreatedBy] [int] NOT NULL ,
	[CreatedDate] [datetime] NOT NULL ,
	[UpdatedBy] [int] NULL ,
	[UpdatedDate] [datetime] NULL ,
	[IsActive] [bit] NULL ,
	[RoleId] [int] NULL ,
	CONSTRAINT [PK_Resource] PRIMARY KEY  CLUSTERED 
	(
		[ResourceId]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO



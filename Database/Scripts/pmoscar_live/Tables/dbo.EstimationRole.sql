if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EstimationRole]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[EstimationRole]
GO

CREATE TABLE [dbo].[EstimationRole] (
	[EstimationRoleID] [int] IDENTITY (1, 1) NOT NULL ,
	[RoleName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ShortName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	CONSTRAINT [PK_EstimationRole] PRIMARY KEY  CLUSTERED 
	(
		[EstimationRoleID]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO



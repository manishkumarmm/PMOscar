if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Phase]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Phase]
GO

CREATE TABLE [dbo].[Phase] (
	[PhaseID] [int] NOT NULL ,
	[Phase] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ShortName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[CreatedBy] [int] NULL ,
	[CreatedDate] [datetime] NULL ,
	[UpdatedBy] [int] NULL ,
	[UpdatedDate] [datetime] NULL ,
	CONSTRAINT [PK_Phase] PRIMARY KEY  CLUSTERED 
	(
		[PhaseID]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Dashboard]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Dashboard]
GO

CREATE TABLE [dbo].[Dashboard] (
	[DashboardID] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[FromDate] [datetime] NULL ,
	[ToDate] [datetime] NULL ,
	[PeriodType] [nchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Status] [nchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	CONSTRAINT [PK_Dashboard] PRIMARY KEY  CLUSTERED 
	(
		[DashboardID]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO



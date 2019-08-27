if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TimeTracker]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[TimeTracker]
GO

CREATE TABLE [dbo].[TimeTracker] (
	[TimeTrackerId] [int] IDENTITY (1, 1) NOT NULL ,
	[ProjectId] [int] NOT NULL ,
	[ResourceId] [int] NOT NULL ,
	[RoleId] [int] NOT NULL ,
	[FromDate] [datetime] NOT NULL ,
	[ToDate] [datetime] NOT NULL ,
	[EstimatedHours] [decimal](18, 0) NULL ,
	[ActualHours] [decimal](18, 0) NULL ,
	[CreatedBy] [int] NOT NULL ,
	[CreatedDate] [datetime] NULL ,
	[UpdatedBy] [int] NULL ,
	[UpdatedDate] [datetime] NOT NULL ,
	[PhaseID] [int] NULL ,
	[WeeklyComments] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	CONSTRAINT [PK_TimeTracker] PRIMARY KEY  CLUSTERED 
	(
		[TimeTrackerId]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO



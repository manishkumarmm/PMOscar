if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TimeTrackerAudit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[TimeTrackerAudit]
GO

CREATE TABLE [dbo].[TimeTrackerAudit] (
	[TimeTrackerAuditID] [int] IDENTITY (1, 1) NOT NULL ,
	[TimeTrackerID] [int] NOT NULL ,
	[ProjectID] [int] NOT NULL ,
	[ResourceID] [int] NOT NULL ,
	[RoleID] [int] NOT NULL ,
	[PhaseID] [int] NULL ,
	[FromDate] [datetime] NULL ,
	[ToDate] [datetime] NULL ,
	[EstimatedHours] [int] NULL ,
	[ActualHours] [int] NULL ,
	[CreatedBy] [int] NULL ,
	[CreatedDate] [datetime] NULL ,
	[WeeklyComments] [nvarchar] (MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	CONSTRAINT [PK_TimeTrackerAudit1] PRIMARY KEY  CLUSTERED 
	(
		[TimeTrackerAuditID]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO



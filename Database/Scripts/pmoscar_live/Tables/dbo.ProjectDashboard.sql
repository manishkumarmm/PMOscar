if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProjectDashboard]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ProjectDashboard]
GO

CREATE TABLE [dbo].[ProjectDashboard] (
	[ProjectDashboardID] [int] IDENTITY (1, 1) NOT NULL ,
	[ProjectID] [int] NULL ,
	[PhaseId] [int] NULL ,
	[ClientStatus] [int] NULL ,
	[TimelineStatus] [int] NULL ,
	[BudgetStatus] [int] NULL ,
	[EscalateStatus] [int] NULL ,
	[CreatedBy] [int] NULL ,
	[CreatedDate] [datetime] NULL ,
	[UpdatedBy] [int] NULL ,
	[UpdatedDate] [datetime] NULL ,
	[Comments] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ProjectName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ShortName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ProjectType] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ProjectOwner] [int] NULL ,
	[ProjectManager] [int] NULL ,
	[DeliveryDate] [datetime] NULL ,
	[RevisedDeliveryDate] [datetime] NULL ,
	[ApprvChangeRequest] [int] NULL ,
	[PMComments] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[DeliveryComments] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[IsActive] [bit] NULL ,
	[DashboardID] [int] NULL ,
	CONSTRAINT [PK_ProjectDashboard] PRIMARY KEY  CLUSTERED 
	(
		[ProjectDashboardID]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO



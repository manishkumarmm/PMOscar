if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProjectDashboardEstimation]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ProjectDashboardEstimation]
GO

CREATE TABLE [dbo].[ProjectDashboardEstimation] (
	[ProjectDashboardEstimationID] [int] IDENTITY (1, 1) NOT NULL ,
	[ProjectID] [int] NULL ,
	[PhaseID] [int] NULL ,
	[EstimationRoleID] [int] NULL ,
	[BillableHours] [int] NULL ,
	[BudgetHours] [int] NULL ,
	[RevisedBudgetHours] [int] NULL ,
	[ActualHrs] [int] NULL ,
	[PeriodEstimetedHrs] [int] NULL ,
	[PeriodActualHrs] [int] NULL ,
	[PeriodEstimetedHrsAdjusted] [decimal](14, 4) NULL ,
	[PeriodActualHrsAdjusted] [decimal](14, 4) NULL ,
	[ActualHrsAdjusted] [decimal](14, 4) NULL ,
	[DashboardID] [int] NULL ,
	[EstRoleName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[EstRoleShrtName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[RoleName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[RoleShrtName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	CONSTRAINT [PK_ProjectDashboardEstimation] PRIMARY KEY  CLUSTERED 
	(
		[ProjectDashboardEstimationID]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO



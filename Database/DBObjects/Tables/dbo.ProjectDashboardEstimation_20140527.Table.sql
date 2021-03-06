
/****** Object:  Table [dbo].[ProjectDashboardEstimation_20140527]    Script Date: 4/19/2018 12:59:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectDashboardEstimation_20140527](
	[ProjectDashboardEstimationID] [int] IDENTITY(12154,1) NOT NULL,
	[ProjectID] [int] NULL,
	[PhaseID] [int] NULL,
	[EstimationRoleID] [int] NULL,
	[BillableHours] [int] NULL,
	[BudgetHours] [int] NULL,
	[RevisedBudgetHours] [int] NULL,
	[ActualHrs] [int] NULL,
	[PeriodEstimetedHrs] [int] NULL,
	[PeriodActualHrs] [int] NULL,
	[PeriodEstimetedHrsAdjusted] [decimal](14, 4) NULL,
	[PeriodActualHrsAdjusted] [decimal](14, 4) NULL,
	[ActualHrsAdjusted] [decimal](14, 4) NULL,
	[DashboardID] [int] NULL,
	[EstRoleName] [nvarchar](100) NULL,
	[EstRoleShrtName] [nvarchar](100) NULL,
	[RoleName] [nvarchar](100) NULL,
	[RoleShrtName] [nvarchar](100) NULL,
	[RevisedComments] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

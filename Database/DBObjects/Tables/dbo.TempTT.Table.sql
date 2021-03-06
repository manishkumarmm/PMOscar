
/****** Object:  Table [dbo].[TempTT]    Script Date: 4/19/2018 12:59:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempTT](
	[ResourceName] [varchar](250) NULL,
	[ResourceId] [int] NULL,
	[Role] [varchar](250) NULL,
	[ProjectName] [varchar](250) NULL,
	[Estimated] [int] NULL,
	[Actual] [int] NULL,
	[ESTTotal] [int] NULL,
	[ACTTotal] [int] NULL,
	[BudgetHours] [int] NULL,
	[WeeklyComments] [varchar](max) NULL,
	[ProjectId] [int] NULL,
	[EstimationPercentage] [int] NULL,
	[EstimationRoleID] [int] NULL,
	[Status] [varchar](2) NULL,
	[PHCount] [int] NULL,
	[RoleID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

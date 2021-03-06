
/****** Object:  Table [dbo].[ProjectDashboard_MADHU_5JUNE2017]    Script Date: 4/19/2018 12:59:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectDashboard_MADHU_5JUNE2017](
	[ProjectDashboardID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NULL,
	[PhaseId] [int] NULL,
	[ClientStatus] [int] NULL,
	[TimelineStatus] [int] NULL,
	[BudgetStatus] [int] NULL,
	[EscalateStatus] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[Comments] [nvarchar](max) NULL,
	[ProjectName] [varchar](50) NULL,
	[ShortName] [varchar](50) NULL,
	[ProjectType] [varchar](50) NULL,
	[ProjectOwner] [int] NULL,
	[ProjectManager] [int] NULL,
	[DeliveryDate] [datetime] NULL,
	[RevisedDeliveryDate] [datetime] NULL,
	[ApprvChangeRequest] [int] NULL,
	[PMComments] [nvarchar](max) NULL,
	[DeliveryComments] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[DashboardID] [int] NULL,
	[POComments] [nvarchar](max) NULL,
	[Utilization] [char](1) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

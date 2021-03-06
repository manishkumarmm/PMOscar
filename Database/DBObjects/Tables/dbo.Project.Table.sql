
/****** Object:  Table [dbo].[Project]    Script Date: 4/19/2018 12:59:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[ProjectId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [varchar](50) NOT NULL,
	[ShortName] [varchar](50) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[PhaseID] [int] NULL,
	[ProjectType] [char](1) NULL,
	[ProjectOwner] [int] NULL,
	[ProjectManager] [int] NULL,
	[DeliveryDate] [datetime] NULL,
	[RevisedDeliveryDate] [datetime] NULL,
	[ApprvChangeRequest] [int] NULL,
	[PMComments] [nvarchar](max) NULL,
	[DeliveryComments] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[ProjectCode] [varchar](50) NULL,
	[ProjStartDate] [datetime] NULL,
	[MaintClosingDate] [datetime] NULL,
	[POComments] [nvarchar](max) NULL,
	[DevURL] [nvarchar](200) NULL,
	[DemoURL] [nvarchar](200) NULL,
	[QaURL] [nvarchar](200) NULL,
	[ProductionURL] [nvarchar](200) NULL,
	[ClientId] [int] NULL,
	[ProgId] [int] NULL,
	[Utilization] [char](1) NULL,
	[ProjUniqueCode] [varchar](50) NULL,
	[CostCentreID] [int] NULL,
	[ShowInDashboard] [int] NULL,
	[SVNPath] [nvarchar](500) NULL,
	[ProjectDetails] [nvarchar](max) NULL,
	[BudgetRevisionRequired] [bit] NULL,
	[WorkOrderID] [int] NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DF_Project_ShowInDashboard]  DEFAULT ((1)) FOR [ShowInDashboard]
GO
ALTER TABLE [dbo].[Project] ADD  DEFAULT ((1)) FOR [BudgetRevisionRequired]
GO

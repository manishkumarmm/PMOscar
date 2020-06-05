USE [PMOscar_Dev]
GO

/****** Object:  Table [dbo].[ProjectActivityStatus]    Script Date: 6/3/2020 7:53:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProjectActivityStatus](
	[ProjectActivityStatusID] [int] IDENTITY(1,1) NOT NULL,
	[ClientStatus] [int] NULL,
	[TimelineStatus] [int] NULL,
	[BudgetStatus] [int] NULL,
	[EscalateStatus] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[ProjectDashboardID] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[ProjectID] [int] NULL,
	[DashboardID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProjectActivityStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



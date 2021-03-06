
/****** Object:  Table [dbo].[TimeTrackerAudit]    Script Date: 4/19/2018 12:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeTrackerAudit](
	[TimeTrackerAuditID] [int] IDENTITY(1,1) NOT NULL,
	[TimeTrackerID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[ResourceID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[PhaseID] [int] NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[EstimatedHours] [int] NULL,
	[ActualHours] [decimal](18, 2) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[WeeklyComments] [nvarchar](max) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[TeamID] [int] NULL,
 CONSTRAINT [PK_TimeTrackerAudit1] PRIMARY KEY CLUSTERED 
(
	[TimeTrackerAuditID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

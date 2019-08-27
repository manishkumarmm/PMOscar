
/****** Object:  Table [dbo].[TimeTracker_20131015]    Script Date: 4/19/2018 12:59:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeTracker_20131015](
	[TimeTrackerId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[ResourceId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[FromDate] [datetime] NOT NULL,
	[ToDate] [datetime] NOT NULL,
	[EstimatedHours] [decimal](18, 0) NULL,
	[ActualHours] [decimal](18, 0) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[PhaseID] [int] NULL,
	[WeeklyComments] [nvarchar](max) NULL,
	[TeamID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

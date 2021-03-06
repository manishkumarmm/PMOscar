
/****** Object:  Table [dbo].[TimeTrackerActualHoursHistory]    Script Date: 4/19/2018 12:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeTrackerActualHoursHistory](
	[TimeTrackerActualHoursHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[TimeTrackerID] [int] NOT NULL,
	[OldActualHours] [decimal](5, 2) NULL,
	[NewActualHours] [decimal](5, 2) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TimeTrackerActualHoursHistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TimeTrackerActualHoursHistory] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO

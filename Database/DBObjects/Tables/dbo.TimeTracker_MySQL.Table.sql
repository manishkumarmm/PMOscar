
/****** Object:  Table [dbo].[TimeTracker_MySQL]    Script Date: 4/19/2018 12:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeTracker_MySQL](
	[ID] [bigint] NOT NULL,
	[UserId] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[Start] [time](7) NULL,
	[Duration] [time](7) NULL,
	[ClientId] [int] NULL,
	[ProjectId] [int] NULL,
	[ActivityId] [int] NOT NULL,
	[TaskId] [int] NULL,
	[InvoiceId] [int] NULL,
	[Billable] [bit] NULL,
	[PMOscarProjectID] [int] NULL,
	[EmployeeCode] [varchar](25) NULL,
	[Importdate] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TimeTracker_MySQL] ADD  DEFAULT (getdate()) FOR [Importdate]
GO

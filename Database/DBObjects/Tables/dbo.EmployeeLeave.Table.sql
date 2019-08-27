
/****** Object:  Table [dbo].[EmployeeLeave]    Script Date: 4/19/2018 12:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeLeave](
	[EmployeeCode] [varchar](50) NOT NULL,
	[NumberOfLeave] [decimal](4, 2) NULL,
	[LeaveFromDate] [datetime] NULL,
	[LeaveToDate] [datetime] NULL,
	[Leave_status] [smallint] NULL
) ON [PRIMARY]
GO

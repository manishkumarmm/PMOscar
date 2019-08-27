DROP TABLE [EmployeeLeave]
CREATE TABLE [dbo].[EmployeeLeave](
	[EmployeeCode] [varchar](50) NOT NULL,
	[NumberOfLeave] [decimal](4, 2) NULL,
	[LeaveFromDate] [datetime] NULL,
	[LeaveToDate] [datetime] NULL,
	[Leave_status] [smallint] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[EmployeeLeave]') AND type in (N'U'))
DROP TABLE [EmployeeLeave]
GO


USE [PMOscar_Dev]
GO

/****** Object:  Table [dbo].[EmployeeLeave]    Script Date: 08/10/2016 10:56:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

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



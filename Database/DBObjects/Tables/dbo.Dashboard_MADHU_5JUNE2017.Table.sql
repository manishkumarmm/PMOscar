
/****** Object:  Table [dbo].[Dashboard_MADHU_5JUNE2017]    Script Date: 4/19/2018 12:59:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dashboard_MADHU_5JUNE2017](
	[DashboardID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[PeriodType] [nchar](1) NULL,
	[Status] [nchar](1) NULL
) ON [PRIMARY]
GO

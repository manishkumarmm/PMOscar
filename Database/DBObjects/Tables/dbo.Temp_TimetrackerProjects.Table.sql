
/****** Object:  Table [dbo].[Temp_TimetrackerProjects]    Script Date: 4/19/2018 12:59:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Temp_TimetrackerProjects](
	[id] [int] NULL,
	[team_id] [int] NOT NULL,
	[name] [varchar](80) NOT NULL,
	[description] [varchar](255) NULL,
	[tasks] [text] NULL,
	[activities] [text] NOT NULL,
	[status] [numeric](3, 0) NULL,
	[PMOscarProjectID] [int] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

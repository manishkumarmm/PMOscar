
/****** Object:  Table [dbo].[TempBackpopulateProjects]    Script Date: 4/19/2018 12:59:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempBackpopulateProjects](
	[Projectname] [varchar](50) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[ProjStartDate] [datetime] NULL,
	[MaintClosingDate] [datetime] NULL
) ON [PRIMARY]
GO

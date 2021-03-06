USE [PMOscarN2NTestEvn]
GO

/****** Object:  Table [dbo].[OhLog]    Script Date: 12/9/2019 6:47:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OhLog](
	[OhId] [int] IDENTITY(1,1) NOT NULL,
	[Year] [int] NOT NULL,
	[emp_Code] [nchar](10) NULL,
	[Holidays] [nvarchar](300) NULL,
 CONSTRAINT [PK_OhLog] PRIMARY KEY CLUSTERED 
(
	[OhId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



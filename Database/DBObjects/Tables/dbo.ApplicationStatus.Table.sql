IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ApplicationStatus') AND type in (N'U', N'U'))
DROP TABLE [dbo].[ApplicationStatus]
/****** Object:  Table [dbo].[ApplicationStatus]    Script Date: 4/19/2018 12:59:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationStatus](
	[ApplicationStatusID] [int] NOT NULL,
	[StatusType] [varchar](50) NOT NULL,
	[Status] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ApplicationStatus] PRIMARY KEY CLUSTERED 
(
	[ApplicationStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

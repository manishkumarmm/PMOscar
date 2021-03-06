
/****** Object:  Table [dbo].[CR$]    Script Date: 4/19/2018 12:59:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CR$](
	[Project ID] [float] NULL,
	[CR#] [nvarchar](255) NULL,
	[PM] [nvarchar](255) NULL,
	[Client Ref #] [nvarchar](255) NULL,
	[Projects] [nvarchar](255) NULL,
	[Project Type] [nvarchar](255) NULL,
	[Proposal #] [nvarchar](255) NULL,
	[Work Order#] [nvarchar](255) NULL,
	[Client Approval #] [nvarchar](255) NULL,
	[CR amount] [float] NULL,
	[CR hours] [nvarchar](255) NULL,
	[Remarks] [nvarchar](255) NULL
) ON [PRIMARY]
GO

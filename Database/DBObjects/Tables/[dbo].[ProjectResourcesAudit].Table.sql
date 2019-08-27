
GO

/****** Object:  Table [dbo].[ProjectResources]    Script Date: 12/12/2018 4:55:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProjectResourcesAudit](
	[ProjectResourceAuditID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectResourcesID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[ResourceID] [int] NOT NULL,
	[IsActive] [bit] NULL DEFAULT ((0)),
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy][int] NULL,
	[ModifiedDate][datetime] NULL,
	[IsAllocated][bit] NULL
PRIMARY KEY CLUSTERED 
(
	[ProjectResourceAuditID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ProjectResources]  WITH CHECK ADD FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectId])
GO

ALTER TABLE [dbo].[ProjectResources]  WITH CHECK ADD FOREIGN KEY([ResourceID])
REFERENCES [dbo].[Resource] ([ResourceId])
GO

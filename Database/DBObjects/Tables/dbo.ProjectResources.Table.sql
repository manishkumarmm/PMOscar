
/****** Object:  Table [dbo].[ProjectResources]    Script Date: 4/19/2018 12:59:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectResources](
	[ProjectResourcesID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[ResourceID] [int] NOT NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProjectResourcesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProjectResources] ADD  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ProjectResources]  WITH CHECK ADD FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectId])
GO
ALTER TABLE [dbo].[ProjectResources]  WITH CHECK ADD FOREIGN KEY([ResourceID])
REFERENCES [dbo].[Resource] ([ResourceId])
GO

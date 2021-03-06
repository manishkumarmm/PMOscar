
/****** Object:  Table [dbo].[EmailConfiguration]    Script Date: 4/19/2018 12:59:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailConfiguration](
	[EmailConfigID] [int] IDENTITY(1,1) NOT NULL,
	[EntityType] [varchar](50) NOT NULL,
	[Action] [varchar](50) NOT NULL,
	[RoleID] [int] NOT NULL,
	[Template] [nvarchar](100) NOT NULL,
	[OnOffFlag] [bit] NOT NULL,
 CONSTRAINT [PK_EmailConfiguration] PRIMARY KEY CLUSTERED 
(
	[EmailConfigID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EmailConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_EmailConfiguration_UserRole] FOREIGN KEY([RoleID])
REFERENCES [dbo].[UserRole] ([UserRoleID])
GO
ALTER TABLE [dbo].[EmailConfiguration] CHECK CONSTRAINT [FK_EmailConfiguration_UserRole]
GO

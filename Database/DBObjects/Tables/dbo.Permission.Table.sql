
/****** Object:  Table [dbo].[Permission]    Script Date: 4/19/2018 12:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[PermissionID] [int] IDENTITY(1,1) NOT NULL,
	[Namespace] [nvarchar](128) NOT NULL,
	[Controller] [nvarchar](64) NOT NULL,
	[Action] [nvarchar](64) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CreatedByUserID] [int] NULL,
	[DateUpdated] [datetime] NULL,
	[UpdatedByUserID] [int] NULL,
	[Deleted] [bit] NOT NULL,
	[DateDeleted] [datetime] NULL,
	[DeletedByUserID] [int] NULL,
	[Name] [nvarchar](100) NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Permission] ADD  CONSTRAINT [DF__Permissio__Delet__4460231C]  DEFAULT ((0)) FOR [Deleted]
GO

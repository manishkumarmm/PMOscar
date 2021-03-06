
/****** Object:  Table [dbo].[EstimationRole]    Script Date: 4/19/2018 12:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstimationRole](
	[EstimationRoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
	[ShortName] [nvarchar](50) NULL,
 CONSTRAINT [PK_EstimationRole] PRIMARY KEY CLUSTERED 
(
	[EstimationRoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

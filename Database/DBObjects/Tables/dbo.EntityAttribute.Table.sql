
/****** Object:  Table [dbo].[EntityAttribute]    Script Date: 4/19/2018 12:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntityAttribute](
	[EntityAttributeID] [bigint] IDENTITY(1,1) NOT NULL,
	[EntityID] [bigint] NOT NULL,
	[EntityAttributeName] [varchar](250) NOT NULL,
 CONSTRAINT [PK_EntityAttributeEntityAttributeID] PRIMARY KEY CLUSTERED 
(
	[EntityAttributeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

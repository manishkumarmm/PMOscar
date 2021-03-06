
/****** Object:  Table [dbo].[AuditAttribute]    Script Date: 4/19/2018 12:59:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditAttribute](
	[AuditAttributeID] [bigint] IDENTITY(1,1) NOT NULL,
	[AuditID] [bigint] NOT NULL,
	[EntityAttributeID] [bigint] NOT NULL,
	[OldValue] [varchar](max) NULL,
	[NewValue] [varchar](max) NULL,
 CONSTRAINT [PK_AuditAttributeAuditAttributeID] PRIMARY KEY CLUSTERED 
(
	[AuditAttributeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

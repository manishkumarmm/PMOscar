
/****** Object:  Table [dbo].[Audit]    Script Date: 4/19/2018 12:59:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Audit](
	[AuditID] [bigint] IDENTITY(1,1) NOT NULL,
	[ActionID] [bigint] NOT NULL,
	[EntityID] [bigint] NOT NULL,
	[RecordID] [nvarchar](300) NOT NULL,
	[AuditUserID] [nvarchar](100) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
 CONSTRAINT [PK_AuditAuditID] PRIMARY KEY CLUSTERED 
(
	[AuditID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

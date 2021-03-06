
/****** Object:  Table [dbo].[ProjectAudit]    Script Date: 4/19/2018 12:59:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectAudit](
	[ProjectAuditID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NULL,
	[PhaseID] [int] NULL,
	[ProjectName] [varchar](20) NULL,
	[ShortName] [varchar](20) NULL,
	[ProjectType] [char](1) NULL,
	[ProjectOwner] [int] NULL,
	[ProjectManager] [int] NULL,
	[DeliveryDate] [datetime] NULL,
	[RevisedDeliveryDate] [datetime] NULL,
	[PMComments] [nvarchar](max) NULL,
	[DeliveryComments] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ApprvChangeRequest] [int] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ProjectAudit] PRIMARY KEY CLUSTERED 
(
	[ProjectAuditID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

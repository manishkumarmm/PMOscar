
/****** Object:  Table [dbo].[BillingDetailsAudit]    Script Date: 4/19/2018 12:59:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillingDetailsAudit](
	[BillingAuditID] [int] IDENTITY(1,1) NOT NULL,
	[BillingID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[ResourceID] [int] NOT NULL,
	[PlannedHours] [int] NULL,
	[ActualHours] [int] NULL,
	[fromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[Status] [varchar](1) NULL,
 CONSTRAINT [PK_BillingDetailsAudit] PRIMARY KEY CLUSTERED 
(
	[BillingAuditID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

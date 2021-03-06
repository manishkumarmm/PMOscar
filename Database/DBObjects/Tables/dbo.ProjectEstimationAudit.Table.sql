
/****** Object:  Table [dbo].[ProjectEstimationAudit]    Script Date: 4/19/2018 12:59:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectEstimationAudit](
	[ProjEstimationAuditID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectEstimationID] [int] NULL,
	[ProjectID] [int] NULL,
	[EstimationRoleID] [int] NULL,
	[PhaseID] [int] NULL,
	[BillableHours] [int] NULL,
	[BudgetHours] [int] NULL,
	[RevisedBudgetHours] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[Comments] [nvarchar](max) NULL,
	[NewBillableHours] [int] NOT NULL,
	[NewBudgetHours] [int] NOT NULL,
	[NewRevisedBudgetHours] [int] NOT NULL,
 CONSTRAINT [PK_ProjectEstimationAudit] PRIMARY KEY CLUSTERED 
(
	[ProjEstimationAuditID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProjectEstimationAudit] ADD  DEFAULT ((0)) FOR [NewBillableHours]
GO
ALTER TABLE [dbo].[ProjectEstimationAudit] ADD  DEFAULT ((0)) FOR [NewBudgetHours]
GO
ALTER TABLE [dbo].[ProjectEstimationAudit] ADD  DEFAULT ((0)) FOR [NewRevisedBudgetHours]
GO

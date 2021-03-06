IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'BudgetRevisionDetails') AND type in (N'U', N'U'))
DROP TABLE [dbo].[BudgetRevisionDetails]
/****** Object:  Table [dbo].[BudgetRevisionDetails]    Script Date: 8/01/2019 12:59:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[BudgetRevisionDetails](
	[BudgetRevisionDetailsID] [int] IDENTITY(1,1) NOT NULL,
	[BudgetRevisionID] [int] NOT NULL,
	[PhaseID] [int] NOT NULL,
	[ResourceID] [int] NULL,
	[EstimationRoleID] [int] NOT NULL,
	[BillableHours] [decimal](6, 2) NULL,
	[EstimatedHours] [decimal](6, 2) NULL,
	[ProductivityAdjustmentHours] [decimal](6, 2) NULL,
	[Overrun] [decimal](6, 2) NULL,
	[BudgetHours] [decimal](6, 2) NULL,
	[Buffer] [decimal](6, 2) NULL,
	[RevisedBudgetHours] [decimal](6, 2) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[Comments] [varchar](max) NULL,
	[IsChangeRequest] [bit] NULL,
	[ActualHours] [decimal](18, 2) NULL,
	[IsApproved] [int] NULL,
	[IsInitialBudget] [int] NOT NULL,
	[IsReallocated] [int] NOT NULL,
	[AdditionalBudgetHours] [decimal](6, 2) NULL,
 CONSTRAINT [PK_BudgetRevisionDetails] PRIMARY KEY CLUSTERED 
(
	[BudgetRevisionDetailsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[BudgetRevisionDetails] ADD  CONSTRAINT [DF_BudgetRevisionDetails_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[BudgetRevisionDetails] ADD  DEFAULT ((0)) FOR [IsInitialBudget]
GO

ALTER TABLE [dbo].[BudgetRevisionDetails] ADD  DEFAULT ((0)) FOR [IsReallocated]
GO

ALTER TABLE [dbo].[BudgetRevisionDetails]  WITH CHECK ADD FOREIGN KEY([BudgetRevisionID])
REFERENCES [dbo].[BudgetRevisionLog] ([BudgetRevisionID])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[BudgetRevisionDetails]  WITH CHECK ADD FOREIGN KEY([EstimationRoleID])
REFERENCES [dbo].[EstimationRole] ([EstimationRoleID])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[BudgetRevisionDetails]  WITH CHECK ADD FOREIGN KEY([PhaseID])
REFERENCES [dbo].[Phase] ([PhaseID])
ON UPDATE CASCADE
GO
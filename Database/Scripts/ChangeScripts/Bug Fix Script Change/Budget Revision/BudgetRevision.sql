IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetRevision]') AND type in (N'U'))
DROP TABLE [BudgetRevision]
GO

/****** Object:  Table [dbo].[BudgetRevision]    Script Date: 11/27/2015 14:47:35 ******/

CREATE TABLE [dbo].[BudgetRevision](
	[BudgetRevisionID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectEstimationID] [int] NOT NULL,
	[BillableHours] [int] NULL,
	[BudgetHours] [int] NULL,
	[IsChangeRequest] [bit] NOT NULL,
	[Reason] [varchar](max) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[IsApproved] [bit] NOT NULL,
 CONSTRAINT [PK_BudgetRevision] PRIMARY KEY CLUSTERED 
(
	[BudgetRevisionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[BudgetRevision]  WITH CHECK ADD  CONSTRAINT [FK_BudgetRevision_ProjectEstimation] FOREIGN KEY([ProjectEstimationID])
REFERENCES [dbo].[ProjectEstimation] ([ProjectEstimationID])
GO

ALTER TABLE [dbo].[BudgetRevision] CHECK CONSTRAINT [FK_BudgetRevision_ProjectEstimation]
GO

ALTER TABLE [dbo].[BudgetRevision] ADD  CONSTRAINT [DF_BudgetRevision_IsChangeRequest]  DEFAULT ((0)) FOR [IsChangeRequest]
GO

ALTER TABLE [dbo].[BudgetRevision] ADD  CONSTRAINT [DF_BudgetRevision_IsApproved]  DEFAULT ((0)) FOR [IsApproved]
GO



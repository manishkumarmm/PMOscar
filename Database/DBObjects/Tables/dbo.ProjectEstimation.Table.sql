
/****** Object:  Table [dbo].[ProjectEstimation]    Script Date: 4/19/2018 12:59:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectEstimation](
	[ProjectEstimationID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NULL,
	[PhaseID] [int] NULL,
	[EstimationRoleID] [int] NULL,
	[BillableHours] [int] NULL,
	[BudgetHours] [int] NULL,
	[RevisedBudgetHours] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[Comments] [nvarchar](max) NULL,
 CONSTRAINT [PK_ProjectEstimation] PRIMARY KEY CLUSTERED 
(
	[ProjectEstimationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetRevisionHistory]') AND type in (N'U'))
DROP TABLE [BudgetRevisionHistory]
GO


/****** Object:  Table [dbo].[BudgetRevision]    Script Date: 11/27/2015 14:47:35 ******/

CREATE TABLE [dbo].[BudgetRevisionHistory](
	[BudgetRevisionHistoryID][int] IDENTITY(1,1) NOT NULL,
	[BudgetRevisionID] [int] NOT NULL,
	[ProjectEstimationID] [int] NOT NULL,
	[BillableHours] [int] NULL,
	[BudgetHours] [int] NULL,
	[IsChangeRequest] [bit] NOT NULL,
	[Reason] [varchar](max) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[IsApproved] [bit] NULL,
	[InsertedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_BudgetRevisionHistory] PRIMARY KEY CLUSTERED 
(
	[BudgetRevisionHistoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


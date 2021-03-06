IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'BudgetRevisionLog') AND type in (N'U', N'U'))
DROP TABLE [dbo].[BudgetRevisionLog]
/****** Object:  Table [dbo].[BudgetRevisionLog]    Script Date: 8/01/2019 12:59:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BudgetRevisionLog](
	[BudgetRevisionID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[Comments] [varchar](max) NULL,
	[Purpose] [varchar](max) NULL,
	[IsPendingApproval] [bit] NULL,
	[IsApproved] [bit] NULL,
	[IsRejected] [bit] NULL,
	[IsInitialRevision] [bit] NULL,
	[IsMailSent] [bit] NULL,
 CONSTRAINT [PK_BudgetRevision] PRIMARY KEY CLUSTERED 
(
	[BudgetRevisionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[BudgetRevisionLog]  WITH CHECK ADD FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectId])
ON UPDATE CASCADE
GO


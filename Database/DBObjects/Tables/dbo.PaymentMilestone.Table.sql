
/****** Object:  Table [dbo].[PaymentMilestone]    Script Date: 4/19/2018 12:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMilestone](
	[PaymentMilestoneID] [int] IDENTITY(1,1) NOT NULL,
	[MileStoneNo] [int] NULL,
	[MilestoneLabel] [varchar](max) NULL,
	[MilestoneDate] [datetime] NULL,
	[RevisedMilestoneDate] [datetime] NULL,
	[PercnetageAmtForMileStone] [decimal](18, 2) NULL,
	[MilestoneHrs] [decimal](18, 2) NULL,
	[Currency] [int] NULL,
	[InvoicedHrs] [decimal](18, 2) NULL,
	[Remarks] [varchar](max) NULL,
	[WorkOrderID] [int] NULL,
	[CR_No] [int] NULL,
	[Status] [int] NULL,
	[InvoicesRaisedOn] [datetime] NULL,
	[MilestoneMetCommunicationDate] [datetime] NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[PaymentMilestoneID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[PaymentMilestone] ADD  DEFAULT (NULL) FOR [InvoicesRaisedOn]
GO
ALTER TABLE [dbo].[PaymentMilestone] ADD  DEFAULT (NULL) FOR [MilestoneMetCommunicationDate]
GO

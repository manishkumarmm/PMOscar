
/****** Object:  Table [dbo].[WorkOrder]    Script Date: 4/19/2018 12:59:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkOrder](
	[WorkOrderID] [int] IDENTITY(1,1) NOT NULL,
	[WorkOrderNo] [varchar](100) NOT NULL,
	[WorkOrderName] [varchar](100) NULL,
	[ProjectType] [int] NULL,
	[Proposal] [varchar](max) NULL,
	[ClientApprovalRef] [varchar](max) NULL,
	[Status] [int] NULL,
	[Currency] [int] NULL,
	[ChangeRequestCount] [int] NULL,
	[ProjectStartDate] [datetime] NULL,
	[ProjectEndDate] [datetime] NULL,
	[InvoiceDuration] [int] NULL,
	[Remark] [varchar](max) NULL,
	[Hours] [decimal](18, 2) NULL,
	[ClientId] [int] NULL,
 CONSTRAINT [PK_WorkOrder_1] PRIMARY KEY CLUSTERED 
(
	[WorkOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_WorkOrderNo] UNIQUE NONCLUSTERED 
(
	[WorkOrderNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[WorkOrder]  WITH CHECK ADD  CONSTRAINT [FK_WorkOrder_ApplicationStatusCurrency] FOREIGN KEY([Currency])
REFERENCES [dbo].[ApplicationStatus] ([ApplicationStatusID])
GO
ALTER TABLE [dbo].[WorkOrder] CHECK CONSTRAINT [FK_WorkOrder_ApplicationStatusCurrency]
GO
ALTER TABLE [dbo].[WorkOrder]  WITH CHECK ADD  CONSTRAINT [FK_WorkOrder_ApplicationStatusInvoiceDur] FOREIGN KEY([InvoiceDuration])
REFERENCES [dbo].[ApplicationStatus] ([ApplicationStatusID])
GO
ALTER TABLE [dbo].[WorkOrder] CHECK CONSTRAINT [FK_WorkOrder_ApplicationStatusInvoiceDur]
GO
ALTER TABLE [dbo].[WorkOrder]  WITH CHECK ADD  CONSTRAINT [FK_WorkOrder_ProjectType] FOREIGN KEY([ProjectType])
REFERENCES [dbo].[ProjectType] ([ProjectTypeID])
GO
ALTER TABLE [dbo].[WorkOrder] CHECK CONSTRAINT [FK_WorkOrder_ProjectType]
GO
ALTER TABLE [dbo].[WorkOrder]  WITH CHECK ADD  CONSTRAINT [FK_WorkOrder_WorkOrder] FOREIGN KEY([WorkOrderID])
REFERENCES [dbo].[WorkOrder] ([WorkOrderID])
GO
ALTER TABLE [dbo].[WorkOrder] CHECK CONSTRAINT [FK_WorkOrder_WorkOrder]
GO

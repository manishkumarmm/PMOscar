
/****** Object:  Table [dbo].[ChangeRequest]    Script Date: 4/19/2018 12:59:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChangeRequest](
	[ChangeRequestID] [int] IDENTITY(1,1) NOT NULL,
	[ChangeRequestNo] [int] NULL,
	[ClientApprovalRef] [varchar](max) NULL,
	[ChangeRequestHours] [decimal](18, 2) NULL,
	[StatusID] [int] NULL,
	[Remark] [varchar](max) NULL,
	[WorkOrderID] [int] NULL,
	[IssueDate] [datetime] NULL,
 CONSTRAINT [PK_ChangeRequest] PRIMARY KEY CLUSTERED 
(
	[ChangeRequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChangeRequest]  WITH CHECK ADD  CONSTRAINT [FK_ChangeRequest_ApplicationStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[ApplicationStatus] ([ApplicationStatusID])
GO
ALTER TABLE [dbo].[ChangeRequest] CHECK CONSTRAINT [FK_ChangeRequest_ApplicationStatus]
GO

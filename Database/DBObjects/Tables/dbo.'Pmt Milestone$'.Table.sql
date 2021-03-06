
/****** Object:  Table [dbo].['Pmt Milestone$']    Script Date: 4/19/2018 12:59:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].['Pmt Milestone$'](
	[Sl#No] [float] NULL,
	[Project ] [nvarchar](255) NULL,
	[Change Request#] [nvarchar](255) NULL,
	[Work Order#] [float] NULL,
	[Status] [nvarchar](255) NULL,
	[Invoice Duration] [nvarchar](255) NULL,
	[MileStone No] [float] NULL,
	[MileStone] [nvarchar](255) NULL,
	[Milestone Date] [nvarchar](255) NULL,
	[%] [float] NULL,
	[Currency] [nvarchar](255) NULL,
	[InvoiceMonth] [nvarchar](255) NULL,
	[Amount] [float] NULL,
	[Amount in USD] [float] NULL,
	[Milestone Met?] [nvarchar](255) NULL,
	[Milestone Met Communication Date] [nvarchar](255) NULL,
	[Invoice Raised/Status] [nvarchar](255) NULL,
	[Amount Invoiced] [float] NULL,
	[Invoices raised on (DDMMYYY)] [nvarchar](255) NULL,
	[F20] [nvarchar](255) NULL,
	[F21] [nvarchar](255) NULL
) ON [PRIMARY]
GO

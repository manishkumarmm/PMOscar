
/****** Object:  Table [dbo].[BillingDetails]    Script Date: 4/19/2018 12:59:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillingDetails](
	[BillingID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[ResourceID] [int] NOT NULL,
	[PlannedHours] [int] NULL,
	[ActualHours] [int] NULL,
	[fromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[Status] [varchar](1) NULL,
	[Freeze] [bit] NOT NULL,
	[UBT] [varchar](10) NULL,
	[Comments] [nvarchar](max) NULL,
	[ActualSpentHours] [decimal](12, 2) NULL,
 CONSTRAINT [PK_BillingDetails] PRIMARY KEY CLUSTERED 
(
	[BillingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[BillingDetails] ADD  DEFAULT ((0)) FOR [Freeze]
GO

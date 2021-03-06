
/****** Object:  Table [dbo].[Resource]    Script Date: 4/19/2018 12:59:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resource](
	[ResourceId] [int] IDENTITY(1,1) NOT NULL,
	[ResourceName] [varchar](50) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[RoleId] [int] NULL,
	[TeamID] [int] NULL,
	[BillingStartDate] [datetime] NULL,
	[emp_Code] [varchar](50) NULL,
	[CostCentreID] [int] NULL,
	[WeeklyHour] [decimal](10, 1) NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[ResourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Resource] ADD  CONSTRAINT [DF__Resource__Weekly]  DEFAULT ((40.0)) FOR [WeeklyHour]
GO

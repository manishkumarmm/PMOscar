
/****** Object:  Table [dbo].[ResourceCC]    Script Date: 4/19/2018 12:59:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceCC](
	[ResourceId] [int] NULL,
	[ResourceName] [varchar](250) NULL,
	[IsActive] [bit] NULL,
	[RoleId] [int] NULL,
	[TeamID] [int] NULL,
	[emp_Code] [varchar](25) NULL,
	[CostCentreID] [int] NULL,
	[CostCentre] [varchar](30) NULL
) ON [PRIMARY]
GO

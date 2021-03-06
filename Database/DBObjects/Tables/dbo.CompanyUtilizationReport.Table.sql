
/****** Object:  Table [dbo].[CompanyUtilizationReport]    Script Date: 4/19/2018 12:59:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyUtilizationReport](
	[CompanyUtilizationReportID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[ResourceID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[TeamID] [int] NOT NULL,
	[AvailableHours] [int] NULL,
	[BilledHours] [int] NULL,
	[Finalize] [bit] NOT NULL,
	[Admin] [int] NULL,
	[Open] [int] NULL,
	[VAS] [int] NULL,
	[Proposal] [int] NULL,
	[ActualHours] [int] NULL,
 CONSTRAINT [PK_CompanyUtilizationReport] PRIMARY KEY CLUSTERED 
(
	[CompanyUtilizationReportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

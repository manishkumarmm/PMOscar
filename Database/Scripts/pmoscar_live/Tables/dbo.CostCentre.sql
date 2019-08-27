GO

/****** Object:  Table [dbo].[CostCentre]    Script Date: 01/29/2015 15:31:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CostCentre]') AND type in (N'U'))
DROP TABLE [dbo].[CostCentre]
GO
/****** Object:  Table [dbo].[CostCentre]    Script Date: 01/29/2015 15:31:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CostCentre](
	[CostCentreID] [int] IDENTITY(1,1) NOT NULL,
	[CostCentre] [varchar](max) NOT NULL,
 CONSTRAINT [PK_CostCentre] PRIMARY KEY CLUSTERED 
(
	[CostCentreID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Project]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Project]
GO

CREATE TABLE [dbo].[Project] (
	[ProjectId] [int] IDENTITY (1, 1) NOT NULL ,
	[ProjectName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ShortName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[CreatedBy] [int] NOT NULL ,
	[CreatedDate] [datetime] NOT NULL ,
	[UpdatedBy] [int] NOT NULL ,
	[UpdatedDate] [datetime] NULL ,
	[PhaseID] [int] NULL ,
	[ProjectType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ProjectOwner] [int] NULL ,
	[ProjectManager] [int] NULL ,
	[DeliveryDate] [datetime] NULL ,
	[RevisedDeliveryDate] [datetime] NULL ,
	[ApprvChangeRequest] [int] NULL ,
	[PMComments] [nvarchar] (MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[DeliveryComments] [nvarchar] (MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[IsActive] [bit] NULL ,
	[ProjectCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ProjStartDate] [datetime] NULL ,
	[MaintClosingDate] [datetime] NULL ,
	CONSTRAINT [PK_Project] PRIMARY KEY  CLUSTERED 
	(
		[ProjectId]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO



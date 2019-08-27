if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProjectAudit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ProjectAudit]
GO

CREATE TABLE [dbo].[ProjectAudit] (
	[ProjectAuditID] [int] IDENTITY (1, 1) NOT NULL ,
	[ProjectID] [int] NULL ,
	[PhaseID] [int] NULL ,
	[ProjectName] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ShortName] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ProjectType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ProjectOwner] [int] NULL ,
	[ProjectManager] [int] NULL ,
	[DeliveryDate] [datetime] NULL ,
	[RevisedDeliveryDate] [datetime] NULL ,
	[PMComments] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[DeliveryComments] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[IsActive] [bit] NULL ,
	[CreatedBy] [int] NULL ,
	[CreatedDate] [datetime] NULL ,
	[ApprvChangeRequest] [int] NULL ,
	CONSTRAINT [PK_ProjectAudit] PRIMARY KEY  CLUSTERED 
	(
		[ProjectAuditID]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO



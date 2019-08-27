if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProjectEstimationAudit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ProjectEstimationAudit]
GO

CREATE TABLE [dbo].[ProjectEstimationAudit] (
	[ProjEstimationAuditID] [int] IDENTITY (1, 1) NOT NULL ,
	[ProjectEstimationID] [int] NULL ,
	[ProjectID] [int] NULL ,
	[EstimationRoleID] [int] NULL ,
	[PhaseID] [int] NULL ,
	[BillableHours] [int] NULL ,
	[BudgetHours] [int] NULL ,
	[RevisedBudgetHours] [int] NULL ,
	[CreatedBy] [int] NULL ,
	[CreatedDate] [datetime] NULL ,
	CONSTRAINT [PK_ProjectEstimationAudit] PRIMARY KEY  CLUSTERED 
	(
		[ProjEstimationAuditID]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO



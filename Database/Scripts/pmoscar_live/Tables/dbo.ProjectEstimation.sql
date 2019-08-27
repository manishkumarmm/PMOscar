if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProjectEstimation]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ProjectEstimation]
GO

CREATE TABLE [dbo].[ProjectEstimation] (
	[ProjectEstimationID] [int] IDENTITY (1, 1) NOT NULL ,
	[ProjectID] [int] NULL ,
	[PhaseID] [int] NULL ,
	[EstimationRoleID] [int] NULL ,
	[BillableHours] [int] NULL ,
	[BudgetHours] [int] NULL ,
	[RevisedBudgetHours] [int] NULL ,
	[CreatedBy] [int] NULL ,
	[CreatedDate] [datetime] NULL ,
	[UpdatedBy] [int] NULL ,
	[UpdatedDate] [datetime] NULL ,
	CONSTRAINT [PK_ProjectEstimation] PRIMARY KEY  CLUSTERED 
	(
		[ProjectEstimationID]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO



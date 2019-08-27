IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[OLTP].[tgrBudgetRevisionHistory]'))
DROP TRIGGER [OLTP].tgrBudgetRevisionHistory
GO

/****** Object:  Trigger [OLTP].[tgrLoanHistory]    Script Date: 11/27/2015 14:51:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [tgrBudgetRevisionHistory] ON [BudgetRevision]
FOR INSERT
	,UPDATE
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [BudgetRevisionHistory] (			
	[BudgetRevisionID],
	[ProjectEstimationID],
	[BillableHours],
	[BudgetHours],
	[IsChangeRequest],
	[Reason],
	[CreatedBy],
	[CreatedDate],
	[UpdatedBy],
	[UpdatedDate],
	[IsApproved],
	[InsertedDate] 
		)
	SELECT [BudgetRevisionID],
	[ProjectEstimationID],
	[BillableHours],
	[BudgetHours],
	[IsChangeRequest],
	[Reason],
	[CreatedBy],
	[CreatedDate],
	[UpdatedBy],
	[UpdatedDate] ,
	[IsApproved],
	GETDATE() 
	FROM Inserted
END

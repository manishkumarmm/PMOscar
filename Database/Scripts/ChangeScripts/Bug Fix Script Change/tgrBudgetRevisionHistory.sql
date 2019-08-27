USE [PMOscar_Dev]
GO

/****** Object:  Trigger [dbo].[tgrBudgetRevisionHistory]    Script Date: 06-12-2016 08:50:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[tgrBudgetRevisionHistory] ON [dbo].[BudgetRevision]
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
	[ApproveStatus],
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
	[ApproveStatus],
	GETDATE() 
	FROM Inserted
END

GO



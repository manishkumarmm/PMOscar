

/**************** Script for save budget revision  *****************************/



-- ==========================================================================================
-- Stored Procedures for save budget revision
-- ==========================================================================================


/****** Object:  StoredProcedure [dbo].[SaveBudgetRevision]    ******/
DROP PROCEDURE [dbo].[SaveBudgetRevision]
GO

/****** Object:  StoredProcedure [dbo].[SaveBudgetRevision]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
-- get the billing details for the selected month and year

CREATE PROCEDURE [dbo].[SaveBudgetRevision]
	-- Add the parameters for the stored procedure here
	
	@ProjectEstimationID INT
   ,@BillableHours INT
   ,@BudgetHours INT
   ,@IsChangeRequest BIT
   ,@Reason VARCHAR(MAX)
   ,@CreatedBy INT
   ,@UpdatedBy INT
   ,@ApproveStatus char(1)
   ,@IsSave BIT
	
AS
BEGIN
		
		IF (@IsSave = 0)
		BEGIN
			UPDATE BudgetRevision
			   SET BillableHours = @BillableHours
				  ,BudgetHours = @BudgetHours
				  ,IsChangeRequest = @IsChangeRequest
				  ,Reason = @Reason				  
				  ,UpdatedBy = @UpdatedBy
				  ,UpdatedDate = GETDATE()
				  ,ApproveStatus = @ApproveStatus
			 WHERE ProjectEstimationID = @ProjectEstimationID
		END
		ELSE
		BEGIN
			INSERT INTO BudgetRevision
					   (ProjectEstimationID
					   ,BillableHours
					   ,BudgetHours
					   ,IsChangeRequest
					   ,Reason
					   ,CreatedBy
					   ,CreatedDate
					   ,UpdatedBy
					   ,UpdatedDate
					   ,ApproveStatus)
				 VALUES
					   (@ProjectEstimationID
					   ,@BillableHours
					   ,@BudgetHours
					   ,@IsChangeRequest
					   ,@Reason
					   ,@CreatedBy
					   ,GETDATE()
					   ,@UpdatedBy
					   ,GETDATE()
					   ,@ApproveStatus)

		END	 
   END

 GO




 
 
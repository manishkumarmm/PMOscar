IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SaveBudgetRevision')
     DROP PROCEDURE SaveBudgetRevision
go
/****** Object:  StoredProcedure [dbo].[SaveBudgetRevision]    Script Date: 12/01/2015 13:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
CREATE PROCEDURE  [dbo].[SaveBudgetRevision]  
(      
	@ProjectEstimationID INT
   ,@BillableHours INT
   ,@BudgetHours INT
   ,@IsChangeRequest BIT
   ,@Reason VARCHAR(MAX)
   ,@CreatedBy INT
   ,@UpdatedBy INT
   ,@IsApproved BIT
   ,@IsSave BIT
)      
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
				  ,IsApproved = @IsApproved
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
					   ,IsApproved)
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
					   ,@IsApproved)

		END	 
   END

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'InsertBudgetRevisionDetails') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertBudgetRevisionDetails]
/****** Object:  StoredProcedure dbo].InsertBudgetRevisionDetails   Script Date: 08/03/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==============================================================================
-- Description: Insert BudgetRevisionDetails 
-- ==============================================================================
-- Created By : Kochurani Kuriakose
-- Modified by :Deepa.R
-- Created On : 2018-08-29
-- Modified On : 2019-03-14
-- ==============================================================================

--exec InsertBudgetRevisionDetails 1,'','','','','','','','','','',''
CREATE Procedure  [dbo].[InsertBudgetRevisionDetails] 
(   
                   @BudgetRevisionID int,
				   @PhaseID int,
                   @ResourceID int,
                   @EstimationRoleID int,
				   @BillableHours decimal(6,2),
                   @EstimatedHours decimal(6,2),
                   @ProductivityAdjustmentHours decimal(6,2),
                   @Overrun decimal(6,2),
                   @BudgetHours decimal(6,2),
                   @Buffer decimal(6,2),
                   @RevisedBudgetHours decimal(6,2),
				   @AdditionalBudgetHours decimal(6,2),
                   @CreatedBy int,
                   @CreatedDate datetime,
                   @UpdatedBy int,
                   @UpdatedDate datetime,
                   @Comments varchar(500),
                   @IsChangeRequest bit,
				   @IsApproved bit,
		           @BudgetRevisionDetailsID int OUTPUT
)      
AS      
      
Begin     


	insert into BudgetRevisionDetails
		(                  
		           BudgetRevisionID,
				   PhaseID,
                   ResourceID,
                   EstimationRoleID,
				   BillableHours,
                   EstimatedHours,
                   ProductivityAdjustmentHours,
                   Overrun,
                   BudgetHours,
                   [Buffer],
                   RevisedBudgetHours,
				   AdditionalBudgetHours,
                   CreatedBy,
                   CreatedDate,
                   UpdatedBy,
                   UpdatedDate,
                   Comments,
				   IsApproved,
                   IsChangeRequest
		)
		VALUES
		(
		           @BudgetRevisionID,
				   @PhaseID,
                   @ResourceID,
                   @EstimationRoleID,
				   @BillableHours,
                   @EstimatedHours,
                   @ProductivityAdjustmentHours,
                   @Overrun,
                   @BudgetHours,
                   @Buffer,
                   @RevisedBudgetHours,
				   @AdditionalBudgetHours,
                   @CreatedBy,
                   @CreatedDate,
                   @UpdatedBy,
                   @UpdatedDate,
                   @Comments,
				   @IsApproved,
                   @IsChangeRequest
		)
		  SELECT @BudgetRevisionDetailsID= SCOPE_IDENTITY() 
    
   End 
     
 
GO



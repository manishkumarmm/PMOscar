
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetApprovedBudgetList') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].GetApprovedBudgetList
GO

GO

/****** Object:  StoredProcedure [dbo].[GetApprovedBudgetList]    Script Date: 1/29/2019 2:31:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Saliha U A
-- Create date: 1/15/2019
-- Description:	Get Approved Budget List
-- =============================================

CREATE Procedure [dbo].[GetApprovedBudgetList]     
 @BudgetRevisionID  int
  
 AS

Begin      
/****** Script for SelectTopNRows command from SSMS  ******/
SELECT [EstimationRoleID], sum(BillableHours) as BillableHours ,sum(EstimatedHours)as EstimatedHours,sum(ProductivityAdjustmentHours) as ProductivityAdjustmentHours,sum(Overrun)as Overrun ,sum(BudgetHours) as BudgetHours,sum(Buffer)as Buffer,sum(RevisedBudgetHours) asRevisedBudgetHours,sum(ActualHours)as ActualHours,[IsApproved]
  FROM [BudgetRevisionDetails] where BudgetRevisionID=@BudgetRevisionID group by [BudgetRevisionID],[PhaseID],[ResourceID],[EstimationRoleID],[BillableHours],[EstimatedHours],[ProductivityAdjustmentHours],[Overrun],[BudgetHours],[Buffer],[RevisedBudgetHours],[ActualHours],[IsApproved]
End 

GO



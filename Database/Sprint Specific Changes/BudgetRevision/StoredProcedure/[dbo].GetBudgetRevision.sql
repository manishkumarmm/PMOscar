
-- =============================================
-- Author:		Joshwa
-- Create date: 1/25/2019
-- Description:	Get Revised Budget List
-- =============================================

CREATE Procedure [dbo].[GetBudgetRevision] 
 @ProjectID int,
 @BudgetRevisionID int
  
 AS
 declare @IsApproved varchar(max) 
Begin      
select @IsApproved=Status from BudgetRevisionLog where BudgetRevisionID=@BudgetRevisionID;
if(@IsApproved='Approved')
BEGIN
select 
	SUM(BillableHours) AS BillableHours,
	SUM(EstimatedHours) AS EstimatedHours,
	SUM(ProductivityAdjustmentHours) AS ProductivityAdjustmentHours,
	SUM(Buffer) AS Buffer,
	SUM(Overrun) AS Overrun,
	SUM(BudgetHours) AS BudgetHours,
	SUM(RevisedBudgetHours) AS RevisedBudgetHours,
	IsChangeRequest,
	(SELECT STUFF ((SELECT  case when Ltrim(RTrim(isnull(bd1.Comments,'')))='' then '' else ',' end  + bd1.Comments
              FROM BudgetRevisionDetails bd1
			  inner join BudgetRevisionLog bl on bl.BudgetRevisionID=bd1.BudgetRevisionID 
			  where projectid= @ProjectID 
			  and bd1.PhaseID=bd.PhaseID 
			  and bd1.EstimationRoleID =bd.EstimationRoleID 
			  and bd.ResourceID=bd1.ResourceID
			                FOR XML PATH ('')), 1, 1, ''))
			  
			   as Comments,
	bd.IsApproved 
from BudgetRevisionDetails bd
inner join BudgetRevisionLog bl on bl.BudgetRevisionID = bd.BudgetRevisionID
where bd.budgetrevisionid = @BudgetRevisionID 
GROUP BY 
	bd.ResourceID,
	bd.EstimationRoleID,
	bd.PhaseID,
	bd.IsApproved,
	IsChangeRequest
order by 
	bd.PhaseID,
	bd.EstimationRoleID,
	bd.ResourceID	
END

ELSE
BEGIN
 select 
	BudgetRevisionDetailsID,
	BillableHours,
	EstimatedHours,
	ProductivityAdjustmentHours,
	Buffer,
	Overrun,
	BudgetHours,
	RevisedBudgetHours,
	IsChangeRequest,
	Comments,
	IsApproved 
from BudgetRevisionDetails
where budgetrevisionid = @BudgetRevisionID 
order by 
	PhaseID,
	EstimationRoleID,
	ResourceID	
END
End 
GO



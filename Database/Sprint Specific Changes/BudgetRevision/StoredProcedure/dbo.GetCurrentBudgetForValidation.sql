IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetCurrentBudgetForValidation') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCurrentBudgetForValidation]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[GetCurrentBudgetForValidation] 488,159237,'T'
-- =============================================  
-- Author:  Haritha E.S
-- modified by : Deepa.R
-- Create date: 3/1/2019 
-- Modified on:15/03/2019
-- Description: Sp to get current budget
-- =============================================  
CREATE PROCEDURE [dbo].[GetCurrentBudgetForValidation]  
  @ProjectID int,
  @BudgetRevisionID int,
  @CalParameter varchar(1)

   
AS  

--P,N,T
declare @IsApproved varchar(max) 
declare @MaxBudgetRevisionId int
declare @budgetrevisiondetailsCount int
declare @ApprovedBudgetSumCount int

BEGIN  

------@MaxBudgetRevisionId is kept only to know if there are any approved Budgetrevision for the selected Project---------
if (@BudgetRevisionID is null or @BudgetRevisionID =0)
	begin
		select @MaxBudgetRevisionId = max(budgetrevisionid) 
		from budgetrevisionlog 
		where projectid = @ProjectID 	 
			and status = 'Approved' 
	end
else
	begin
		 select @MaxBudgetRevisionId = max(budgetrevisionid) 
		 from budgetrevisionlog 
		 where projectid = @ProjectID 
				and BudgetRevisionID <=@BudgetRevisionID 
				and status = 'Approved' 
  end


select @IsApproved=Status 
from BudgetRevisionLog 
where BudgetRevisionID=@BudgetRevisionID;
--print @MaxBudgetRevisionId
IF (@MaxBudgetRevisionId <> 0)
BEGIN
		if(@CalParameter='T')
		  BEGIN
			SELECT 
		
				--EstimationRoleID,
				--bd.PhaseID,		
				sum(BillableHours) as BillableHours,
				sum(BudgetHours) as BudgetHours,
				sum(bd.AdditionalBudgetHours) as RevisedBudgetHours,
				--sum(bd.RevisedBudgetHours) as RevisedBudgetHours,
				
				sum([EstimatedHours] ) [EstimatedHours],
			sum([ProductivityAdjustmentHours]) [ProductivityAdjustmentHours] ,
			sum([Overrun] ) [Overrun],
			sum([Buffer]) [Buffer]
				from BudgetRevisionDetails bd
			inner join BudgetRevisionLog bl on bl.BudgetRevisionID=bd.BudgetRevisionID 
			
			 where  bl.Status = 'Approved' 
			 --and bd.IsInitialBudget=1
			 and bl.ProjectID = @ProjectID
			 and ( bd.BudgetRevisionID <=@BudgetRevisionID or @BudgetRevisionID=0)
			 --group by 
		
				-- bd.EstimationRoleID,
				-- bd.PhaseID,				
				-- bd.IsApproved
			  --order by 		
		
				--bd.PhaseID,
				--bd.EstimationRoleID

			END
		
		

		Else if(@CalParameter='P')
		  BEGIN
			SELECT 
		
				--EstimationRoleID,
				--bd.PhaseID,		
				sum(case when BillableHours>0 then BillableHours else 0 end) as BillableHours,
				sum(case when BudgetHours>0 then budgethours else 0 end ) as BudgetHours,
				sum(case when AdditionalBudgetHours>0 then AdditionalBudgetHours else 0 end) as RevisedBudgetHours,				
				--sum(case when RevisedBudgetHours>0 then RevisedBudgetHours else 0 end) as RevisedBudgetHours,				
				sum(case when EstimatedHours>0 then EstimatedHours else 0 end  ) [EstimatedHours],
				sum(case when ProductivityAdjustmentHours>0 then ProductivityAdjustmentHours else 0 end) [ProductivityAdjustmentHours] ,
				sum(case when Overrun>0 then overrun else 0 end ) [Overrun],
				sum(case when [Buffer]>0 then [buffer] else 0 end) [Buffer]
				from BudgetRevisionDetails bd
			inner join BudgetRevisionLog bl on bl.BudgetRevisionID=bd.BudgetRevisionID 
			
			 where  bl.Status = 'Approved' 
			  and bl.IsInitialRevision=0
			 and bl.ProjectID = @ProjectID
			 and ( bd.BudgetRevisionID <=@BudgetRevisionID or @BudgetRevisionID=0)
			 --group by 
		
				-- bd.EstimationRoleID,
				-- bd.PhaseID,				
				-- bd.IsApproved
			 -- order by 		
		
				--bd.PhaseID,
				--bd.EstimationRoleID

			END
		Else if(@CalParameter='N')
		  BEGIN
			SELECT 
		
				--EstimationRoleID,
				--bd.PhaseID,		
				sum(case when BillableHours<0 then BillableHours else 0 end) as BillableHours,
				sum(case when BudgetHours<0 then budgethours else 0 end ) as BudgetHours,
				sum(case when AdditionalBudgetHours<0 then AdditionalBudgetHours else 0 end) as RevisedBudgetHours,				
				--sum(case when RevisedBudgetHours<0 then RevisedBudgetHours else 0 end) as RevisedBudgetHours,				
				sum(case when EstimatedHours<0 then EstimatedHours else 0 end  ) [EstimatedHours],
				sum(case when ProductivityAdjustmentHours<0 then ProductivityAdjustmentHours else 0 end) [ProductivityAdjustmentHours] ,
				sum(case when Overrun<0 then overrun else 0 end ) [Overrun],
				sum(case when [Buffer]<0 then [buffer] else 0 end) [Buffer]
				from BudgetRevisionDetails bd
			inner join BudgetRevisionLog bl on bl.BudgetRevisionID=bd.BudgetRevisionID 
			
			 where  bl.Status = 'Approved' 
			 and bl.IsInitialRevision=0
			 and bl.ProjectID = @ProjectID
			 and ( bd.BudgetRevisionID <=@BudgetRevisionID or @BudgetRevisionID=0)
			 --group by 
		
				-- bd.EstimationRoleID,
				-- bd.PhaseID,				
				-- bd.IsApproved
			 -- order by 		
		
				--bd.PhaseID,
				--bd.EstimationRoleID

			END
		
		END

END

GO



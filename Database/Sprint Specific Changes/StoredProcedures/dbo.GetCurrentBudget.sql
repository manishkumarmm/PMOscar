IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetCurrentBudget') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCurrentBudget]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================  
-- Author:  Haritha E.S
-- modified by : Deepa
-- Create date: 3/1/2019 
-- Modified on:15/03/2019
-- Description: Sp to get current budget
-- =============================================  
--[dbo].[GetCurrentBudget]   902,12
CREATE PROCEDURE [dbo].[GetCurrentBudget]  
  @ProjectID int,
  @BudgetRevisionID int

   
AS  
declare @IsApproved varchar(max) 
declare @MaxBudgetRevisionId int
declare @budgetrevisiondetailsCount int
declare @ApprovedBudgetSumCount int,
@RevisedBudgethours decimal (12,2)

BEGIN  

------@MaxBudgetRevisionId is kept only to know if there are any approved Budgetrevision for the selected Project---------
if (@BudgetRevisionID is null or @BudgetRevisionID =0)
begin
	select @MaxBudgetRevisionId = max(budgetrevisionid) from budgetrevisionlog 
	where projectid = @ProjectID 
	and 
	 status = 'Approved' 
 end
 else
 begin
 select @MaxBudgetRevisionId = max(budgetrevisionid) from budgetrevisionlog 
where projectid = @ProjectID 
and BudgetRevisionID <=@BudgetRevisionID and
 status = 'Approved' 
 end
select @IsApproved=Status from BudgetRevisionLog where BudgetRevisionID=@BudgetRevisionID;
select @RevisedBudgethours=sum(RevisedBudgetHours) from BudgetRevisionDetails where BudgetRevisionID=@BudgetRevisionID;

IF (@MaxBudgetRevisionId <> 0)
BEGIN


create table #ApprovedBudgetSum
(
    ResourceID int, 
    ActualHours decimal(18,2), 
    EstimationRoleID int,
	PhaseID int,
	Phase varchar(max),
	BillableHours decimal(18,2),
	BudgetHours decimal(18,2),
	RevisedBudgetHours decimal(18,2),
	Comments varchar(max),
	IsApproved bit,
		[EstimatedHours] [decimal](6, 2) NULL,
	[ProductivityAdjustmentHours] [decimal](6, 2) NULL,
	[Overrun] [decimal](6, 2) NULL,	
	[Buffer] [decimal](6, 2) NULL,
)
--declare @additionalBudget  decimal(18,2);
--set @additionalBudget=(SELECT SUM(AdditionalBudgetHours) FROM BudgetRevisionDetails WHERE BudgetRevisionID =159336 and AdditionalBudgetHours>0);
Insert into #ApprovedBudgetSum
	SELECT 
		bd.ResourceID,
		sum(ac.ActualHours) as ActualHours,
		EstimationRoleID,
		bd.PhaseID,
		p.Phase,
				 
		sum(bd.BillableHours) as BillableHours,
		sum(bd.BudgetHours) as BudgetHours,
		--(sum(bd.BudgetHours)+ @additionalBudget) as RevisedBudgetHours,
		sum(bd.AdditionalBudgetHours) as RevisedBudgetHours,
		--sum(bd.RevisedBudgetHours) as RevisedBudgetHours,
		(SELECT STUFF ((
SELECT  case when Ltrim(RTrim(isnull(bd1.Comments,'')))='' then '' else ',' end  + bd1.Comments
				  FROM BudgetRevisionDetails bd1
				  inner join BudgetRevisionLog bl on bl.BudgetRevisionID=bd1.BudgetRevisionID 
				  where projectid= @ProjectID 
				  and bd1.PhaseID=bd.PhaseID 
				  and bd1.EstimationRoleID =bd.EstimationRoleID 
				  and bd.ResourceID=bd1.ResourceID
								FOR XML PATH ('')), 1, 1, ''))
			  
				   as Comments,
		bd.IsApproved ,
		 sum(bd.[EstimatedHours]) [EstimatedHours],
		  sum(bd.[ProductivityAdjustmentHours]) [ProductivityAdjustmentHours],
		  sum(bd.[Overrun]) [Overrun],		
		  sum(bd.[Buffer]) [Buffer]

		from BudgetRevisionDetails bd
	inner join BudgetRevisionLog bl on bl.BudgetRevisionID=bd.BudgetRevisionID 
	inner join Phase p on p.PhaseID = bd.PhaseID
	left join 
		(select 
				sum(ActualHours) as ActualHours,
				resourceid,
				RoleId,
				PhaseID,
				projectID 
				from TimeTracker tt 
			where FromDate <= GETDATE() and ProjectID = @ProjectID
			group by 
				resourceid,
				roleid,
				phaseid,
				ProjectId) ac 
		on ac.resourceid = bd.ResourceID
		and ac.RoleId = bd.EstimationRoleID
		and ac.PhaseID = bd.PhaseID 
		and ac.projectid = bl.ProjectID 
	 where  bl.Status in('Approved') 
	 and bl.ProjectID = @ProjectID
	 and ( bd.BudgetRevisionID <=@BudgetRevisionID or @BudgetRevisionID=0)
	 group by 
		 bd.ResourceID,
		 bd.EstimationRoleID,
		 bd.PhaseID,
		 p.Phase,
		 bd.IsApproved
	  order by 		
		
		bd.PhaseID,
		bd.EstimationRoleID,
		bd.ResourceID
 
 select @budgetrevisiondetailsCount = count(*) from BudgetRevisionDetails where BudgetRevisionID = @BudgetRevisionID
 select @ApprovedBudgetSumCount = count(*) from #ApprovedBudgetSum 
 print @budgetrevisiondetailsCount
 print @ApprovedBudgetSumCount

 IF(@budgetrevisiondetailsCount > @ApprovedBudgetSumCount)
 BEGIN

 select * from 
 
 (SELECT 
		ResourceID,			
		PhaseID,
		EstimationRoleID,
		ActualHours,
		Phase,
		BillableHours,
		BudgetHours,
		@RevisedBudgethours as RevisedBudgetHours ,--RevisedBudgetHours,
		Comments,
		IsApproved ,
	  [EstimatedHours]
      ,[ProductivityAdjustmentHours]
      ,[Overrun]
      
      ,[Buffer]
from #ApprovedBudgetSum
union
	select bd.ResourceID,
		bd.PhaseID,
		bd.EstimationRoleID ,
		0 ActualHours,
		p.Phase,
		0 BillableHours,
		0 BudgetHours,
		0 RevisedBudgetHours,
		'' Comments,
		0 IsApproved, 
		0 [EstimatedHours]
      ,0 [ProductivityAdjustmentHours]
      ,0 [Overrun]
     
      ,0 [Buffer]
	from BudgetRevisionDetails bd
	inner join Phase P on P.phaseID=bd.PhaseID
	left join #ApprovedBudgetSum a on a.ResourceID=bd.ResourceID and a.PhaseID=bd.PhaseID 
	and a.EstimationRoleID =bd.EstimationRoleID
	where bd.BudgetRevisionID = @BudgetRevisionID
	and a.BudgetHours is null ) as T
	order by PhaseID,EstimationRoleID,ResourceID

	
 END
 ELSE
 BEGIN
	SELECT  ResourceID , 
    ActualHours , 
    EstimationRoleID ,
	PhaseID ,
	Phase ,
	BillableHours ,
	BudgetHours ,
	@RevisedBudgetHours RevisedBudgetHours ,
	Comments,
	IsApproved ,
		[EstimatedHours] ,
	[ProductivityAdjustmentHours] ,
	[Overrun] ,	
	[Buffer] 
	
	 FROM #ApprovedBudgetSum order by PhaseID,EstimationRoleID,ResourceID
 END

END

ELSE
BEGIN

SELECT 
	bd.ResourceID,
	sum(ac.ActualHours) as ActualHours,
	EstimationRoleID,
	bd.PhaseID,
	p.Phase,
	bd.IsApproved 
from BudgetRevisionDetails bd
inner join BudgetRevisionLog bl on bl.BudgetRevisionID=bd.BudgetRevisionID  
and bl.BudgetRevisionID=@BudgetRevisionID

inner join Phase p on p.PhaseID = bd.PhaseID
left join 
(select 
	sum(ActualHours) as ActualHours,
	resourceid,
	RoleId,
	PhaseID,
	projectID 
from TimeTracker tt 
where FromDate <= GETDATE() and ProjectID = @ProjectID 
group by 
	resourceid,
	roleid,
	phaseid,
	ProjectId) ac 
	on ac.resourceid = bd.ResourceID 
	and ac.RoleId = bd.EstimationRoleID
	and ac.PhaseID = bd.PhaseID 
	and ac.projectid = bl.ProjectID 
	group by bd.ResourceID,bd.EstimationRoleID,bd.PhaseID,bd.IsApproved,p.Phase
order by 
	bd.PhaseID,
	bd.EstimationRoleID,
	bd.ResourceID	

END
DROP TABLE #ApprovedBudgetSum
END

GO



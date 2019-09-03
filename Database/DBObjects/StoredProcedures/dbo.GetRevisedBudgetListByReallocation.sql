IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetRevisedBudgetListByReallocation') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetRevisedBudgetListByReallocation]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Deepa.R
-- Create date: 1/3/2019
-- Description:	Get Revised Budget List With Re-allocation
--Changes Done : New design for the Budget revisionLog Page.
-- =============================================

CREATE Procedure [dbo].[GetRevisedBudgetListByReallocation]     
 @ProjectID  int,
 @UserID int
  --[GetRevisedBudgetList] 78,32
 AS
Begin 
declare @UserRole varchar(100)
	, @UserName varchar(100) 



select @UserName =u.username ,@UserRole=ur.UserRole
from [user]  u
join userrole ur on ur.UserRoleID=u.UserRoleID
where userid=@UserID

if (@UserRole='COO' or @UserName='nishad.hassan@naicoits.com')

 BEGIN 
		if (@ProjectID>0)
		Begin 
				Select   B.ProjectID
							  ,B.BudgetRevisionID
							  ,B.[BudgetRevisionName]
							  ,B.[ApprovedDate]
							  ,B.[RequestedDate]
							  ,B.UpdatedDate StatusDate     
							  ,B.[Status]
							  ,(B1.[BillableHours]) [BillableHours]
							  ,(B1.[EstimatedHours]) [EstimatedHours]
							  ,(B1.[ProductivityAdjustmentHours]) [ProductivityAdjustmentHours]
							  ,(B1.[Overrun]) [Overrun]
							  ,(B1.[BudgetHours]) [BudgetHours]
							  ,(B1.[Buffer]) [Buffer]
							  ,(B1.[RevisedBudgetHours]) [RevisedBudgetHours]
							 ,B1.IsReallocated
							   ,sum(B2.[CumulativeBillableHours]) [CumulativeBillableHours]
							  ,sum(B2.[CumulativeEstimatedHours]) [CumulativeEstimatedHours]
							  ,sum(B2.[CumulativeProductivityAdjustmentHours]) [CumulativeProductivityAdjustmentHours]
							  ,sum(B2.[CumulativeOverrun]) [CumulativeOverrun]
							  ,sum(B2.[CumulativeBudgetHours]) [CumulativeBudgetHours]
							  ,sum(B2.[CumulativeBuffer]) [CumulativeBuffer]
							  ,sum(B2.[CumulativeRevisedBudgetHours]) [CumulativeRevisedBudgetHours] 
							    ,Case when B.Status= 'Progress' then  1
							  when B.Status= 'PendingApproval' then  2
							  when B.Status= 'Approved' then  3
							  when B.Status= 'Rejected' then 4 end as Sort
				from BudgetRevisionLog B 
				LEFT Join 

					(
							 SELECT 
							  BL.ProjectID
							  ,BL.BudgetRevisionID
							  ,BL.[BudgetRevisionName]
							  ,BL.[ApprovedDate]
							  ,BL.[RequestedDate]
							  ,BL.UpdatedDate StatusDate     
							  ,BL.[Status]
							  ,sum(BDL.[BillableHours]) [BillableHours]
							  ,sum(BDL.[EstimatedHours]) [EstimatedHours]
							  ,sum(BDL.[ProductivityAdjustmentHours]) [ProductivityAdjustmentHours]
							  ,sum(BDL.[Overrun]) [Overrun]
							  ,sum(BDL.[BudgetHours]) [BudgetHours]
							  ,sum(BDL.[Buffer]) [Buffer]
							  ,sum(BDL.[RevisedBudgetHours]) [RevisedBudgetHours]
							  ,max(BDL.IsReallocated)  IsReallocated
			 

						  FROM [dbo].[BudgetRevisionLog] BL
						JOIN [dbo].[BudgetRevisionDetails]  BDL on BL.BudgetRevisionID=BDL.BudgetRevisionID
 
						--WHERE BL.[status]='Approved'
						GROUP BY    BL.ProjectID
							  ,BL.BudgetRevisionID
							  ,BL.[BudgetRevisionName]
							  ,BL.[ApprovedDate]
							  ,BL.[RequestedDate]
							  ,BL.UpdatedDate      
							  ,BL.[Status]

							  ) B1 on B.BudgetRevisionID=B1.BudgetRevisionID
					  LEFT JOIN
	  
						(
				
						  select 
							  BL1.ProjectID
							  ,BL1.BudgetRevisionID
							  , sum(BDL1.[BillableHours]) [CumulativeBillableHours]
							  ,sum(BDL1.[EstimatedHours]) [CumulativeEstimatedHours]
							  ,sum(BDL1.[ProductivityAdjustmentHours]) [CumulativeProductivityAdjustmentHours]
							  ,sum(BDL1.[Overrun]) [CumulativeOverrun]
							  ,sum(BDL1.[BudgetHours]) [CumulativeBudgetHours]
							  ,sum(BDL1.[Buffer]) [CumulativeBuffer]
							  ,sum(BDL1.[RevisedBudgetHours]) [CumulativeRevisedBudgetHours]
							  FROM  [dbo].[BudgetRevisionLog]  BL1 
						 JOIN [dbo].[BudgetRevisionDetails]  BDL1 on BL1.BudgetRevisionID=BDL1.BudgetRevisionID 
						 WHERE  BL1.Status='Approved'  
				 
							GROUP BY  BL1.ProjectID,BL1.BudgetRevisionID 
			
						) B2 on B2.ProjectID=B1.ProjectID 
															and B2.BudgetRevisionID<=B1.BudgetRevisionID 
											
											
					Where B.ProjectID=@ProjectID --and B1.IsReallocated=1 							
					GROUP BY 
					B.ProjectID
							  ,B.BudgetRevisionID
							  ,B.[BudgetRevisionName]
							  ,B.[ApprovedDate]
							  ,B.[RequestedDate]
							  ,B.UpdatedDate      
							  ,B.[Status]
							  ,(B1.[BillableHours]) 
							  ,(B1.[EstimatedHours]) 
							  ,(B1.[ProductivityAdjustmentHours]) 
							  ,(B1.[Overrun]) 
							  ,(B1.[BudgetHours]) 
							  ,(B1.[Buffer]) 
							  ,(B1.[RevisedBudgetHours]) 	
							  ,B1.IsReallocated
							     ,Case when B.Status= 'Progress' then  1
							  when B.Status= 'PendingApproval' then  2
							  when B.Status= 'Approved' then  3
							  when B.Status= 'Rejected' then 4 end 
							  order by Sort,BudgetRevisionID desc
			end

			ELSE 
				BEGIN 
					Select   B.ProjectID
							  ,B.BudgetRevisionID
							  ,B.[BudgetRevisionName]
							  ,B.[ApprovedDate]
							  ,B.[RequestedDate]
							  ,B.UpdatedDate StatusDate     
							  ,B.[Status]
							  ,(B1.[BillableHours]) [BillableHours]
							  ,(B1.[EstimatedHours]) [EstimatedHours]
							  ,(B1.[ProductivityAdjustmentHours]) [ProductivityAdjustmentHours]
							  ,(B1.[Overrun]) [Overrun]
							  ,(B1.[BudgetHours]) [BudgetHours]
							  ,(B1.[Buffer]) [Buffer]
							  ,(B1.[RevisedBudgetHours]) [RevisedBudgetHours]
							  ,B1.IsReallocated
							   ,sum(B2.[CumulativeBillableHours]) [CumulativeBillableHours]
							  ,sum(B2.[CumulativeEstimatedHours]) [CumulativeEstimatedHours]
							  ,sum(B2.[CumulativeProductivityAdjustmentHours]) [CumulativeProductivityAdjustmentHours]
							  ,sum(B2.[CumulativeOverrun]) [CumulativeOverrun]
							  ,sum(B2.[CumulativeBudgetHours]) [CumulativeBudgetHours]
							  ,sum(B2.[CumulativeBuffer]) [CumulativeBuffer]
							  ,sum(B2.[CumulativeRevisedBudgetHours]) [CumulativeRevisedBudgetHours] 
							    ,Case when B.Status= 'Progress' then  1
							  when B.Status= 'PendingApproval' then  2
							  when B.Status= 'Approved' then  3
							  when B.Status= 'Rejected' then 4 end as Sort
							 
				from BudgetRevisionLog B 
				LEFT Join 

					(
							 SELECT 
							  BL.ProjectID
							  ,BL.BudgetRevisionID
							  ,BL.[BudgetRevisionName]
							  ,BL.[ApprovedDate]
							  ,BL.[RequestedDate]
							  ,BL.UpdatedDate StatusDate     
							  ,BL.[Status]
							  ,sum(BDL.[BillableHours]) [BillableHours]
							  ,sum(BDL.[EstimatedHours]) [EstimatedHours]
							  ,sum(BDL.[ProductivityAdjustmentHours]) [ProductivityAdjustmentHours]
							  ,sum(BDL.[Overrun]) [Overrun]
							  ,sum(BDL.[BudgetHours]) [BudgetHours]
							  ,sum(BDL.[Buffer]) [Buffer]
							  ,sum(BDL.[RevisedBudgetHours]) [RevisedBudgetHours]
							  ,max(BDL.IsReallocated)  IsReallocated
			 

						  FROM [dbo].[BudgetRevisionLog] BL
						JOIN [dbo].[BudgetRevisionDetails]  BDL on BL.BudgetRevisionID=BDL.BudgetRevisionID
 
						--WHERE BL.[status]='Approved'
						GROUP BY    BL.ProjectID
							  ,BL.BudgetRevisionID
							  ,BL.[BudgetRevisionName]
							  ,BL.[ApprovedDate]
							  ,BL.[RequestedDate]
							  ,BL.UpdatedDate      
							  ,BL.[Status]
							  ) B1 on B.BudgetRevisionID=B1.BudgetRevisionID
					  LEFT JOIN
	  
						(
				
						  select 
							  BL1.ProjectID
							  ,BL1.BudgetRevisionID
							  , sum(BDL1.[BillableHours]) [CumulativeBillableHours]
							  ,sum(BDL1.[EstimatedHours]) [CumulativeEstimatedHours]
							  ,sum(BDL1.[ProductivityAdjustmentHours]) [CumulativeProductivityAdjustmentHours]
							  ,sum(BDL1.[Overrun]) [CumulativeOverrun]
							  ,sum(BDL1.[BudgetHours]) [CumulativeBudgetHours]
							  ,sum(BDL1.[Buffer]) [CumulativeBuffer]
							  ,sum(BDL1.[RevisedBudgetHours]) [CumulativeRevisedBudgetHours]
							  FROM  [dbo].[BudgetRevisionLog]  BL1 
						 JOIN [dbo].[BudgetRevisionDetails]  BDL1 on BL1.BudgetRevisionID=BDL1.BudgetRevisionID 
						 WHERE  BL1.Status='Approved'  
				 
							GROUP BY  BL1.ProjectID,BL1.BudgetRevisionID 
			
						) B2 on B2.ProjectID=B1.ProjectID 
															and B2.BudgetRevisionID<=B1.BudgetRevisionID 
											
								--WHERE B1.IsReallocated=1 		 	
												
					GROUP BY 
					B.ProjectID
							  ,B.BudgetRevisionID
							  ,B.[BudgetRevisionName]
							  ,B.[ApprovedDate]
							  ,B.[RequestedDate]
							  ,B.UpdatedDate      
							  ,B.[Status]
							  ,(B1.[BillableHours]) 
							  ,(B1.[EstimatedHours]) 
							  ,(B1.[ProductivityAdjustmentHours]) 
							  ,(B1.[Overrun]) 
							  ,(B1.[BudgetHours]) 
							  ,(B1.[Buffer]) 
							  ,(B1.[RevisedBudgetHours]) 
							  ,B1.IsReallocated	
							     ,Case when B.Status= 'Progress' then  1
							  when B.Status= 'PendingApproval' then  2
							  when B.Status= 'Approved' then  3
							  when B.Status= 'Rejected' then 4 end 
							  order by Sort,BudgetRevisionID desc
				END
	END
  ELSE ---NOT COO
  BEGIN 
		if (@ProjectID>0)
		Begin 
				Select   B.ProjectID
							  ,B.BudgetRevisionID
							  ,B.[BudgetRevisionName]
							  ,B.[ApprovedDate]
							  ,B.[RequestedDate]
							  ,B.UpdatedDate StatusDate     
							  ,B.[Status]
							  ,(B1.[BillableHours]) [BillableHours]
							  ,(B1.[EstimatedHours]) [EstimatedHours]
							  ,(B1.[ProductivityAdjustmentHours]) [ProductivityAdjustmentHours]
							  ,(B1.[Overrun]) [Overrun]
							  ,(B1.[BudgetHours]) [BudgetHours]
							  ,(B1.[Buffer]) [Buffer]
							  ,(B1.[RevisedBudgetHours]) [RevisedBudgetHours]
							   ,B1.IsReallocated
							   ,sum(B2.[CumulativeBillableHours]) [CumulativeBillableHours]
							  ,sum(B2.[CumulativeEstimatedHours]) [CumulativeEstimatedHours]
							  ,sum(B2.[CumulativeProductivityAdjustmentHours]) [CumulativeProductivityAdjustmentHours]
							  ,sum(B2.[CumulativeOverrun]) [CumulativeOverrun]
							  ,sum(B2.[CumulativeBudgetHours]) [CumulativeBudgetHours]
							  ,sum(B2.[CumulativeBuffer]) [CumulativeBuffer]
							  ,sum(B2.[CumulativeRevisedBudgetHours]) [CumulativeRevisedBudgetHours] 
							    ,Case when B.Status= 'Progress' then  1
							  when B.Status= 'PendingApproval' then  2
							  when B.Status= 'Approved' then  3
							  when B.Status= 'Rejected' then 4 end as Sort
				from BudgetRevisionLog B 
				Left Join 

					(
							 SELECT 
							  BL.ProjectID
							  ,BL.BudgetRevisionID
							  ,BL.[BudgetRevisionName]
							  ,BL.[ApprovedDate]
							  ,BL.[RequestedDate]
							  ,BL.UpdatedDate StatusDate     
							  ,BL.[Status]
							  ,sum(BDL.[BillableHours]) [BillableHours]
							  ,sum(BDL.[EstimatedHours]) [EstimatedHours]
							  ,sum(BDL.[ProductivityAdjustmentHours]) [ProductivityAdjustmentHours]
							  ,sum(BDL.[Overrun]) [Overrun]
							  ,sum(BDL.[BudgetHours]) [BudgetHours]
							  ,sum(BDL.[Buffer]) [Buffer]
							  ,sum(BDL.[RevisedBudgetHours]) [RevisedBudgetHours]
							   ,max(BDL.IsReallocated)  IsReallocated

						  FROM [dbo].[BudgetRevisionLog] BL
						JOIN [dbo].[BudgetRevisionDetails]  BDL on BL.BudgetRevisionID=BDL.BudgetRevisionID
 
						--WHERE BL.[status]='Approved'
						GROUP BY    BL.ProjectID
							  ,BL.BudgetRevisionID
							  ,BL.[BudgetRevisionName]
							  ,BL.[ApprovedDate]
							  ,BL.[RequestedDate]
							  ,BL.UpdatedDate      
							  ,BL.[Status]
							  ) B1 on B.BudgetRevisionID=B1.BudgetRevisionID
					  LEFT JOIN
	  
						(
				
						  select 
							  BL1.ProjectID
							  ,BL1.BudgetRevisionID
							  , sum(BDL1.[BillableHours]) [CumulativeBillableHours]
							  ,sum(BDL1.[EstimatedHours]) [CumulativeEstimatedHours]
							  ,sum(BDL1.[ProductivityAdjustmentHours]) [CumulativeProductivityAdjustmentHours]
							  ,sum(BDL1.[Overrun]) [CumulativeOverrun]
							  ,sum(BDL1.[BudgetHours]) [CumulativeBudgetHours]
							  ,sum(BDL1.[Buffer]) [CumulativeBuffer]
							  ,sum(BDL1.[RevisedBudgetHours]) [CumulativeRevisedBudgetHours]
							  FROM  [dbo].[BudgetRevisionLog]  BL1 
						 JOIN [dbo].[BudgetRevisionDetails]  BDL1 on BL1.BudgetRevisionID=BDL1.BudgetRevisionID 
						 WHERE  BL1.Status='Approved'  
				 
							GROUP BY  BL1.ProjectID,BL1.BudgetRevisionID 
			
						) B2 on B2.ProjectID=B1.ProjectID 
															and B2.BudgetRevisionID<=B1.BudgetRevisionID 
											
											
					Where B.ProjectID=@ProjectID --and B1.IsReallocated=1 									
					GROUP BY 
					B.ProjectID
							  ,B.BudgetRevisionID
							  ,B.[BudgetRevisionName]
							  ,B.[ApprovedDate]
							  ,B.[RequestedDate]
							  ,B.UpdatedDate      
							  ,B.[Status]
							  ,(B1.[BillableHours]) 
							  ,(B1.[EstimatedHours]) 
							  ,(B1.[ProductivityAdjustmentHours]) 
							  ,(B1.[Overrun]) 
							  ,(B1.[BudgetHours]) 
							  ,(B1.[Buffer]) 
							  ,(B1.[RevisedBudgetHours]) 
							  ,B1.IsReallocated	
							     ,Case when B.Status= 'Progress' then  1
							  when B.Status= 'PendingApproval' then  2
							  when B.Status= 'Approved' then  3
							  when B.Status= 'Rejected' then 4 end 
							  order by Sort,BudgetRevisionID desc

			end

			ELSE 
				BEGIN 
					Select   B.ProjectID
							  ,B.BudgetRevisionID
							  ,B.[BudgetRevisionName]
							  ,B.[ApprovedDate]
							  ,B.[RequestedDate]
							  ,B.UpdatedDate StatusDate     
							  ,B.[Status]
							  ,(B1.[BillableHours]) [BillableHours]
							  ,(B1.[EstimatedHours]) [EstimatedHours]
							  ,(B1.[ProductivityAdjustmentHours]) [ProductivityAdjustmentHours]
							  ,(B1.[Overrun]) [Overrun]
							  ,(B1.[BudgetHours]) [BudgetHours]
							  ,(B1.[Buffer]) [Buffer]
							  ,(B1.[RevisedBudgetHours]) [RevisedBudgetHours]
			  
							   ,sum(B2.[CumulativeBillableHours]) [CumulativeBillableHours]
							  ,sum(B2.[CumulativeEstimatedHours]) [CumulativeEstimatedHours]
							  ,sum(B2.[CumulativeProductivityAdjustmentHours]) [CumulativeProductivityAdjustmentHours]
							  ,sum(B2.[CumulativeOverrun]) [CumulativeOverrun]
							  ,sum(B2.[CumulativeBudgetHours]) [CumulativeBudgetHours]
							  ,sum(B2.[CumulativeBuffer]) [CumulativeBuffer]
							  ,sum(B2.[CumulativeRevisedBudgetHours]) [CumulativeRevisedBudgetHours] 
							    ,Case when B.Status= 'Progress' then  1
							  when B.Status= 'PendingApproval' then  2
							  when B.Status= 'Approved' then  3
							  when B.Status= 'Rejected' then 4 end as Sort
							  ,B1.IsReallocated
				from BudgetRevisionLog B 
				left Join 

					(
							 SELECT 
							  BL.ProjectID
							  ,BL.BudgetRevisionID
							  ,BL.[BudgetRevisionName]
							  ,BL.[ApprovedDate]
							  ,BL.[RequestedDate]
							  ,BL.UpdatedDate StatusDate     
							  ,BL.[Status]
							  ,sum(BDL.[BillableHours]) [BillableHours]
							  ,sum(BDL.[EstimatedHours]) [EstimatedHours]
							  ,sum(BDL.[ProductivityAdjustmentHours]) [ProductivityAdjustmentHours]
							  ,sum(BDL.[Overrun]) [Overrun]
							  ,sum(BDL.[BudgetHours]) [BudgetHours]
							  ,sum(BDL.[Buffer]) [Buffer]
							  ,sum(BDL.[RevisedBudgetHours]) [RevisedBudgetHours]
							    ,max(BDL.IsReallocated)  IsReallocated
			 

						  FROM [dbo].[BudgetRevisionLog] BL
						JOIN [dbo].[BudgetRevisionDetails]  BDL on BL.BudgetRevisionID=BDL.BudgetRevisionID
 
						--WHERE BL.[status]='Approved'
						GROUP BY    BL.ProjectID
							  ,BL.BudgetRevisionID
							  ,BL.[BudgetRevisionName]
							  ,BL.[ApprovedDate]
							  ,BL.[RequestedDate]
							  ,BL.UpdatedDate      
							  ,BL.[Status]
							  ) B1 on B.BudgetRevisionID=B1.BudgetRevisionID
					  LEFT JOIN
	  
						(
				
						  select 
							  BL1.ProjectID
							  ,BL1.BudgetRevisionID
							  , sum(BDL1.[BillableHours]) [CumulativeBillableHours]
							  ,sum(BDL1.[EstimatedHours]) [CumulativeEstimatedHours]
							  ,sum(BDL1.[ProductivityAdjustmentHours]) [CumulativeProductivityAdjustmentHours]
							  ,sum(BDL1.[Overrun]) [CumulativeOverrun]
							  ,sum(BDL1.[BudgetHours]) [CumulativeBudgetHours]
							  ,sum(BDL1.[Buffer]) [CumulativeBuffer]
							  ,sum(BDL1.[RevisedBudgetHours]) [CumulativeRevisedBudgetHours]
							  FROM  [dbo].[BudgetRevisionLog]  BL1 
						 JOIN [dbo].[BudgetRevisionDetails]  BDL1 on BL1.BudgetRevisionID=BDL1.BudgetRevisionID 
						 WHERE  BL1.Status='Approved'  
				 
							GROUP BY  BL1.ProjectID,BL1.BudgetRevisionID 
			
						) B2 on B2.ProjectID=B1.ProjectID 
															and B2.BudgetRevisionID<=B1.BudgetRevisionID 
											
											
							--WHERE B1.IsReallocated=1 				
					GROUP BY 
					B.ProjectID
							  ,B.BudgetRevisionID
							  ,B.[BudgetRevisionName]
							  ,B.[ApprovedDate]
							  ,B.[RequestedDate]
							  ,B.UpdatedDate      
							  ,B.[Status]
							  ,(B1.[BillableHours]) 
							  ,(B1.[EstimatedHours]) 
							  ,(B1.[ProductivityAdjustmentHours]) 
							  ,(B1.[Overrun]) 
							  ,(B1.[BudgetHours]) 
							  ,(B1.[Buffer]) 
							  ,(B1.[RevisedBudgetHours]) 
							  ,B1.IsReallocated	
							     ,Case when B.Status= 'Progress' then  1
							  when B.Status= 'PendingApproval' then  2
							  when B.Status= 'Approved' then  3
							  when B.Status= 'Rejected' then 4 end 
							  order by Sort,BudgetRevisionID desc
				END
	END
End
GO



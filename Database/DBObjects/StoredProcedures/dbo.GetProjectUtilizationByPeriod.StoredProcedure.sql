IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetProjectUtilizationByPeriod') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetProjectUtilizationByPeriod]
/****** Object:  StoredProcedure [dbo].[GetProjectUtilizationByPeriod]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
-- =============================================          
-- Author:  Abid          
-- Create date: 26 April 2012          
-- Description: Procedure to get project details by ResourceId          
-- =============================================          
      
CREATE PROCEDURE [dbo].[GetProjectUtilizationByPeriod]          
       
        
 @FromDate datetime,          
 @ToDate datetime         
        
           
           
AS          
BEGIN          
 -- SET NOCOUNT ON added to prevent extra result sets from          
 -- interfering with SELECT statements.          
 SET NOCOUNT ON;          
    
select 
pr.ProjectName as [ProjectName],
ph.Phase,

tt.ProjectId,

rl.Role,
rs.ResourceName as [ResourceName],
  
'1' as [BillableHours],'1' as [BudgetHours],
'1' as [RevisedBudgetHours],

isnull(tt.ActualHours,0) as [ActualHours],
ph.PhaseID


from TimeTracker tt
join Project pr on pr.ProjectId=tt.ProjectId
join Phase ph on ph.PhaseID=tt.PhaseID
join [Resource] rs on rs.ResourceId=tt.ResourceId
join [Role] rl on rl.RoleId=tt.RoleId
inner join dbo.ProjectEstimation pe on pe.ProjectID=tt.ProjectID and pe.PhaseID=tt.PhaseID --and pe.EstimationRoleID=tt.RoleId

where

(tt.FromDate)>=@FromDate and (tt.ToDate)<=@ToDate

--(YEAR(tt.FromDate)=@Year and MONTH(tt.FromDate)=@Month) 

and tt.PhaseID not in(1,6) --VAS and Proposal excludes here

and pr.ProjectName Not In('Admin','Open')

group by 
pr.ProjectName,ph.Phase,rl.Role,rs.ResourceName,

tt.ResourceId,tt.ProjectId,tt.FromDate,tt.ToDate,tt.ActualHours,ph.PhaseID

order by pr.ProjectName,ph.PhaseID,rl.Role,rs.ResourceName
   
 END
    
    
GO

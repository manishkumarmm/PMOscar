IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetResourceUtilizationByPeriod') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetResourceUtilizationByPeriod]
/****** Object:  StoredProcedure [dbo].[GetResourceUtilizationByPeriod]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
-- =============================================          
-- Author:  Abid          
-- Create date: 26 April 2012          
-- Description: Procedure to get resource details     
-- =============================================          
      
CREATE PROCEDURE [dbo].[GetResourceUtilizationByPeriod]          
       
        
 @FromDate datetime,          
 @ToDate datetime        
        
           
           
AS          
BEGIN          
 -- SET NOCOUNT ON added to prevent extra result sets from          
 -- interfering with SELECT statements.          
 SET NOCOUNT ON;          
    
SELECT 

--* 

rs.ResourceName,
pr.ProjectName,
isnull(ph.Phase,'NIL') as Phase,
isnull(sum(tt.EstimatedHours),0) as PlannedHours,
isnull(sum(tt.ActualHours),0) as ActualHours,

tt.ResourceId,tt.projectId,
count(tt.ProjectId) as countOfEntryInTimeTrack,

ph.PhaseID
 
  FROM [dbo].[TimeTracker] tt
  
join Project pr on pr.ProjectId=tt.ProjectId
left join Phase ph on ph.PhaseID=tt.PhaseID
join [Resource] rs on rs.ResourceId=tt.ResourceId
join [Role] rl on rl.RoleId=tt.RoleId
left join dbo.ProjectEstimation pe on pe.ProjectID=tt.ProjectID and pe.PhaseID=tt.PhaseID and pe.EstimationRoleID=tt.RoleId

where 

(tt.FromDate)>=@FromDate and (tt.ToDate)<=@ToDate

--and tt.PhaseID not in(1,6) --VAS and Proposal excludes here

--and pr.ProjectName Not In('Admin','Open')

group by tt.ResourceId,tt.[ProjectId],rs.ResourceName,pr.ProjectName,ph.PhaseID,ph.Phase

order by rs.ResourceName,pr.ProjectName
    
    
 END
    
    

GO

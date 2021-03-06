IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetProjectDetailsForProject') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetProjectDetailsForProject]
/****** Object:  StoredProcedure [dbo].[GetProjectDetailsForProject]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetProjectDetailsForProject]          
       
 @ProjectID int,          
 @Year int,          
 @Month int,          
 @dayfrom varchar(15),          
 @dayto varchar(15)          
           
           
AS          
BEGIN          
 -- SET NOCOUNT ON added to prevent extra result sets from          
 -- interfering with SELECT statements.          
 SET NOCOUNT ON;          
          
    SELECT TT.TimeTrackerId AS TimeTrackerId,RS.ResourceId,RS.ResourceName,R.Role,TT.EstimatedHours As EstimatedHours,TT.ActualHours As ActualHours,PH.Phase,T.Team  
    ,case when dt1.TotHours>isnull(Sum(ProjectEstimation.RevisedBudgetHours),0) then 'R' else 'N' end [status],RS.IsActive    
    ,cast(Day(TT.FromDate) as char(3))+'- '+cast(Day(TT.ToDate) as char(3)) AS [Week]  
               
    FROM TimeTracker TT          
    INNER JOIN  Resource RS on TT.ResourceId = RS.ResourceId          
    INNER JOIN Role R  on TT.RoleId = R.RoleId           
    INNER JOIN Project P on P.ProjectId = TT.ProjectId         
    Left JOIN Phase PH on TT.PhaseID = PH.PhaseID    
    Left JOIN Team T on TT.TeamID = T.TeamID   
    Left join ProjectEstimation on  ProjectEstimation.ProjectId = TT.ProjectId  and ProjectEstimation.EstimationRoleID=R.EstimationRoleID              
  and ProjectEstimation.PhaseID=TT.PhaseID     
      
    left join(select       
 tt.ProjectID      
 ,tt.PhaseID      
 ,er.EstimationRoleID      
 ,sum(case when ActualHours is null then EstimatedHours*r.EstimationPercentage else ActualHours*r.EstimationPercentage end) as TotHours      
from       
 Timetracker tt      
 join [Role] r on r.RoleID=tt.RoleID    
 left join [Team] t on t.TeamID = TT.TeamID   
 join EstimationRole er on er.EstimationRoleID=r.EstimationRoleID      
--where       
    
    
  -- TT.FromDate>=@dayfrom and  TT.ToDate<=@dayto      And tt.ProjectId = @ProjectID  
    group by       
 tt.ProjectID      
 ,tt.PhaseID      
 ,er.EstimationRoleID      
)  dt1 on dt1.ProjectID=TT.ProjectID and dt1.PhaseID=TT.PhaseID and      
dt1.EstimationRoleID=R.EstimationRoleID    
      
      
           
    WHERE P.ProjectId = @ProjectID  and (Year(TT.FromDate)= Year(@dayfrom) or Year(TT.ToDate) = Year(@dayto))        
            
    and TT.FromDate>=@dayfrom and  TT.ToDate<=@dayto          
      
     Group By TT.TimeTrackerId ,P.ProjectId,ProjectName,R.Role,TT.EstimatedHours,TT.ActualHours ,PH.Phase,t.Team,dt1.TotHours,RS.ResourceId,RS.ResourceName,RS.IsActive 
     ,TT.FromDate,TT.ToDate
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetActualHoursHistoryNotAdjusted]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetActualHoursHistoryNotAdjusted]
GO
                      
CREATE PROCEDURE [dbo].[GetActualHoursHistoryNotAdjusted]                              
                           
 @ProjectID int,        
  @PhaseID int,                            
 @EstimationRoleID int                             
                            
AS                              
BEGIN                              
       
select ResourceName AS [Resource Name],Phase,[Role],Actuals    
from(        
Select RE.ResourceName,PH.Phase,R.[Role],floor(round(isnull(Sum(TT.ActualHours),0),0)) As Actuals        
from TimeTracker TT inner join [Role] R on TT.RoleId=R.RoleID        
inner join [Resource] RE on TT.ResourceId = RE.ResourceId       
inner join Phase PH on TT.PhaseID=PH.PhaseID        
where R.EstimationRoleID=@EstimationRoleID and TT.PhaseID=@PhaseID and TT.ProjectID=@ProjectID     
group by TT.ProjectId,RE.ResourceId,TT.PhaseID,RE.ResourceName,R.[Role],PH.Phase        
) as t1 where t1.Actuals!=0    
                           
END 
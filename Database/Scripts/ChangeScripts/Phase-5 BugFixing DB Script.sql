
/*=================================================================*/
/* PMOscar Phase-5 BugFixing script	                       */
/* Date: 05-04-2013					                               */
/*=================================================================*/
 -- Update procedure GetProjectDetailsForResource to display multiweeks

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
       
-- EXEC GetProjectDetailsForResource 40,2014,1,'2013-12-30','2014-1-5'
ALTER PROCEDURE [dbo].[GetProjectDetailsForResource]          
       
 @ResourceID int,          
 @Year int,          
 @Month int,          
 @dayfrom varchar(15),          
 @dayto varchar(15)          
           
           
AS          
BEGIN          
 -- SET NOCOUNT ON added to prevent extra result sets from          
 -- interfering with SELECT statements.          
 SET NOCOUNT ON;          
          
    SELECT TT.TimeTrackerId AS TimeTrackerId,P.ProjectId,ProjectName,R.Role,TT.EstimatedHours As EstimatedHours,TT.ActualHours As ActualHours,PH.Phase,T.Team,    
        case when dt1.TotHours>isnull(Sum(ProjectEstimation.RevisedBudgetHours),0) then 'R' else 'N' end [status],P.IsActive
        ,cast(Day(TT.FromDate) as char(3))+'- '+cast(Day(TT.ToDate) as char(3)) AS [Week]
       
    FROM TimeTracker TT          
    INNER JOIN  Resource RS on TT.ResourceId = RS.ResourceId          
    INNER JOIN Role R  on TT.RoleId = R.RoleId           
    INNER JOIN Project P on P.ProjectId = TT.ProjectId         
    LEFT JOIN Phase PH on TT.PhaseID = PH.PhaseID   
    LEFT JOIN Team T on TT.TeamID = T.TeamID   
  Left join ProjectEstimation on  ProjectEstimation.ProjectId = P.ProjectId  and ProjectEstimation.EstimationRoleID=R.EstimationRoleID                
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
          
--   TT.FromDate>=@dayfrom and  TT.ToDate<=@dayto     
       
group by    
       
 tt.ProjectID        
 ,tt.PhaseID        
 ,er.EstimationRoleID        
)  dt1 on dt1.ProjectID=TT.ProjectID and dt1.PhaseID=TT.PhaseID and        
dt1.EstimationRoleID=R.EstimationRoleID  and dt1.ProjectID = P.ProjectID    
           
    WHERE RS.ResourceId = @ResourceID  and (Year(TT.FromDate)= Year(@dayfrom) or Year(TT.ToDate) = Year(@dayto))          
            
    and TT.FromDate>=@dayfrom and  TT.ToDate<=@dayto      
        
    Group By     TT.TimeTrackerId ,P.ProjectId,ProjectName,R.Role,TT.EstimatedHours,TT.ActualHours ,PH.Phase,t.Team, dt1.TotHours ,P.IsActive 
    ,TT.FromDate,TT.ToDate  
             
END 


--************************************************************                 
 -- Update procedure GetProjectDetailsForProject to display multiweeks
 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetProjectDetailsForProject]          
       
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
--************************************************************                 
-- ==============================================================
-- Author     :	Riya
-- Create Date: 08-04-2013
-- Description:	Procedure to get TotalHours,EstimatedHours and ActualHours by given 
-- ==============================================================
--Enhancement : Dashboard should not finalize If TotalHours not equal to total estimated hours and total actual hours 

--EXEC FinalizeDashBoard_GetTotalHours '2013-04-8','2013-04-14'
CREATE PROCEDURE [dbo].[FinalizeDashBoard_GetTotalHours]
@dayfrom VARCHAR(15),
@dayto VARCHAR(15)
AS
BEGIN
	SELECT 
		COUNT(DISTINCT R.ResourceId) NoOfResources
		,CASE WHEN COUNT(DISTINCT R.ResourceId)  > 0 THEN COUNT(DISTINCT R.ResourceId)*40 ELSE 0 END AS TotalHours
		,CASE WHEN Sum(TimeTracker.EstimatedHours) > 0 THEN Sum(TimeTracker.EstimatedHours) ELSE 0 END AS EstimatedHours
		,CASE WHEN Sum(TimeTracker.ActualHours) > 0 THEN Sum(TimeTracker.ActualHours) ELSE 0 END AS ActualHours 
	FROM
	TimeTracker
		JOIN [Resource] R  On TimeTracker.ResourceId = R.ResourceId
		INNER JOIN [Role] On TimeTracker.RoleId = Role.RoleId 
		INNER JOIN Project On Project.ProjectId = TimeTracker.ProjectId 
	WHERE (Year(TimeTracker.FromDate)=YEAR(@dayfrom) AND Year(TimeTracker.ToDate)=YEAR(@dayto)
			AND TimeTracker.FromDate>=@dayfrom 
			AND TimeTracker.ToDate<=@dayto ) 
END

--************************************************************                 
-- ==============================================================
-- Author     :	Riya
-- Create Date: 03-05-2013
-- Description:	Procedure to Get Resource by ID 
-- ==============================================================
--EXEC GetResourceById 
CREATE PROCEDURE [dbo].[GetResourceById]
@ResourceID INT
AS
BEGIN
	SELECT ResourceId,ResourceName,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,IsActive,RoleId,TeamId,BillingStartDate 
	FROM Resource  
	WHERE IsActive = 1 and ResourceId = @ResourceID 
END

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetTimeTrackerAudit') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].GetTimeTrackerAudit
GO

/****** Object:  StoredProcedure [dbo].[GetTimeTrackerAudit]    Script Date: 6/5/2018 3:48:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- ==============================================================================
-- Description: Track Sync Process

-- ==============================================================================
-- Author:  
-- Create date: 2018-04-19
--Modified By : Haritha E.S
--Modified On : 2018-06-05
-- ==============================================================================



              
CREATE PROCEDURE [dbo].[GetTimeTrackerAudit]                  
               
 @ResourceID int,                  
 @Year int,    
 @ProjectID int,    
 @Status char(1) ,               
               
 @DayFrom varchar(15),                
 @DayTo varchar(15)             
                   
                   
AS                  
BEGIN                  
    
if @Status = 1    
Begin    
    
Select     
     
U.FirstName,PJ.ProjectName as [Project Name],     
convert(varchar,TA.InsertedDate,103) +' ' + convert(varchar,TA.InsertedDate,108) As ModifiedDate,     
 convert(varchar,FromDate,106)+' - '+convert(varchar,ToDate,106) As Week,     
ResourceName as [Resource Name],      
[Role],  
[Team],    
P.Phase,       
isnull(OldEstimatedHours,0) as [Estimated Hours(Old)],      
isnull(OldActualHours,0) as [Actual Hours(Old)],  
isnull(NewEstimatedHours,0)  as [Estimated Hours(New)],      
isnull(NewActualHours,0) as  [Actual Hours(New)],
[Action] as Action   
      
From      
dbo.TimeTrackerAudit TA     
Inner Join dbo.[User]  U    
On  TA.UpdatedBy  = U.UserId     
inner join dbo.Resource R       
on TA.ResourceId = R.ResourceId      
left join dbo.Phase P      
on TA.OldPhaseID = P.PhaseID   
left join dbo.Phase PN      
on TA.NewPhaseID = PN.PhaseID     
inner join dbo.Role RO      
on TA.RoleId = RO.RoleId  
Left join dbo.Team T
on TA.TeamID = T.TeamID    
Left join dbo.Project PJ    
on TA.OldProjectID = PJ.ProjectID  
Left join dbo.Project PJN    
on TA.NewProjectID = PJN.ProjectID    
    
 WHERE TA.ResourceId = @ResourceID  and (Year(TA.FromDate)=@Year or Year(TA.ToDate)=@Year)                
                  
  and TA.FromDate>=@DayFrom and  TA.ToDate<=@DayTo    Order By TA.InsertedDate Desc
        
 End    
        
    if @Status = 2    
Begin    
        
    Select     
     
U.FirstName,PJ.ProjectName as [Project Name],    
convert(varchar,TA.InsertedDate,103) +' ' + convert(varchar,TA.InsertedDate,108) As ModifiedDate,     
 convert(varchar,FromDate,106)+' - '+convert(varchar,ToDate,106) As Week,     
ResourceName as [Resource Name],      
[Role],
[Team],      
P.Phase,
isnull(OldEstimatedHours,0) as [Estimated Hours(Old)],      
isnull(OldActualHours,0) as [Actual Hours(Old)],  
isnull(NewEstimatedHours,0)  as [Estimated Hours(New)],      
isnull(NewActualHours,0) as  [Actual Hours(New)] ,
[Action] as Action     
      
From      
dbo.TimeTrackerAudit TA     
Inner Join dbo.[User]  U    
On  TA.UpdatedBy  = U.UserId     
inner join dbo.Resource R       
on TA.ResourceId = R.ResourceId      
left join dbo.Phase P      
on TA.OldPhaseID = P.PhaseID   
left join dbo.Phase PN      
on TA.NewPhaseID = PN.PhaseID     
inner join dbo.Role RO      
on TA.RoleId = RO.RoleId 
Left join dbo.Team T
on TA.TeamID = T.TeamID         
Left join dbo.Project PJ    
on TA.OldProjectID = PJ.ProjectID  
Left join dbo.Project PJN    
on TA.NewProjectID = PJN.ProjectID     
     WHERE TA.OldProjectID = @ProjectID  and (Year(TA.FromDate)=@Year or Year(TA.ToDate)=@Year)                
                  
    and TA.FromDate>=@DayFrom and  TA.ToDate<=@DayTo         Order By TA.InsertedDate Desc
        
        
    End    
        
                
                
END 

GO



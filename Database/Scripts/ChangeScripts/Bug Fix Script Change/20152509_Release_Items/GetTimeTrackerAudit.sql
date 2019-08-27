
              
ALTER PROCEDURE [dbo].[GetTimeTrackerAudit]                  
               
 @ResourceID int,                  
 @Year int,    
 @ProjectID int,    
 @status char(1) ,               
               
 @dayfrom varchar(15),                
 @dayto varchar(15)             
                   
                   
AS                  
BEGIN                  
    
if @Status = 1    
Begin    
    
Select     
     
U.FirstName,PJ.ProjectName,    
convert(varchar,TA.UpdatedDate,106) As ModifiedDate,      
 convert(varchar,FromDate,106)+' - '+convert(varchar,ToDate,106) As Week,     
ResourceName,      
[Role],  
[Team],    
Phase,      
EstimatedHours,      
ActualHours      
      
From      
dbo.TimeTrackerAudit TA     
Inner Join dbo.[User]  U    
On  TA.UpdatedBy  = U.UserId     
inner join dbo.Resource R       
on TA.ResourceId = R.ResourceId      
inner join dbo.Phase P      
on TA.PhaseID = P.PhaseID      
inner join dbo.Role RO      
on TA.RoleId = RO.RoleId  
Left join dbo.Team T
on TA.TeamID = T.TeamID    
inner join dbo.Project PJ    
on TA.ProjectID = PJ.ProjectID     
    
 WHERE TA.ResourceId = @ResourceID  and (Year(TA.FromDate)=@Year or Year(TA.ToDate)=@Year)                
                  
    and TA.FromDate>=@dayfrom and  TA.ToDate<=@dayto    Order By TA.UpdatedDate Desc
        
    End    
        
    if @Status = 2    
Begin    
        
    Select     
     
U.FirstName,PJ.ProjectName,    
convert(varchar,TA.UpdatedDate,106) As ModifiedDate,      
 convert(varchar,FromDate,106)+' - '+convert(varchar,ToDate,106) As Week,     
ResourceName,      
[Role],
[Team],      
Phase,      
EstimatedHours,      
ActualHours      
      
From      
dbo.TimeTrackerAudit TA     
Inner Join dbo.[User]  U    
On  TA.UpdatedBy  = U.UserId     
inner join dbo.Resource R       
on TA.ResourceId = R.ResourceId      
inner join dbo.Phase P      
on TA.PhaseID = P.PhaseID      
inner join dbo.Role RO      
on TA.RoleId = RO.RoleId 
Left join dbo.Team T
on TA.TeamID = T.TeamID         
inner join dbo.Project PJ    
on TA.ProjectID = PJ.ProjectID     
     WHERE TA.ProjectId = @ProjectID  and (Year(TA.FromDate)=@Year or Year(TA.ToDate)=@Year)                
                  
    and TA.FromDate>=@dayfrom and  TA.ToDate<=@dayto         Order By TA.UpdatedDate Desc
        
        
    End    
        
                
                
END 
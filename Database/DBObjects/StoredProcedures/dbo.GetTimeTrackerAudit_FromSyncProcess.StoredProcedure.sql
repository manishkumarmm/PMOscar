IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetTimeTrackerAudit_FromSyncProcess') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetTimeTrackerAudit_FromSyncProcess]
/****** Object:  StoredProcedure [dbo].[GetTimeTrackerAudit_FromSyncProcess]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


              
CREATE PROCEDURE [dbo].[GetTimeTrackerAudit_FromSyncProcess]                  
                 
 @Year int,                                 
 @dayfrom varchar(15),                
 @dayto varchar(15)             
                   
                   
AS                  
BEGIN                  
    
   
    
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
    
 WHERE (Year(TA.FromDate)=@Year or Year(TA.ToDate)=@Year)                
                  
    and TA.FromDate>=@dayfrom and  TA.ToDate<=@dayto    Order By TA.UpdatedDate Desc 
        
             
END 


GO

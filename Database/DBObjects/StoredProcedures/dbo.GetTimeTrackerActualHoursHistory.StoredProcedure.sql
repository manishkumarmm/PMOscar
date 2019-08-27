IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetTimeTrackerActualHoursHistory') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetTimeTrackerActualHoursHistory] 


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--[getProjectDashboardDetails] 
-- =============================================
-- Modified by:	  Vibin MB
-- Modified date: 12/06/2018
-- =============================================

  --[GetTimeTrackerActualHoursHistory] '2018','2018-02-26','2018-03-04',6            
CREATE PROCEDURE [dbo].[GetTimeTrackerActualHoursHistory]                  
                 
 @Year int,                                 
 @dayfrom varchar(15),                
 @dayto varchar(15)
 ,@UserId int                  
                   
AS                  
BEGIN                  
    
   Declare @SystemUSerID int
Select @SystemUSerID=userid from dbo.[user] where UserName ='SystemUSer';
    
Select distinct     
     
iif(U.FirstName='SystemUSer','SyncProcess',U.FirstName) as ModifiedUser 

,PJ.ProjectName,    
TA.CreatedDate As ModifiedDate,      
 convert(varchar,FromDate,105)+' - '+convert(varchar,ToDate,105) As Week,     
ResourceName,      
[Role],  
[Team],     

TA.OldActualHours,     
TA.NewActualHours      
From      
dbo.TimeTrackerActualHoursHistory TA 
left join --TimeTracker 
TimeTrackerAudit TT
on TA.TimeTrackerID=TT.TimeTrackerID     
Inner Join dbo.[User]  U    
On  TA.CreatedBy  = U.UserId     
inner join dbo.Resource R       
on TT.ResourceId = R.ResourceId           
inner join dbo.Role RO      
on TT.RoleId = RO.RoleId  
Left join dbo.Team T
on TT.TeamID = T.TeamID    
inner join dbo.Project PJ    
on TT.OldProjectID = PJ.ProjectID     
    
 WHERE -- (U.UserId=@UserId   or U.UserId=@SystemUSerID )          
                  
    --and TA.createddate>=@dayfrom and   TA.createddate<=@dayto 
	--and 
	TT.FromDate>=@dayfrom and   TT.ToDate<=@dayto   
Order By ta.createddate Desc 
        
             
END 


GO
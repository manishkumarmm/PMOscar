IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProjectAudit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetProjectAudit]
GO
                      
CREATE PROCEDURE [dbo].[GetProjectAudit]                          
                       
 @ProjectID int,                          
 @Year int                         
                        
AS                          
BEGIN                          
              
Select               
U.FirstName As Name,              
convert(varchar,PA.UpdatedDate,106) As [Modified Date],              
ProjectName As [Project Name],              
PA.ShortName As [Short Name],              
case when ProjectType='F' then 'Fixed Cost' else 'Time & Material' end As [Project Type], 
ph.Phase As [Current Phase] ,U1.FirstName As [Project Owner],   
U2.FirstName As [Project Manager],              
convert(varchar,DeliveryDate,106) As [Delivery Date],              
convert(varchar,RevisedDeliveryDate,106) As  [Revised Delivery Date],              
PMComments As [PM Comments],              
DeliveryComments As [Delivery Comments]             
              
From              
dbo.ProjectAudit  PA          
Inner Join dbo.[User]  U          
On  PA.UpdatedBy  = U.UserId     
Inner Join dbo.[User]  U1            
 On  PA.ProjectOwner  = U1.UserId      
 Inner Join dbo.[User]  U2      
 On  PA.ProjectManager  = U2.UserId      
 left join Phase ph on ph.PhaseID=PA.PhaseID              
Where ProjectID = @ProjectID    
  
Order By PA.UpdatedDate Desc          
                               
                        
END 
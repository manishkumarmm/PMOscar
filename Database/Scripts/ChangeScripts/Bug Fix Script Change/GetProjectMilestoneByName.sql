IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProjectMilestoneByName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetProjectMilestoneByName]
GO


-- =============================================    
-- Author:  Syam    
-- Create date: <12/12/2016>    
-- Description: <Get milestone by name>    
-- =============================================    
CREATE PROCEDURE [dbo].[GetProjectMilestoneByName]  
 @MilestoneName varchar(10),
 @ProjectID INT         
AS    
BEGIN    
     
 SET NOCOUNT ON;    
	SELECT ProjectMilestoneID, ProjectId, MilestoneName ,StartDate ,EndDate ,[Status]
	FROM ProjectMilestone 
	WHERE MilestoneName = @MilestoneName AND ProjectID = @ProjectID   
    
END 



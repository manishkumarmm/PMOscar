IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetProjectDetailsById') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetProjectDetailsById]
/****** Object:  StoredProcedure [dbo].[GetProjectDetailsById]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
    
CREATE Procedure [dbo].[GetProjectDetailsById]        
    
@ProjectID int 

AS        
        
Begin       
        
	Select DISTINCT P.ProjectId, P.ProjectName, P.ShortName, P.ProjectCode,(B.FirstName+' '+B.LastName) AS ProjectOwner, (C.FirstName+' '+C.LastName) AS ProjectManager,
	P.ProgId AS ProgramId, PG.ProgName AS ProgramName, PH.Phase AS CurrentPhase
	From Project P
	INNER JOIN [User] B on P.ProjectOwner = B.UserID                                      
	INNER JOIN [User] C on P.ProjectManager = C.UserID 
	INNER JOIN Program PG ON P.ProgId = PG.ProgId
	INNER JOIN ProjectDashboard PD ON PD.ProjectId = P.ProjectId
	INNER JOIN Phase PH ON PH.PhaseID=PD.PhaseID
	where P.ProjectId=@ProjectID   
	
End 
GO

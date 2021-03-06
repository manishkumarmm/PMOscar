IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetProjectsByProgramID') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetProjectsByProgramID]
/****** Object:  StoredProcedure [dbo].[GetProjectsByProgramID]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
    
CREATE Procedure [dbo].[GetProjectsByProgramID]        
    
@ProgramID int=0    

AS        
        
Begin        
    IF @ProgramID = 0    
		Select * From Project Order By ProjStartDate DESC
	ELSE		      
        Select * From Project where ProgId=@ProgramID Order By ProjStartDate DESC
    
End 
GO

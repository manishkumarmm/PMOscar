IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetPrograms') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPrograms]
/****** Object:  StoredProcedure [dbo].[GetPrograms]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Exec GetPrograms
    
CREATE Procedure [dbo].[GetPrograms]        
@ProgramID int =0        
AS        
        
Begin        
        
SELECT ProgId AS ProgramID, ProgName AS ProgramName FROM Program 
WHERE (ProgId = @ProgramID OR @ProgramID =0)
ORDER BY ProgName        
        
End 
GO

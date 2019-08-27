
/****** Object:  StoredProcedure [dbo].[GetProjectNameById]    Script Date: 09/19/2012 10:18:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProjectNameById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetProjectNameById]
GO


/****** Object:  StoredProcedure [dbo].[GetProjectNameById]    Script Date: 09/19/2012 10:18:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
   
    
Create Procedure [dbo].[GetProjectNameById]        
    
@projectId int 

AS        
        
Begin       
        
	select ProjectName from Project where ProjectId =@projectId 
	
End 

GO



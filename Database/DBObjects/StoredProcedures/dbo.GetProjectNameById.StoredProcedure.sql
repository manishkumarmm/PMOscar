IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetProjectNameById') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetProjectNameById]
/****** Object:  StoredProcedure [dbo].[GetProjectNameById]    Script Date: 4/19/2018 1:02:16 PM ******/
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

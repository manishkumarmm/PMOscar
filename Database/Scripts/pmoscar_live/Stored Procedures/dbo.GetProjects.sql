SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetProjects]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetProjects]
GO



CREATE Procedure [dbo].[GetProjects]    
(    
    
@Proj_Id int    
    
)    
AS    
    
Begin    
    
Select * From Project where IsActive=1 Order By ProjectName    
    
End  
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


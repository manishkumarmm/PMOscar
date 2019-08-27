SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetUserRole]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetUserRole]
GO



CREATE Procedure [dbo].[GetUserRole]    
(    
    
@Role_Id int    
    
)    
AS    
    
Begin    
    
Select * From UserRole  Order By UserRole   
    
End 
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


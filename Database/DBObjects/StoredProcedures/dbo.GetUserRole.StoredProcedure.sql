IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetUserRole') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUserRole]
/****** Object:  StoredProcedure [dbo].[GetUserRole]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
    
CREATE Procedure [dbo].[GetUserRole]        
       
AS        
        
Begin        
        
Select * From UserRole  Order By UserRole       
        
End 
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetRole') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetRole]
/****** Object:  StoredProcedure [dbo].[GetRole]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetRole]      
     
AS      
      
Begin      
      
Select * From Role  Order By Role     
      
End 
GO

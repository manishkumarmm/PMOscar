SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetRole]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetRole]
GO

CREATE Procedure [dbo].[GetRole]  
(  
  
@Role_Id int  
  
)  
AS  
  
Begin  
  
Select * From Role  Order By Role 
  
End  
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


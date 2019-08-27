SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spDeleteNewUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spDeleteNewUser]
GO



CREATE PROCEDURE [dbo].[spDeleteNewUser]  

  @UserId int
   
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
 Delete from [User]
     
     Where UserId = @UserId
  
     
END  
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


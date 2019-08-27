IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'spDeleteNewUser') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spDeleteNewUser]
/****** Object:  StoredProcedure [dbo].[spDeleteNewUser]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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

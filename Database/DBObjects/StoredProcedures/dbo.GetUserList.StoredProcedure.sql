IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetUserList') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUserList]
/****** Object:  StoredProcedure [dbo].[GetUserList]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================    
-- Author:  Abid    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- Modified By:Joshwa
-- Modified On:06/20/2018
-- =============================================    
CREATE PROCEDURE [dbo].[GetUserList]    
 
 
 @UserStatus varchar(10)
         
AS    
BEGIN    
     
 SET NOCOUNT ON;    
   IF @UserStatus='1'
    
   BEGIN
   SELECT         
   UserId        
  ,UserName      
  ,FirstName      
  ,LastName      
  ,UserRole.UserRole        
  ,IsActive        
  ,CASE IsActive         
   WHEN 'True' THEN 'Active'        
   ELSE 'Inactive'        
  END As Status        
  FROM [User]       
        
  Inner Join UserRole on UserRole.UserRoleID = [User].UserRoleID   where IsActive=1  order by FirstName
   END
   
   ELSE IF @UserStatus='2'  
   
    BEGIN     
        
   SELECT         
   UserId        
  ,UserName      
  ,FirstName      
  ,LastName      
  ,UserRole.UserRole        
  ,IsActive        
  ,CASE IsActive         
   WHEN 'True' THEN 'Active'        
   ELSE 'Inactive'        
  END As Status        
  FROM [User]       
        
  Inner Join UserRole on UserRole.UserRoleID = [User].UserRoleID   where IsActive=0 order by FirstName 
   END
   
   ELSE IF @UserStatus='3'  
   
    BEGIN     
        
   SELECT         
   UserId        
  ,UserName      
  ,FirstName      
  ,LastName      
  ,UserRole.UserRole        
  ,IsActive        
  ,CASE IsActive         
   WHEN 'True' THEN 'Active'        
   ELSE 'Inactive'        
  END As Status        
  FROM [User]       
        
  Inner Join UserRole on UserRole.UserRoleID = [User].UserRoleID  order by FirstName
   END
        
END 
GO

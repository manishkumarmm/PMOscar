
-- =============================================        
-- Author:  Abid        
-- Create date: <Create Date,,>        
-- Description: <Description,,>        
-- =============================================        
ALTER PROCEDURE [dbo].[GetUserList] 
(
	 @UserStatus varchar(10)
  )   
        
AS        
BEGIN        
         
 IF  @UserStatus='1'  
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
        
  Inner Join UserRole on UserRole.UserRoleID = [User].UserRoleID   where IsActive=1  
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
        
  Inner Join UserRole on UserRole.UserRoleID = [User].UserRoleID   where IsActive=0  
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
        
  Inner Join UserRole on UserRole.UserRoleID = [User].UserRoleID    
   END
        
END 
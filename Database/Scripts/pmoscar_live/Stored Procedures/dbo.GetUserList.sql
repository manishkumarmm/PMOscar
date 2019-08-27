SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetUserList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetUserList]
GO



-- =============================================    
-- Author:  Abid    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
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
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUserList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================    
-- Author:  Deepa    
-- Modified date: <12/01/2016>    
-- Description: <For Listing Client based on Status active or not>    
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
        
  Inner Join UserRole on UserRole.UserRoleID = [User].UserRoleID   where IsActive=1  order by UserName ASC
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
        
  Inner Join UserRole on UserRole.UserRoleID = [User].UserRoleID   where IsActive=0  order by UserName ASC
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
        
  Inner Join UserRole on UserRole.UserRoleID = [User].UserRoleID    order by UserName ASC
   END
        
END 

GO



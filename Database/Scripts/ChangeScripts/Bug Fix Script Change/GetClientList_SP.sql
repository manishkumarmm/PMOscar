IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetClientList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetClientList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================    
-- Author:  Deepa    
-- Create date: <11/23/2016>    
-- Description: <For Listing Client based on Status active or not>    
-- =============================================    
CREATE PROCEDURE [dbo].[GetClientList]    
 
 
 @ClientStatus varchar(10)
         
AS    
BEGIN    
     
 SET NOCOUNT ON;    
   IF @ClientStatus='1'
    
   BEGIN
   SELECT         
   ClientId        
  ,ClientName      
  ,ClientCode        
  ,IsActive        
  ,CASE IsActive         
   WHEN 'True' THEN 'Active'        
   ELSE 'Inactive'        
  END As Status        
  FROM [Client]       
        
  where IsActive=1  order by ClientName ASC
   END
   
   ELSE IF @ClientStatus='2'  
   
    BEGIN     
        
   SELECT         
   ClientId        
  ,ClientName      
  ,ClientCode        
  ,IsActive            
  ,CASE IsActive         
   WHEN 'True' THEN 'Active'        
   ELSE 'Inactive'        
  END As Status        
  FROM [Client]       
        
 where IsActive=0  order by ClientName ASC

   END
   
   ELSE IF @ClientStatus='3'  
   
    BEGIN     
        
   SELECT         
   ClientId        
  ,ClientName      
  ,ClientCode        
  ,IsActive            
  ,CASE IsActive          
   WHEN 'True' THEN 'Active'        
   ELSE 'Inactive'        
  END As Status        
  FROM [Client]   order by ClientName ASC
   END
        
END 


GO



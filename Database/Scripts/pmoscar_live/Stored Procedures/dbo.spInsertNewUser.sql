SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spInsertNewUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spInsertNewUser]
GO



-- =============================================  
-- Author:  Abid  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[spInsertNewUser]  
   
 @FirstName varchar(50),  
 @LastName varchar(50),  
 @UserName varchar(50),  
 @Password varchar(50),  
 @EmailId varchar(50),  
 @Status varchar(15) out,
 @CreatedBy int,
 @Createddate datetime,
 @UpdatedBy int,
 @UpdatedDate datetime ,
 @MiddleName varchar(50)   
   
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
     
   Begin Transaction  
     
     
   IF not exists(select UserName from [dbo].[User] where UserName=@UserName)  
     
   BEGIN  
    insert into [dbo].[User] 
    (
    UserName,
    Password,
    EmailId,
    FirstName,
    LastName,
    MiddleName,
    CreatedBy,
    Createddate,
    UpdatedBy,
    UpdatedDate
    
    )  
    
     values
     (
     @UserName,
     @Password,
     @EmailId,
     @FirstName,
     @LastName,
     @MiddleName,
     @CreatedBy,
     @Createddate,
     @UpdatedBy,
     @UpdatedDate
     )  
   END  
     
  ELSE  
       BEGIN  
       SET @Status='invalid'  
       RETURN  
   END  
         
   IF @@error <> 0  
 BEGIN  
 ROLLBACK TRANSACTION  
 RAISERROR('Unable to Insert User', 16, 1)  
 RETURN  
  END        
     
     
   COMMIT TRANSACTION  
   SET @Status='true'  
     
END  
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


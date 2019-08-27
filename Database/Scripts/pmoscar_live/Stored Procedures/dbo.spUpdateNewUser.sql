SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spUpdateNewUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spUpdateNewUser]
GO



-- =============================================  
-- Author:  Murali  
-- Create date: <21/2/2011>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[spUpdateNewUser]  
   
 @FirstName varchar(50),  
 @LastName varchar(50),  
 @UserName varchar(50),  
 @Password varchar(50),  
 @EmailId varchar(50),  
 @Status varchar(15) out,
 @UserId int,
 @UpdatedBy int,
 @UpdatedDate datetime 
   
   
   
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
     
   
    Update [dbo].[User] 
    
    Set
    
     UserName = @UserName,    
     FirstName=@FirstName,
     LastName=@LastName,
     EmailId = @EmailId,     
     UpdatedBy = @UpdatedBy,
     UpdatedDate = @UpdatedDate,
     [Password]=@Password
     Where UserId = @UserId
     
      set  @Status='true';
      return;
     
END  
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


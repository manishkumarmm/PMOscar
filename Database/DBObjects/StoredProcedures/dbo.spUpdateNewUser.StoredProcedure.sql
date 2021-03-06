IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'spUpdateNewUser') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spUpdateNewUser]
/****** Object:  StoredProcedure [dbo].[spUpdateNewUser]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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

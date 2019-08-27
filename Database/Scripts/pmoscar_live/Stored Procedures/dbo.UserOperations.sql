SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UserOperations]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[UserOperations]
GO



-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[UserOperations]  
   
 @FirstName varchar(50),    
 @LastName varchar(50),    
 @UserName varchar(50),    
 @Password varchar(50),    
 @EmailId varchar(50),    
 @Status varchar(15)  out,  
 @CreatedBy int,  
 @Createddate datetime,  
 @UpdatedBy int,  
 @UpdatedDate datetime ,  
 @MiddleName varchar(50),  
 @userID int,
 @IsActive bit,
 @UserRoleID int  
   
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
if (@Status='I')  
begin   
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
    UpdatedDate,
    IsActive,
    UserRoleID  
      
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
     @UpdatedDate,
     @IsActive,
     @UserRoleID    
     )    
     SET @Status='t'    
     return;  
   END    
       
  ELSE    
       BEGIN    
       SET @Status='i'    
       RETURN    
   END   
     
   
END  
  
else if (@Status='U')  
begin  
  
IF not exists(select UserName from [dbo].[User] where UserName=@UserName and UserId<>@userID)    
       
   BEGIN   
     
   Update [dbo].[User]   
      
    Set      
     UserName = @UserName,      
     FirstName=@FirstName,  
     LastName=@LastName,  
     MiddleName=@MiddleName,  
     EmailId = @EmailId,       
     UpdatedBy = @UpdatedBy,  
     UpdatedDate = @UpdatedDate,
     IsActive = @IsActive,
     UserRoleID = @UserRoleID ,
     Password=@Password
          
     Where UserId = @UserId  
       
      set  @Status='t';  
      return;  
     
   END   
else  
   BEGIN    
       SET @Status='i'    
       RETURN    
   end   
END   
else if (@Status='D')  
begin  
  Update [User]
  Set IsActive = 0
     Where UserId = @UserId 
end   
  
  
END  
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


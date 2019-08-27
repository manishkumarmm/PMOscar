IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'UserOperations') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserOperations]



/****** Object:  StoredProcedure [dbo].[UserOperations]    Script Date: 2018-05-31 17:01:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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
 @UserRoleID int ,
 @EmployeeCode varchar(25) 
   
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
    UserRoleID ,
	EmployeeCode
      
    )    
      
     values  
     (  
     @UserName,  
     @Password,  
     @UserName,--@EmailId,  
     @FirstName,  
     @LastName,  
     @MiddleName,  
     @CreatedBy,  
     @Createddate,  
     @UpdatedBy,  
     @UpdatedDate,
     @IsActive,
     @UserRoleID,
	 @EmployeeCode    
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
     EmailId = @UserName,--@EmailId,       
     UpdatedBy = @UpdatedBy,  
     UpdatedDate = @UpdatedDate,
     IsActive = @IsActive,
     UserRoleID = @UserRoleID ,
	 EmployeeCode=@EmployeeCode,
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
  
     Declare @isactive1 bit;
     set @isactive1=(Select IsActive from [User] where UserId = @UserId );
      if @isactive1='1'
       begin
       Update [User]  Set IsActive = 0
     Where UserId = @UserId     
       end
      else
       begin
        Update [User]  Set IsActive = 1
     Where UserId = @UserId 
       end     
     
end   
  
  
END  

GO


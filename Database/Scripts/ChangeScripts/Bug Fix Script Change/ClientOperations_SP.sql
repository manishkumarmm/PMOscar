IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClientOperations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ClientOperations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[ClientOperations]  
 @ClientID int,
 @ClientName varchar(50),    
 @ClientCode varchar(3),    
 @Status varchar(15)  out,
 @IsActive bit
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
if (@Status='I')  
begin   
IF not exists(select ClientName from [dbo].[Client] where ClientName=@ClientName)    
       
   BEGIN    
    insert into [dbo].[Client]   
    (  

ClientName,
ClientCode,
 IsActive     
    )    
      
     values  
     (  

     @ClientName,  
     @ClientCode,
	 @IsActive
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
  
IF not exists(select ClientName from [dbo].[Client] where ClientName=@ClientName and ClientId<>@ClientID)    
       
   BEGIN   
     
   Update [dbo].[Client]   
      
    Set      
     ClientName = @ClientName, 
	 ClientCode=@ClientCode ,
	  IsActive = @IsActive   
     Where ClientId = @ClientID 
       
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
     set @isactive1=(Select IsActive from [Client] where ClientId = @ClientId );
      if @isactive1='1'
       begin
       Update [Client]  Set IsActive = 0
     Where ClientId = @ClientID  
       end
      else
       begin
        Update [Client]  Set IsActive = 1
     Where ClientId = @ClientId 
       end     
     
end   
  
END  

GO



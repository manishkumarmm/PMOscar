IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResourceOperations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ResourceOperations]
GO
       
CREATE PROCEDURE [dbo].[ResourceOperations]      
(         
@ResourceId  int, 
@ResourceStatus varchar(10),     
@ResourceName VARCHAR(50),      
@CreatedBy int,      
@CreatedDate datetime,      
@UpdatedBy int,      
@UpdatedDate datetime,      
@OpMode varchar(10),    
@IsActive bit,  
@RoleId int,      
@TeamID int,
@BillingStartDate datetime ='01-01-2005', 
@CostCentreID int ,
@emp_Code   varchar(50),
@WeeklyHour INT
)      
AS      
      
Begin      
  If @OpMode = 'INSERT'      
        
   Begin      
      
    Insert into Resource       
    (      
      
     ResourceName,      
     CreatedBy,      
     CreatedDate,      
     UpdatedBy,      
     UpdatedDate,    
     IsActive,  
     RoleId,
     TeamID,
     BillingStartDate ,
     emp_Code,
     CostCentreID,
     WeeklyHour   
    )       
         
   Values      
   (  
     @ResourceName,      
     @CreatedBy,      
     @CreatedDate,      
     @UpdatedBy,      
     @UpdatedDate,    
     @IsActive,  
     @RoleId,
     @TeamID,     
     @BillingStartDate ,
     @emp_Code,
     @CostCentreID,
     @WeeklyHour
   )      
      
   End      
      
 If @OpMode = 'UPDATE'      
        
   Begin      
        
    Update Resource        
        
    Set     
     IsActive =@IsActive,      
     ResourceName = @ResourceName,      
     UpdatedBy = @UpdatedBy,      
     UpdatedDate = @UpdatedDate,  
     RoleId = @RoleId,
     TeamID = @TeamID,         
     BillingStartDate = @BillingStartDate,
     emp_Code=@emp_Code,
     CostCentreID=@CostCentreID,
     WeeklyHour = @WeeklyHour
    Where ResourceId = @ResourceId       
        
   End      
        
 If @OpMode = 'DELETE'      
        
   Begin   
   
   Declare @isactive1 bit;
     set @isactive1=(Select IsActive from Resource where ResourceId = @ResourceId);
      if @isactive1='1'
       begin
        Update Resource   
       Set        
		IsActive = 0        
		Where ResourceId = @ResourceId        
       end
      else
       begin
         Update Resource   
       Set        
		IsActive = 1        
		Where ResourceId = @ResourceId       
       end     
  End      
         
 If @OpMode = 'SELECTALL'    
        
     
        
    Begin         
        if @ResourceStatus='1'
        
        begin
		SELECT     
		ResourceId    
	   ,ResourceName    
	   ,IsActive  
	   ,Role
	   ,Team ,
	   CostCentre 
	   ,CASE IsActive     
	   WHEN 'True' THEN 'Active'    
	   ELSE 'Inactive'    
	   END As Status1    
	   FROM Resource 
	   Inner Join Role on Role.RoleID = Resource.RoleID Left join Team on Team.TeamID = Resource.TeamID  left join CostCentre on CostCentre.CostCentreId=Resource.CostCentreID where IsActive=1 Order By ResourceName 
      
		End  
		
		else if @ResourceStatus='2'
		begin
			SELECT     
			ResourceId    
			,ResourceName    
			,IsActive  
			,Role
			 ,Team ,
			CostCentre 
			,CASE IsActive     
			WHEN 'True' THEN 'Active'    
			ELSE 'Inactive'    
			END As Status1    
			FROM Resource 
			Inner Join Role on Role.RoleID = Resource.RoleID Left join Team on Team.TeamID = Resource.TeamID  left join CostCentre on CostCentre.CostCentreId=Resource.CostCentreID where IsActive=0 Order By ResourceName 
			
		end
		
		else if @ResourceStatus='3'
		begin
		
		SELECT     
			ResourceId    
			,ResourceName    
			,IsActive  
			,Role
			 ,Team ,
			CostCentre 
			,CASE IsActive     
			WHEN 'True' THEN 'Active'    
			ELSE 'Inactive'    
			END As Status1    
			FROM Resource 
			Inner Join Role on Role.RoleID = Resource.RoleID Left join Team on Team.TeamID = Resource.TeamID  left join CostCentre on CostCentre.CostCentreId=Resource.CostCentreID  Order By ResourceName 
		end
   End    
      
End  




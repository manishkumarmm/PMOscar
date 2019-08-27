SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[trg_Update_Project]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[trg_Update_Project]
GO

CREATE trigger trg_Update_Project on     
  Project     
for update         
as        
begin        
         
        
 declare @ProjectID int;        
 declare @ProjectName varchar;        
 declare @ShortName varchar;    
 declare @ProjectType char(1);        
 declare @ProjectOwner int;     
 declare @ProjectManager int ;   
 declare @DeliveryDate datetime     
 declare @RevisedDeliveryDate datetime;        
 declare @PMComments nvarchar;  
 declare @DeliveryComments nvarchar;    
 declare @IsActive  bit;  
 declare @CreatedBy int;  
 declare @CreatedDate datetime;  
 declare @phaseID int;
 declare @ApprvChangeRequest int;
         
 select @ProjectID =ProjectID , @ProjectName = ProjectName, @ShortName =ShortName,@ProjectType=ProjectType,    
 @ProjectOwner=ProjectOwner,@ProjectManager=ProjectManager ,@DeliveryDate = DeliveryDate,@RevisedDeliveryDate =RevisedDeliveryDate,@PMComments=PMComments,  
 @DeliveryComments=DeliveryComments,@IsActive=IsActive,@CreatedBy=CreatedBy,@CreatedDate=CreatedDate,@phaseID=phaseID,@ApprvChangeRequest=ApprvChangeRequest  
  from deleted;        
         
 if @ProjectID!=0    
 begin        
          
  insert into dbo.ProjectAudit(ProjectID,ProjectName,ShortName,ProjectType,ProjectOwner,ProjectManager,DeliveryDate,RevisedDeliveryDate,PMComments,  
  DeliveryComments,IsActive,CreatedBy,CreatedDate,phaseID,ApprvChangeRequest)    
  values(@ProjectID,@ProjectName,@ShortName,@ProjectType,@ProjectOwner,@ProjectManager,@DeliveryDate,@RevisedDeliveryDate,@PMComments,  
  @DeliveryComments,@IsActive,@CreatedBy,@CreatedDate,@phaseID,@ApprvChangeRequest);          
      
          
 end        
         
end
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


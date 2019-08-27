SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProjectOperations]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProjectOperations]
GO


                      
CREATE Procedure [dbo].[ProjectOperations]                          
(                       
                      
                      
@ProjectCode varchar(50),                           
@ProjectId int ,                          
@ProjectName varchar(50),                          
@ShortName varchar(50),                        
@PhaseID int,                        
@ProjectType char(1),                        
@ProjectOwner int,                        
@ProjectManager int,    
 @ProjectManagerName varchar(30),                     
@DeliveryDateText nvarchar(20),                        
@RevisedDeliveryDateText nvarchar(20),  
@ProjStartDateText nvarchar(20),                     
@MaintClosingDateText nvarchar(20),         
        
@ApprvChangeRequest int,                        
@PMComments nvarchar(max),                        
@DeliveryComments nvarchar(max),                        
@isActive bit,                        
@UpdatedBy int,                        
@UpdatedDate datetime,                          
@CreatedBy int,                            
@CreatedDate datetime,                          
@OpMode varchar(10),  
@CostCentreID int,                      
@status varchar(15) out ,                       
               
@POComments nvarchar(max) ,                
@devurl varchar(200),                
@qaurl varchar(200),                
@demourl varchar(200),                
@productionurl varchar(200),            
@ClientId int,              
@ProgramId int,              
@Utilization char(1),
@ShowInDashboard int
                      
)                          
AS                          
                          
Begin    
   
Declare @RevisedDeliveryDate datetime,  
 @DeliveryDate datetime,  
 @ProjStartDate datetime,                      
 @MaintClosingDate datetime  
       
set @RevisedDeliveryDate =   convert(datetime,  @RevisedDeliveryDateText,          103)   
set @DeliveryDate =   convert(datetime,  @DeliveryDateText,          103)   
set @ProjStartDate =   convert(datetime,  @ProjStartDateText,          103)   
set @MaintClosingDate =   convert(datetime,  @MaintClosingDateText,          103)   
  
  
           
                          
  If @OpMode = 'UPDATE'                        
                             
    BEGIN                     
      IF not exists(select *,ProjectName from [dbo].Project where ProjectName=@ProjectName and  ProjectId != @ProjectId)                                                
		BEGIN                   
			 IF not exists(select *,ProjectName from [dbo].Project where  ShortName= case when @ShortName = '' then '%' else @ShortName  end and  ProjectId != @ProjectId)                          
				BEGIN                   
					IF not exists(select *,ProjectName from [dbo].Project where ProjectCode = case when @ProjectCode = '' then '%' else @ProjectCode end and  ProjectId != @ProjectId)                          
						Begin         
							Update Project Set     
							ProjectCode = @ProjectCode,ProjectName = @ProjectName,ShortName = @ShortName,ProjectType=@ProjectType,
							ProjectOwner=@ProjectOwner,ProjectManager=@ProjectManager,                        
							DeliveryDate=@DeliveryDate,RevisedDeliveryDate=@RevisedDeliveryDate,                        
							PhaseID=@PhaseID,ApprvChangeRequest=@ApprvChangeRequest,PMComments=@PMComments,POComments = @POComments, 
							DeliveryComments=@DeliveryComments,IsActive=@IsActive,UpdatedBy = @UpdatedBy,UpdatedDate = @UpdatedDate, 
							ProjStartDate =  @ProjStartDate,MaintClosingDate=@MaintClosingDate,DevURL =@DevURL,DemoURL=@DemoURL, 
							QaURL=@QaURL,ProductionURL=@ProductionURL,ClientId = @ClientId,ProgId = @ProgramId,Utilization = @Utilization ,
							CostCentreID=@CostCentreID,ShowInDashboard=@ShowInDashboard
							Where ProjectId = @ProjectId  return @ProjectId;                         
						END               
					else  begin SET  @status = '3';end                     
				End  
			else begin SET  @status = '2';end       
		End                  
    else  begin SET   @status = '1'; end                          
  END    
                        
--************************************************************                                 
  If @OpMode = 'INSERT'                                        
   Begin                                    
		IF not exists(select *,ProjectName from [dbo].Project where ProjectName=@ProjectName)                                                       
			BEGIN                   
				IF not exists(select *,ProjectName from [dbo].Project where ShortName= case when @ShortName = '' then '%' else @ShortName  end)                          
					BEGIN                   
						IF not exists(select *,ProjectName from [dbo].Project where ProjectCode = case when @ProjectCode = '' then '%' else @ProjectCode end)                          
							Begin          
							   Insert into Project                               
							   (                          
								 ProjectCode,ProjectName,ShortName,ProjectType,ProjectOwner,ProjectManager,DeliveryDate,RevisedDeliveryDate,                        
								 PhaseID,ApprvChangeRequest,PMComments,DeliveryComments,IsActive,CreatedBy,CreatedDate,UpdatedBy,
								 UpdatedDate,ProjStartDate,MaintClosingDate,POComments,DevURL,DemoURL,QaURL,ProductionURL, ClientId,ProgId,Utilization,CostCentreID ,ShowInDashboard
							   )                               
							   Values                              
							  (                          
								@ProjectCode,@ProjectName,@ShortName,@ProjectType,@ProjectOwner,@ProjectManager,@DeliveryDate,
								@RevisedDeliveryDate,@PhaseID,@ApprvChangeRequest,@PMComments,@DeliveryComments,@IsActive, @CreatedBy,
								@CreatedDate,@UpdatedBy,@UpdatedDate,@ProjStartDate,@MaintClosingDate,@POComments,@DevURL,@DemoURL,@QaURL,
								@ProductionURL,@ClientId,@ProgramId,@Utilization,@CostCentreID ,@ShowInDashboard                 
							   )return SCOPE_IDENTITY();                                  
							END                       
					    else  begin  SET  @status = '3';  end                     
					End                          
				else  begin  SET  @status = '2';	end                                 
			End                  
		else   begin SET   @status = '1';  end                                  
  End                  
--*************************************************************                                     
    If @OpMode = 'DELETE'                        
    Begin                   
		Declare @isactive1 bit;                      
		set @isactive1=(Select IsActive from Project where ProjectId = @ProjectId);                      
		if @isactive1='1'                      
			begin Update  Project  set  IsActive=0 Where ProjectId = @ProjectId  end                      
		else                      
			begin Update  Project  set  IsActive=1  Where ProjectId = @ProjectId  end                                             
    End                          
	--*************************************************************                        
    If @OpMode = 'SELECT'                        
		Begin                        
			if @ProjectCode='1'                      
				begin                        
					Select prj.ProjectId,ProjectName As ProjectName,C.Clientname,P.ProgName,
					Case prj.IsActive When 1 then 'Active' else 'Inactive' end Status,                      
					prowner.FirstName as ProjOwner,prmanger.FirstName as ProjManager,
					case prj.ProjectType  when 'F' then 'Fixed Cost' when 'T' then 'T & M' end ProjectType,ph.Phase as CurrentPhase,
					case prj.Utilization when 'C' then 'Commercial' when 'S' then 'Semi Commercial' when 'I' then 'Internal' when 'G' then 'GIS' when 'P' then 'Product' else '' end Utilization, 
					sum(pe.BillableHours)BillableHours,sum(pe.BudgetHours)BudgetHours,                      
					sum(pe.RevisedBudgetHours)RevisedBudgetHours,isnull(round(D.ActualHours,0),0)ActualHours                     
					From Project prj                      
					inner join [user] prowner on prowner.UserID=prj.ProjectOwner                       
					inner join [user] prmanger on prmanger.UserID=prj.ProjectManager                      
				                
					left join Client C on C.ClientId = prj.ClientId    
					left join Program P on P.ProgId = prj.ProgId            
				                
					left join Phase ph on ph.PhaseID=prj.PhaseID                      
					left join ProjectEstimation pe on pe.ProjectID=prj.ProjectID              
				                   
					left join (SELECT CASE P.ProjectType  WHEN  'F'  THEN round(isnull(sum(A.ActualHours* B.EstimationPercentage),0),0)
							 ELSE round(isnull(sum(A.ActualHours),0),0) END ActualHours,    
							 A.ProjectId from TimeTracker A inner join Role B on A.RoleID=B.RoleID     
							 inner join Project P on P.ProjectId = A.ProjectId where P.PhaseID not in(1,6) and A.PhaseID not in(1,6)   group by A.ProjectId, P.ProjectType    
							) as D on    prj.ProjectId=D.ProjectId	where prj.IsActive=1  
					group by prj.ProjectId,ProjectName,prj.IsActive,prowner.FirstName,prmanger.FirstName,prj.ProjectType, 
					ph.Phase, prj.Utilization,D.ActualHours  ,C.ClientName,P.ProgName Order by  ProjectName                       
				end                       
			else if @ProjectCode='2'                      
				begin                      
					Select prj.ProjectId,ProjectName As ProjectName,C.Clientname,P.ProgName,
					Case prj.IsActive When 1 then 'Active' else 'Inactive' end Status,                      
					prowner.FirstName as ProjOwner,prmanger.FirstName as ProjManager,
					case prj.ProjectType  when 'F' then 'Fixed Cost' when 'T' then 'T & M' else 'Product' end ProjectType,ph.Phase as CurrentPhase,
					case prj.Utilization when 'C' then 'Commercial' when 'S' then 'Semi Commercial' when 'I' then 'Internal' when 'G' then 'GIS' when 'P' then 'Product' else '' end Utilization,
					sum(pe.BillableHours)BillableHours,sum(pe.BudgetHours)BudgetHours,                      
					sum(pe.RevisedBudgetHours)RevisedBudgetHours,isnull(round(D.ActualHours,0),0)ActualHours                      
					From Project prj                      
					inner join [user] prowner on prowner.UserID=prj.ProjectOwner                       
					inner join [user] prmanger on prmanger.UserID=prj.ProjectManager
					left join Client C on C.ClientId = prj.ClientId            
					left join Program P on P.ProgId = prj.ProgId  
					left join Phase ph on ph.PhaseID=prj.PhaseID                      
					left join ProjectEstimation pe on pe.ProjectID=prj.ProjectID                       
					left join (   SELECT  CASE P.ProjectType  WHEN  'F'  THEN round(isnull(sum(A.ActualHours* B.EstimationPercentage),0),0) 
							 ELSE round(isnull(sum(A.ActualHours),0),0) END ActualHours,     
							 A.ProjectId from TimeTracker A inner join Role B on A.RoleID=B.RoleID       
							 inner join Project P on P.ProjectId = A.ProjectId where P.PhaseID not in(1,6) and A.PhaseID not in(1,6)  group by A.ProjectId, P.ProjectType      
							) as D on    prj.ProjectId=D.ProjectId where prj.IsActive=0   
					group by prj.ProjectId,ProjectName,prj.IsActive,prowner.FirstName,prmanger.FirstName,prj.ProjectType, 
					ph.Phase, prj.Utilization,D.ActualHours ,C.ClientName,P.ProgName Order by  ProjectName                       
				end  
				
				else if @ProjectCode='3'   
				begin
				
				Select prj.ProjectId,ProjectName As ProjectName,C.Clientname,P.ProgName,
					Case prj.IsActive When 1 then 'Active' else 'Inactive' end Status,                      
					prowner.FirstName as ProjOwner,prmanger.FirstName as ProjManager,
					case prj.ProjectType  when 'F' then 'Fixed Cost' when 'T' then 'T & M' end ProjectType,ph.Phase as CurrentPhase,
					case prj.Utilization when 'C' then 'Commercial' when 'S' then 'Semi Commercial' when 'I' then 'Internal' when 'G' then 'GIS' when 'P' then 'Product' else '' end Utilization, 
					sum(pe.BillableHours)BillableHours,sum(pe.BudgetHours)BudgetHours,                      
					sum(pe.RevisedBudgetHours)RevisedBudgetHours,isnull(round(D.ActualHours,0),0)ActualHours                     
					From Project prj                      
					inner join [user] prowner on prowner.UserID=prj.ProjectOwner                       
					inner join [user] prmanger on prmanger.UserID=prj.ProjectManager                      
				                
					left join Client C on C.ClientId = prj.ClientId    
					left join Program P on P.ProgId = prj.ProgId            
				                
					left join Phase ph on ph.PhaseID=prj.PhaseID                      
					left join ProjectEstimation pe on pe.ProjectID=prj.ProjectID              
				                   
					left join (SELECT CASE P.ProjectType  WHEN  'F'  THEN round(isnull(sum(A.ActualHours* B.EstimationPercentage),0),0)
							 ELSE round(isnull(sum(A.ActualHours),0),0) END ActualHours,    
							 A.ProjectId from TimeTracker A inner join Role B on A.RoleID=B.RoleID     
							 inner join Project P on P.ProjectId = A.ProjectId where P.PhaseID not in(1,6) and A.PhaseID not in(1,6)  group by A.ProjectId, P.ProjectType    
							) as D on    prj.ProjectId=D.ProjectId	where prj.IsActive=1 and prmanger.FirstName=@ProjectManagerName
					group by prj.ProjectId,ProjectName,prj.IsActive,prowner.FirstName,prmanger.FirstName,prj.ProjectType, 
					ph.Phase, prj.Utilization,D.ActualHours  ,C.ClientName,P.ProgName   Order by  ProjectName    
				end
				
				                     
			else  if @ProjectCode='4'                       
				begin                      
					Select prj.ProjectId,ProjectName As ProjectName,C.Clientname,P.ProgName,
					Case prj.IsActive When 1 then 'Active' else 'Inactive' end Status,                      
					prowner.FirstName as ProjOwner,prmanger.FirstName as ProjManager,
					case prj.ProjectType  when 'F' then 'Fixed Cost' when 'T' then 'T & M' else 'Product' end ProjectType,ph.Phase as CurrentPhase,
					case prj.Utilization when 'C' then 'Commercial' when 'S' then 'Semi Commercial' when 'I' then 'Internal' when 'G' then 'GIS' when 'P' then 'Product' else '' end Utilization,
					sum(pe.BillableHours)BillableHours,sum(pe.BudgetHours)BudgetHours,                      
					sum(pe.RevisedBudgetHours)RevisedBudgetHours,isnull(round(D.ActualHours,0),0)ActualHours From Project prj
					inner join [user] prowner on prowner.UserID=prj.ProjectOwner                       
					inner join [user] prmanger on prmanger.UserID=prj.ProjectManager 
					left join Client C on C.ClientId = prj.ClientId            
					left join Program P on P.ProgId = prj.ProgId            
				    left join CostCentre CC on CC.CostCentreID=prj.CostCentreID  
					left join Phase ph on ph.PhaseID=prj.PhaseID                      
					left join ProjectEstimation pe on pe.ProjectID=prj.ProjectID                       
					left join (  SELECT  CASE P.ProjectType WHEN  'F'  THEN round(isnull(sum(A.ActualHours* B.EstimationPercentage),0),0)
							 ELSE round(isnull(sum(A.ActualHours),0),0) END ActualHours,    
							 A.ProjectId from TimeTracker A inner join Role B on A.RoleID=B.RoleID     
							 inner join Project P on P.ProjectId = A.ProjectId  where P.PhaseID not in(1,6) and A.PhaseID not in(1,6) group by A.ProjectId, P.ProjectType    
							 ) as D on    prj.ProjectId=D.ProjectId where 1=1   
					group by prj.ProjectId,ProjectName,prj.IsActive,prowner.FirstName,prmanger.FirstName,prj.ProjectType, 
					ph.Phase, prj.Utilization,D.ActualHours,C.ClientName,P.ProgName Order by  ProjectName    
				end                                     
		End                          
	--*************************************************************                      
	 If @OpMode = 'SELECTALL'                        
		Begin                                               
			Select ProjectName,ShortName,ProjectType,ProjectOwner,ProjectManager,DeliveryDate,RevisedDeliveryDate ,PhaseID,                        
				   ApprvChangeRequest,PMComments,DeliveryComments,IsActive,ProjectCode,ProjStartDate,MaintClosingDate,POComments,DevURL,                
				   DemoURL,QaURL,ProductionURL,ClientId,ProgId,ISNULL(Utilization,0) AS Utilization,CostCentreID ,ShowInDashboard From Project prj Where ProjectId = @ProjectId   Order by  ProjectName                                               
		End 
	    
	--*************************************************************                      
                     
End


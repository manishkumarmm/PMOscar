IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'CloneProject')
     DROP PROCEDURE CloneProject
go

-- =============================================    
-- Author:  Anila   
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE PROCEDURE [dbo].[CloneProject]    
 
 
-- Add the parameters for the stored procedure here
	@ProjectID int,
	@ProjectName varchar(50),
	@ProjectShortName varchar(50),
	@UserID int,
	@Status int out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET @Status=0
	DECLARE @NewProjectID int
	
	BEGIN TRAN
	
	IF  EXISTS(SELECT 1 FROM dbo.Project WHERE ProjectName=@ProjectName)
		SET @Status = 1
	ELSE IF EXISTS(SELECT 1 FROM dbo.Project WHERE ShortName=@ProjectShortName)
		SET @Status = 2
	ELSE
	
		BEGIN	
			/************** Insert project details ************/
		
			INSERT INTO Project                               
			   ( ProjectName,ShortName,ProjectType,ProjectOwner,ProjectManager,PhaseID,ApprvChangeRequest,PMComments,DeliveryComments,
				 IsActive,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,POComments,DevURL,DemoURL,QaURL,ProductionURL,ClientId,ProgId,Utilization,CostCentreID
				 ,ShowInDashboard,SVNPath,ProjectDetails           
			   )
				SELECT @ProjectName, @ProjectShortName,ProjectType,ProjectOwner,ProjectManager,PhaseID,ApprvChangeRequest,PMComments,                        
				 DeliveryComments,IsActive,@UserID,GetDate(),@UserID,GetDate(),POComments,DevURL, DemoURL,QaURL,ProductionURL,ClientId,ProgId,Utilization ,CostCentreID 
				 ,ShowInDashboard,SVNPath,ProjectDetails  
				 FROM Project WHERE ProjectId = @ProjectID
			     
			/********** Project details insert ends here **************************/
	         
			SET @NewProjectID = SCOPE_IDENTITY()
	        
			/*********** Insert project estimation details ***********************/
			BEGIN  
	        
			 INSERT INTO dbo.ProjectEstimation(ProjectID,PhaseID,BillableHours,BudgetHours,RevisedBudgetHours,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,EstimationRoleID)  
			 SELECT @NewProjectID,PhaseID,BillableHours,BudgetHours,RevisedBudgetHours,@UserID,GetDate(),@UserID,GetDate(),EstimationRoleID                                
			 FROM  ProjectEstimation         
			 WHERE ProjectID = @ProjectID
	         
			END
	        
			/*********** Project estimation details insert ends here *************/
	        
			
		END	
		
	IF @@Error=0
		COMMIT
	ELSE
		ROLLBACK
END

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'CloneProject') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CloneProject]
GO

/****** Object:  StoredProcedure [dbo].[CloneProject]    Script Date: 18-04-2018 17:24:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================    
-- Author:  Anila   
-- Create date: <Create Date,,>    
-- Description: <Description,,>  
-- Edited by : Vibin mb
-- Edited date: 18/12/2018  
-- =============================================    
CREATE PROCEDURE [dbo].[CloneProject]    
 
 
-- Add the parameters for the stored procedure here
	@ProjectID int,
	@ProjectName varchar(50),
	@ProjectShortName varchar(50),
	@UserID int,
	@Status int out
	,@StartDate varchar(15)
    ,@EndDate varchar(15)
	,@WorkorderID int,
	@BugzillaProjectId int = null
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
				 ,ShowInDashboard,SVNPath,ProjectDetails,ProjStartDate,MaintClosingDate ,WorkOrderID,BugProjectId          
			   )
				SELECT @ProjectName, @ProjectShortName,ProjectType,ProjectOwner,ProjectManager,PhaseID,ApprvChangeRequest,PMComments,                        
				 DeliveryComments,IsActive,@UserID,GetDate(),@UserID,GetDate(),POComments,DevURL, DemoURL,QaURL,ProductionURL,ClientId,ProgId,Utilization ,CostCentreID 
				 ,ShowInDashboard,SVNPath,ProjectDetails,@StartDate,@EndDate,@WorkorderID, @BugzillaProjectId
				 FROM Project WHERE ProjectId = @ProjectID
			     
			/********** Project details insert ends here **************************/
	         
			SET @NewProjectID = SCOPE_IDENTITY()
	        
			/*********** Insert project estimation details ***********************/
			BEGIN  
	        
			 INSERT INTO dbo.ProjectEstimation(ProjectID,PhaseID,BillableHours,BudgetHours,RevisedBudgetHours,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,EstimationRoleID)  
			 SELECT @NewProjectID,PhaseID,BillableHours,BudgetHours,RevisedBudgetHours,@UserID,GetDate(),@UserID,GetDate(),EstimationRoleID                                
			 FROM  ProjectEstimation         
			 WHERE ProjectID = @ProjectID

	--         Insert into ProjectResourceEstimation       
 --   ( 

 --    	 ProjectID,
	--	 PhaseID,   
	--	 ResourceID,
	--	 EstimationRoleID,
	--	 BillableHours,
	--	 EstimatedHours,
	--	 Overhead,
	--	 BudgetHours,
	--	 RevisedBudgetHours,
	--	 CreatedBy,
	--	 CreatedDate,
	--	 UpdatedBy,
	--	 UpdatedDate,
	--	 FromDate,
	--	 ToDate
		 
      
	--)  
	--select @NewProjectID,
	--	 PhaseID, 
	--	 ResourceID,
	--	 EstimationRoleID,
	--	 BillableHours,
	--	 EstimatedHours,
	--	 Overhead,
	--	 BudgetHours,
	--	 RevisedBudgetHours,
	--	 @UserID,
	--	 GETDATE(),
	--	 @UserID,
	--	 GETDATE(),
	--	 FromDate,
	--	 ToDate
		   
	--	 from  ProjectResourceEstimation
	--	  WHERE ProjectID = @ProjectID
			END
	        
			/*********** Project estimation details insert ends here *************/
	        
			
		END	
		
	IF @@Error=0
		COMMIT
	ELSE
		ROLLBACK
END


GO



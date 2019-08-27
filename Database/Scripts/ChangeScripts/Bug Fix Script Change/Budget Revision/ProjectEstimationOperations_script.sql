IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ProjectEstimationOperations')
     DROP PROCEDURE ProjectEstimationOperations
USE [PMOscar_Dev]
GO

/****** Object:  StoredProcedure [dbo].[ProjectEstimationOperations]    Script Date: 08/08/2016 09:04:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--EXEC ProjectEstimationOperations 225,4,3,40,30,30,10,'2013-10-25 11:57:39.050',10,'2013-10-25 11:57:39.050','I',1,''
CREATE PROCEDURE  [dbo].[ProjectEstimationOperations]  
 -- Add the parameters for the stored procedure here  
@ProjectId INT,    
@PhaseID INT,    
@RoleID INT,  
@BillableHours INT,    
@BudgetHours INT,    
@RevisedBudgetHours INT,   
@CreatedBy INT,  
@CreatedDate DATETIME,  
@UpdatedBy INT,  
@UpdatedDate DATETIME,  
@Status CHAR(1),  
@ProjectEstimationID INT,
@Comments NVARCHAR(MAX) = NULL
AS  
BEGIN  
	 -- SET NOCOUNT ON added to prevent extra result sets from  
	 -- interfering with SELECT statements.  
	SET NOCOUNT ON;  
	IF (@Status='I')        
	BEGIN
		DECLARE @ProjectEstiID INT
		SET @ProjectEstiID = 0
	
		INSERT INTO dbo.ProjectEstimation(ProjectID,PhaseID,BillableHours,BudgetHours,RevisedBudgetHours
											,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,EstimationRoleID,Comments)  
		VALUES(@ProjectId,@PhaseID,@BillableHours,@BudgetHours,@RevisedBudgetHours,@CreatedBy
				,@CreatedDate,@UpdatedBy,@UpdatedDate,@RoleID,@Comments);      
		SET @ProjectEstiID = SCOPE_IDENTITY();
		INSERT INTO dbo.ProjectEstimationAudit(ProjectEstimationID,ProjectID,EstimationRoleID,PhaseID
												,BillableHours,BudgetHours,RevisedBudgetHours,CreatedBy
												,UpdatedBy,Comments,CreatedDate,UpdatedDate,RevisedBillableHours)  
			SELECT ProjectEstimationID,ProjectID,EstimationRoleID,PhaseID,BillableHours,BudgetHours
					,RevisedBudgetHours,CreatedBy,UpdatedBy,Comments
					,GETDATE(),GETDATE(),ISNULL(RevisedBillableHours,0) 
			FROM ProjectEstimation 
			WHERE ProjectEstimationID=@ProjectEstiID;  
	END
	ELSE IF (@Status='U')  
	BEGIN
	-----------------------------------------------
	--check for empty then insert as new entry
	       
		DECLARE @Count INT
		SELECT @Count = count(*) 
		FROM  dbo.ProjectEstimation 
		WHERE PhaseID=@PhaseID and EstimationRoleID=@RoleID and ProjectId=@ProjectId
		
		IF (@Count > 0)
		BEGIN
		--update
			
			INSERT INTO dbo.ProjectEstimationAudit(ProjectEstimationID,ProjectID,EstimationRoleID,PhaseID
													,BillableHours,BudgetHours,RevisedBudgetHours
													,CreatedBy,UpdatedBy,Comments
													,NewBillableHours,NewBudgetHours,NewRevisedBudgetHours,
													CreatedDate,UpdatedDate,RevisedBillableHours,NewRevisedBillableHours)  
			SELECT ProjectEstimationID,ProjectID,EstimationRoleID,PhaseID,BillableHours,BudgetHours
					,RevisedBudgetHours,CreatedBy,UpdatedBy,@Comments
					,@BillableHours,@BudgetHours,@RevisedBudgetHours
					,GETDATE(),GETDATE()
					,isnull(RevisedBillableHours,0),0
			FROM ProjectEstimation  
			WHERE ProjectEstimationID=@ProjectEstimationID; 
			
			UPDATE ProjectEstimation 
			SET PhaseID=@PhaseID
				,BillableHours=@BillableHours
				,BudgetHours=@BudgetHours
				,RevisedBudgetHours=@RevisedBudgetHours
				,UpdatedBy=@UpdatedBy
				,UpdatedDate=@UpdatedDate
				,EstimationRoleID=@RoleID
				,Comments=@Comments
				,RevisedBillableHours=0
			WHERE ProjectEstimationID=@ProjectEstimationID;   
			          
		END 
		ELSE
		BEGIN
		 --insert as new Entry
		DECLARE @ProjectEstimID INT
		SET @ProjectEstimID = 0
		
			INSERT INTO dbo.ProjectEstimation(ProjectID,PhaseID,BillableHours,BudgetHours,RevisedBudgetHours
												,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,EstimationRoleID,Comments)  
			VALUES (@ProjectId,@PhaseID,@BillableHours,@BudgetHours,@RevisedBudgetHours,@CreatedBy
					,@CreatedDate,@UpdatedBy,@UpdatedDate,@RoleID,@Comments);  
			SET @ProjectEstimID = SCOPE_IDENTITY();
			
		   INSERT INTO dbo.ProjectEstimationAudit(ProjectEstimationID,ProjectID,EstimationRoleID,PhaseID
												,BillableHours,BudgetHours,RevisedBudgetHours,CreatedBy
												,UpdatedBy,Comments,CreatedDate,UpdatedDate)  
			SELECT ProjectEstimationID,ProjectID,EstimationRoleID,PhaseID,BillableHours,BudgetHours
					,RevisedBudgetHours,CreatedBy,UpdatedBy,Comments,GETDATE(),GETDATE() 
			FROM ProjectEstimation 
			WHERE ProjectEstimationID=@ProjectEstimID;                
		END
	-------------------------------------------
	END   
	ELSE
	BEGIN
		INSERT INTO dbo.ProjectEstimationAudit(ProjectEstimationID,ProjectID,EstimationRoleID,PhaseID
												,BillableHours,BudgetHours,RevisedBudgetHours,CreatedBy
												,UpdatedBy,Comments,CreatedDate,UpdatedDate)  
		SELECT ProjectEstimationID,ProjectID,EstimationRoleID,PhaseID,BillableHours
			,BudgetHours,RevisedBudgetHours,CreatedBy,UpdatedBy
			,'Deleted',GETDATE(),GETDATE()
		FROM ProjectEstimation  
		WHERE ProjectEstimationID=@ProjectEstimationID;
		
		DELETE FROM BudgetRevision  WHERE ProjectEstimationID=@ProjectEstimationID;
		  
		DELETE 
		FROM dbo.ProjectEstimation 
		WHERE ProjectEstimationID=@ProjectEstimationID;  
	END   
END


GO



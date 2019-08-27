
/*=================================================================*/
/* PMOscar database changes script	                       */
/* Date: 10-03-2013					                               */
/*=================================================================*/

-- Adding column 'Comment' into ProjectEstimation table
ALTER TABLE dbo.ProjectEstimation ADD Comments NVARCHAR(MAX) NULL

-- Adding column 'Comment' into ProjectEstimationAudit table
ALTER TABLE dbo.ProjectEstimationAudit ADD Comments NVARCHAR(MAX) NULL

-- Adding column 'Comment' into ProjectEstimationAudit table
ALTER TABLE ProjectEstimationAudit
ADD NewBillableHours INT NOT NULL DEFAULT(0),
	NewBudgetHours INT NOT NULL DEFAULT(0),
	NewRevisedBudgetHours INT NOT NULL DEFAULT(0)
	
/************************************************************************/
-- Updated Procedure to insert Comment 

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--EXEC ProjectEstimationOperations 225,4,3,40,30,30,10,'2013-10-25 11:57:39.050',10,'2013-10-25 11:57:39.050','I',1,''
ALTER PROCEDURE  [dbo].[ProjectEstimationOperations]  
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
												,UpdatedBy,Comments,CreatedDate,UpdatedDate)  
			SELECT ProjectEstimationID,ProjectID,EstimationRoleID,PhaseID,BillableHours,BudgetHours
					,RevisedBudgetHours,CreatedBy,UpdatedBy,Comments
					,GETDATE(),GETDATE() 
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
													CreatedDate,UpdatedDate)  
			SELECT ProjectEstimationID,ProjectID,EstimationRoleID,PhaseID,BillableHours,BudgetHours
					,RevisedBudgetHours,CreatedBy,UpdatedBy,@Comments
					,@BillableHours,@BudgetHours,@RevisedBudgetHours
					,GETDATE(),GETDATE()
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
		DELETE 
		FROM dbo.ProjectEstimation 
		WHERE ProjectEstimationID=@ProjectEstimationID;  
	END   
END

/************************************************************************/
-- Updated Procedure to get Comments 

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
                            
ALTER PROCEDURE [dbo].[GetProjectEstimationAudit]                                  
 @ProjectID INT,                      
 @Year INT                     
  
AS                      
BEGIN                              
	SELECT 
		U.FirstName,CONVERT(VARCHAR,PEA.UpdatedDate,106) AS ModifiedDate,p.Phase,ER.RoleName
		,BillableHours AS OldBillableHours,NewBillableHours
		,BudgetHours AS OldBudgetHours,NewBudgetHours
		,RevisedBudgetHours AS OldRevisedBudgetHours,NewRevisedBudgetHours
		,Comments		      
	FROM dbo.ProjectEstimationAudit PEA        
	INNER JOIN dbo.[User] U ON  PEA.UpdatedBy  = U.UserId       
	INNER JOIN Phase p ON P.PhaseID=PEA.PhaseID    
	INNER JOIN  EstimationRole ER ON ER.EstimationRoleID=PEA.EstimationRoleID           
	WHERE ProjectID = @ProjectID    
	ORDER BY PEA.UpdatedDate  DESC                                         
END 





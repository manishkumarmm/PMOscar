IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProjectEstimationAudit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetProjectEstimationAudit]
GO
                      
CREATE PROCEDURE [dbo].[GetProjectEstimationAudit]                                  
 @ProjectID INT,                      
 @Year INT                     
  
AS                      
BEGIN                              
	SELECT 
		U.FirstName AS Name,CONVERT(VARCHAR,PEA.UpdatedDate,106) AS [Modified Date]
		,p.Phase,ER.RoleName AS [Role]
		,BillableHours AS [Old Billable Hours],NewBillableHours AS [New Billable Hours]
		,BudgetHours AS [Old Budget Hours],NewBudgetHours AS [New Budget Hours]
		,RevisedBudgetHours AS [Old Revised Budget Hours],NewRevisedBudgetHours AS [New Revised Budget Hours]
		,Comments		      
	FROM dbo.ProjectEstimationAudit PEA        
	INNER JOIN dbo.[User] U ON  PEA.UpdatedBy  = U.UserId       
	INNER JOIN Phase p ON P.PhaseID=PEA.PhaseID    
	INNER JOIN  EstimationRole ER ON ER.EstimationRoleID=PEA.EstimationRoleID           
	WHERE ProjectID = @ProjectID    
	ORDER BY PEA.UpdatedDate  DESC                                         
END 

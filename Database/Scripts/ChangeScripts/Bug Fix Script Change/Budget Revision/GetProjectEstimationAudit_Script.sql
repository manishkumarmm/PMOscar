IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetProjectEstimationAudit')
     DROP PROCEDURE GetProjectEstimationAudit
USE [PMOscar_Dev]
GO

/****** Object:  StoredProcedure [dbo].[GetProjectEstimationAudit]    Script Date: 08/08/2016 09:03:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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
		,isnull(RevisedBudgetHours,0) AS [Old Revised Budget Hours],NewRevisedBudgetHours AS [New Revised Budget Hours]
		,isnull(RevisedBillableHours,0) AS [Old Revised Billable Hours],isnull(NewRevisedBillableHours,0) AS [New Revised Billable Hours]
		,Comments		      
	FROM dbo.ProjectEstimationAudit PEA        
	INNER JOIN dbo.[User] U ON  PEA.UpdatedBy  = U.UserId       
	INNER JOIN Phase p ON P.PhaseID=PEA.PhaseID    
	INNER JOIN  EstimationRole ER ON ER.EstimationRoleID=PEA.EstimationRoleID 
	WHERE ProjectID = @ProjectID    
	ORDER BY PEA.UpdatedDate  DESC                                         
END 

GO



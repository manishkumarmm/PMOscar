IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetEstimationForProjects') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetEstimationForProjects]
/****** Object:  StoredProcedure [dbo].[GetEstimationForProjects]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
-- =============================================          
-- Author:  Abid          
-- Create date: 26 April 2012          
-- Description: Procedure to get project details by ResourceId     

--Exec [GetEstimationForProjects] '118,125'      
-- =============================================          
      
CREATE PROCEDURE [dbo].[GetEstimationForProjects]          
               
 @ProjectIDList varchar(MAX)    

AS          
BEGIN          
 -- SET NOCOUNT ON added to prevent extra result sets from          
 -- interfering with SELECT statements.          
 SET NOCOUNT ON;          
    
 DECLARE @SQL varchar(MAX)

    SET @SQL =
		'Select 
			A.ProjectID,A.PhaseID,p.ProjectName,
			B.Phase,
			C.RoleName,
			A.BillableHours,A.BudgetHours,A.RevisedBudgetHours

			from dbo.ProjectEstimation A
 
			inner join Phase B on A.PhaseID=B.PhaseID
			inner join EstimationRole C on  C.EstimationRoleID=A.EstimationRoleID 
			inner join Project p on p.ProjectId=A.ProjectID
			
            where A.ProjectID 
		    IN (' + @ProjectIDList + ')' 
		
		EXEC(@SQL)
    
    
    
 END
    
    
GO

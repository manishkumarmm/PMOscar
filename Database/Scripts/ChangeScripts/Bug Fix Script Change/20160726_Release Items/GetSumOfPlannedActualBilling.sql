IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSumOfPlannedActualBilling')
     DROP PROCEDURE GetSumOfPlannedActualBilling
go
/****** Object:  StoredProcedure [dbo].[GetSumOfPlannedActualBilling]    Script Date: 11/25/2015 13:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- sp to delete billing details       
CREATE PROCEDURE  [dbo].[GetSumOfPlannedActualBilling]  
(      
	@Month int,
	@ProjectID int,
	@Year int
)      
AS   
   Begin  
		DECLARE @Date VARCHAR(15)
		SET @Date =  @Month + '-01-' + CONVERT(VARCHAR(4), @year)
		SELECT pe.BillableHours AS plannedTotal,	
		 sum(bd.ActualHours) + ISNULL(bd2.ActualHours, 0) AS actualTotal
		 FROM BillingDetails bd
		 INNER JOIN Project p ON p.ProjectId = bd.ProjectID	 
		 LEFT JOIN (SELECT ProjectID ,SUM(BillableHours) AS BillableHours FROM ProjectEstimation GROUP BY ProjectID) pe ON pe.ProjectId = p.ProjectID
		 LEFT JOIN (SELECT bd1.ProjectID, SUM(bd1.ActualHours) ActualHours FROM   BillingDetails bd1 
					WHERE bd1.fromDate < @Date GROUP BY ProjectID ) bd2 ON bd2.ProjectID = bd.ProjectId
		 WHERE DATEPART(month,bd.fromDate)=@Month and DATEPART(year,bd.fromDate)=@year AND p.ProjectID=@ProjectID
		 GROUP BY  pe.BillableHours, bd2.ActualHours 
	 
   End

IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetBillingDetailsByMonthYear')
     DROP PROCEDURE GetBillingDetailsByMonthYear
go
/****** Object:  StoredProcedure [dbo].[GetBillingDetailsByMonthYear]    Script Date: 11/16/2015 16:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[GetBillingDetailsByMonthYear] '02',2015
CREATE PROCEDURE [dbo].[GetBillingDetailsByMonthYear]
 @Month NVARCHAR(50),
 @year INT 
AS
BEGIN
 SET NOCOUNT ON;  
	DECLARE @Date VARCHAR(15)
	SET @Date =  @Month + '-01-' + CONVERT(VARCHAR(4), @year)	
	
	 SELECT  bd.ProjectID,p.ProjectName,sum(bd.PlannedHours) AS plannedTotal,sum(bd.ActualHours) AS actualTotal, 
	 (SELECT DISTINCT SUBSTRING(CONVERT(VARCHAR(11),fromDate,106),3,9) FROM BillingDetails
		 WHERE DATEPART(month,fromDate)=@Month and DATEPART(year,fromDate)=@year) AS month
	, bd.Freeze, pe.BillableHours AS totalBillableHrs, ISNULL(bd2.ActualHours, 0) AS totalBilledTillLastMonth, 
	  sum(bd.ActualHours) + ISNULL(bd2.ActualHours, 0) AS totalBilled
	 FROM BillingDetails bd
	 INNER JOIN Project p ON p.ProjectId = bd.ProjectID	 
	 LEFT JOIN (SELECT ProjectID ,SUM(BillableHours) AS BillableHours FROM ProjectEstimation GROUP BY ProjectID) pe ON pe.ProjectId = p.ProjectID
	 LEFT JOIN (SELECT bd1.ProjectID, SUM(bd1.ActualHours) ActualHours FROM   BillingDetails bd1 
				WHERE bd1.fromDate < @Date GROUP BY ProjectID ) bd2 ON bd2.ProjectID = bd.ProjectId
	 WHERE DATEPART(month,bd.fromDate)=@Month and DATEPART(year,bd.fromDate)=@year
	 GROUP BY bd.ProjectID,p.ProjectName,bd.Freeze, pe.BillableHours, bd2.ActualHours 

END


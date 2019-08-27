/****** Object:  StoredProcedure [dbo].[GetBillingDetailsByMonthYear]    Script Date: 10/9/2017 16:13:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetBillingDetailsByMonthYear]
 @Month NVARCHAR(50),
 @year INT 
AS
BEGIN
SET NOCOUNT ON;  
DECLARE @Date VARCHAR(15)
SET @Date =  @Month + '-01-' + CONVERT(VARCHAR(4), @year)    

SElect 
	ProjectID, ProjectName, plannedTotal, actualTotal, [MONTH]
	, Freeze, totalBillableHrs, totalBilledTillLastMonth, totalBilled
	,ActualSpentThisMonth
from (
	SELECT  
		bd.ProjectID,p.ProjectName,sum(bd.PlannedHours) AS plannedTotal
		,sum(bd.ActualHours) AS actualTotal,
		SUBSTRING(CONVERT(VARCHAR(11),CAST(@Date AS Date),106),3,9) AS Month
		, bd.Freeze, pe.BillableHours AS totalBillableHrs
		, ISNULL(bd2.ActualHours, 0) AS totalBilledTillLastMonth,
		sum(bd.ActualHours) + ISNULL(bd2.ActualHours, 0) AS totalBilled
		, sum(bd.ActualSpentHours) AS ActualSpentThisMonth
	FROM BillingDetails bd
	INNER JOIN Project p ON p.ProjectId = bd.ProjectID    
	LEFT JOIN (
		SELECT ProjectID ,SUM(BillableHours) AS BillableHours 
		FROM ProjectEstimation 
		GROUP BY ProjectID
		) pe ON pe.ProjectId = p.ProjectID
	LEFT JOIN (
		SELECT bd1.ProjectID, SUM(bd1.ActualHours) ActualHours 
		FROM   BillingDetails bd1
		WHERE bd1.fromDate < @Date GROUP BY ProjectID 
		) bd2 ON bd2.ProjectID = bd.ProjectId
	WHERE DATEPART(month,bd.fromDate)=@Month and DATEPART(year,bd.fromDate)=@year
	GROUP BY bd.ProjectID,p.ProjectName,bd.Freeze, pe.BillableHours, bd2.ActualHours
	UNION ALL
	select 
		TT.ProjectId, p.ProjectName, 0 AS plannedTotal,0 as actualTotal,
		SUBSTRING(CONVERT(VARCHAR(11),CAST(@Date AS Date),106),3,9) AS Month, 0 AS Freeze,
		pe.BillableHours AS totalBillableHrs
		, 0 AS totalBilledTillLastMonth,
		0 AS totalBilled, 0 AS ActualSpentThisMonth
	from Project p
	INNER JOIN TimeTracker TT ON p.ProjectId = TT.ProjectID    
	LEFT JOIN (
		SELECT TT1.ProjectID, SUM(TT1.ActualHours) ActualHours 
		FROM   TimeTracker TT1
		WHERE TT1.fromDate < @Date GROUP BY ProjectID 
		) tt2 ON tt2.ProjectID = TT.ProjectId
	LEFT JOIN (
		SELECT ProjectID ,SUM(BillableHours) AS BillableHours 
		FROM ProjectEstimation 
		GROUP BY ProjectID
		) pe ON pe.ProjectId = p.ProjectID
	WHERE DATEPART(month,fromDate)=@Month and DATEPART(year,fromDate)=@year
	AND TT.ProjectId not in (select ProjectID from BillingDetails WHERE DATEPART(month,fromDate)=@Month and DATEPART(year,fromDate)=@year)
	and not exists (select 1 from TimeTracker t where t.ProjectId=tt.ProjectId and p.ProjectName in ('Admin','Open')) --su-20170910 - excluding the admin and open projects
	GROUP BY TT.ProjectId, p.ProjectName, tt2.ActualHours, pe.BillableHours
)bh
order by bh.ProjectId, BH.ProjectName


END
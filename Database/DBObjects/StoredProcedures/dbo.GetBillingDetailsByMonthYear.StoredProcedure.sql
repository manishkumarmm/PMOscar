IF EXISTS (SELECT
    *
  FROM sys.objects
  WHERE object_id = OBJECT_ID(N'GetBillingDetailsByMonthYear')
  AND type IN (N'P', N'PC'))
  DROP PROCEDURE [dbo].[GetBillingDetailsByMonthYear]
/****** Object:  StoredProcedure [dbo].[GetBillingDetailsByMonthYear]    Script Date: 4/19/2018 1:02:16 PM ******/
--=================================================================
-- Change History:
-------------------------
-- Name                    Date             Comments
--------------------------------------------------------
-- Joshwa George           08/06/2018      Modified to get actual spent from time tracker
-- Joshwa George           05/07/2018	   Modified to dynamically bind actual hours in billing details grid


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBillingDetailsByMonthYear] @month nvarchar(50),
@year int
AS
BEGIN
  SET NOCOUNT ON;
  DECLARE @date varchar(15)
  SET @date = @month + '-01-' + CONVERT(varchar(4), @year)

  SELECT
    ProjectID,
    ProjectName,
    plannedTotal,
    actualTotal,
    [MONTH],
    Freeze,
    totalBillableHrs,
    totalBilledTillLastMonth,
    totalBilled,
    ActualSpentThisMonth
  FROM (SELECT
    bd.ProjectID,
    p.ProjectName,
    SUM(bd.PlannedHours) AS plannedTotal,
    SUM(bd.ActualHours) AS actualTotal,
    SUBSTRING(CONVERT(varchar(11), CAST(@date AS date), 106), 3, 9) AS Month,
    bd.Freeze,
    pe.BillableHours AS totalBillableHrs,
    ISNULL(bd2.ActualHours, 0) AS totalBilledTillLastMonth,
    SUM(bd.ActualHours) + ISNULL(bd2.ActualHours, 0) AS totalBilled,
    bd3.ActualSpentHours AS ActualSpentThisMonth
  FROM BillingDetails bd
  INNER JOIN Project p
    ON p.ProjectId = bd.ProjectID
  LEFT JOIN (SELECT
    ProjectID,
    SUM(BillableHours) AS BillableHours
  FROM ProjectEstimation
  GROUP BY ProjectID) pe
    ON pe.ProjectId = p.ProjectID
  LEFT JOIN (SELECT
    bd1.ProjectID,
    SUM(bd1.ActualHours) ActualHours
  FROM BillingDetails bd1
  WHERE bd1.fromDate < @date
  GROUP BY ProjectID) bd2
    ON bd2.ProjectID = bd.ProjectId
  LEFT JOIN (SELECT
    ProjectId,
    ISNULL(SUM(tt.ActualHours), 0) AS ActualSpentHours
  FROM Timetracker tt
  WHERE DATEPART(MONTH, fromDate) = @month
  AND DATEPART(YEAR, fromDate) = @Year
  GROUP BY ProjectId) bd3
    ON bd3.ProjectID = p.ProjectId
  WHERE DATEPART(MONTH, bd.fromDate) = @month
  AND DATEPART(YEAR, bd.fromDate) = @year
  GROUP BY bd.ProjectID,
           p.ProjectName,
           bd.Freeze,
           pe.BillableHours,
           bd2.ActualHours,
           bd3.ActualSpentHours
  UNION ALL
 SELECT
    TT.ProjectId,
    p.ProjectName,
    0 AS plannedTotal,
    0 AS actualTotal,
    SUBSTRING(CONVERT(varchar(11), CAST(@date AS date), 106), 3, 9) AS Month,
    0 AS Freeze,
    pe.BillableHours AS totalBillableHrs,
    bd2.ActualHours AS totalBilledTillLastMonth,
    --SUM(bd.ActualHours) +
	ISNULL(bd2.ActualHours, 0) AS totalBilled,
    bd3.ActualSpentHours AS ActualSpentThisMonth
  FROM Project p
  INNER JOIN TimeTracker TT
    ON p.ProjectId = TT.ProjectID
	left join BillingDetails bd ON p.ProjectId = bd.ProjectID
		and DATEPART(MONTH, bd.fromDate) = @month
       AND DATEPART(YEAR, bd.fromDate) = @year
  LEFT JOIN (SELECT
    ProjectID,
    SUM(BillableHours) AS BillableHours
  FROM ProjectEstimation
  GROUP BY ProjectID) pe
    ON pe.ProjectId = p.ProjectID
LEFT JOIN (SELECT
    bd1.ProjectID,
    SUM(bd1.ActualHours) ActualHours
  FROM BillingDetails bd1
  WHERE bd1.fromDate < @date
  GROUP BY ProjectID) bd2
  ON bd2.ProjectID = p.ProjectId
LEFT JOIN (SELECT
    ProjectId,
    ISNULL(SUM(tt.ActualHours), 0) AS ActualSpentHours
  FROM Timetracker tt
  WHERE DATEPART(MONTH, fromDate) = @month
  AND DATEPART(YEAR, fromDate) = @Year
  GROUP BY ProjectId) bd3
    ON bd3.ProjectID = p.ProjectId
  WHERE DATEPART(MONTH, tt.fromDate) = @month
  AND DATEPART(YEAR, tt.fromDate) = @year
  AND TT.ProjectId NOT IN (SELECT
    ProjectID
  FROM BillingDetails bd
  WHERE DATEPART(MONTH, fromDate) = @month
  AND DATEPART(YEAR, fromDate) = @year)

  AND (TT.ActualHours >0 and tt.ActualHours is not null)

  AND NOT EXISTS (SELECT
    1
  FROM TimeTracker t
  WHERE t.ProjectId = TT.ProjectId
  AND p.ProjectName IN ('Admin', 'Open')) --su-20170910 - excluding the admin and open projects
  GROUP BY TT.ProjectId,
           p.ProjectName,
           pe.BillableHours,
		   bd3.ActualSpentHours,bd2.ActualHours) bh
  ORDER BY bh.ProjectName


END
GO
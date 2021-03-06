IF EXISTS (SELECT
    *
  FROM sys.objects
  WHERE object_id = OBJECT_ID(N'GetSumOfPlannedActualBilling')
  AND type IN (N'P', N'PC'))
  DROP PROCEDURE [dbo].[GetSumOfPlannedActualBilling]
/****** Object:  StoredProcedure [dbo].[GetSumOfPlannedActualBilling]    Script Date: 4/19/2018 1:02:16 PM ******/
--======================================================================
-- Change History:
-------------------------
-- Name                    Date             Comments
--------------------------------------------------------
-- Joshwa George           08/06/2018      Modified to get actual spent from time tracker

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- sp to get the sum of planned and actual billing by projectid    

CREATE PROCEDURE [dbo].[GetSumOfPlannedActualBilling] (@month int,
@year int,
@projectId int)
AS
BEGIN
  DECLARE @Date varchar(15)
  SET @Date = CONVERT(varchar, @month) + '-01-' + CONVERT(varchar(4), @year)
  PRINT @Date
  SELECT
    pe.BillableHours AS plannedTotal,
    ISNULL(bd2.ActualHours, 0) AS actualTotal
  FROM BillingDetails bd
  INNER JOIN Project p
    ON p.ProjectId = bd.ProjectID
  LEFT JOIN (SELECT
    ProjectID,
    SUM(PlannedHours) AS BillableHours
  FROM BillingDetails WHERE MONTH(fromDate) = @month
  GROUP BY ProjectID) pe
    ON pe.ProjectId = p.ProjectID
  LEFT JOIN (SELECT
    ProjectId,
    ISNULL(SUM(tt.ActualHours), 0) AS ActualHours
  FROM Timetracker tt
  WHERE tt.ProjectId = @projectId
  AND DATEPART(MONTH, fromDate) = @month
  AND DATEPART(YEAR, fromDate) = @year
  GROUP BY ProjectId) bd2
    ON bd2.ProjectID = bd.ProjectId
  WHERE DATEPART(MONTH, bd.fromDate) = @month
  AND DATEPART(YEAR, bd.fromDate) = @year
  AND p.ProjectID = @projectId
  GROUP BY pe.BillableHours,
           bd2.ActualHours

END
GO
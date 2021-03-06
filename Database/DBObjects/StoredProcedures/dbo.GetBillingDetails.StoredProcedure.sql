IF EXISTS (SELECT
    *
  FROM sys.objects
  WHERE object_id = OBJECT_ID(N'GetBillingDetails')
  AND type IN (N'P', N'PC'))
  DROP PROCEDURE [dbo].[GetBillingDetails]
/****** Object:  StoredProcedure [dbo].[GetBillingDetails]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:  	Dona
-- Create date: 8-5-2012
-- Description:	procedure to get team details for a resource
-- =============================================
-- Change History:
-------------------------
-- Name                    Date             Comments
--------------------------------------------------------
-- Joshwa George           08/06/2018      Modified to get actual spent from time tracker

-- display the billing details for a particular project

CREATE PROCEDURE [dbo].[GetBillingDetails]
-- Add the parameters for the stored procedure here

@editId int,
@billingId int,
@projectId int,
@month int,
@year int

AS
BEGIN
  -- SET NOCOUNT ON added to prevent extra result sets from
  -- interfering with SELECT statements.
  SET NOCOUNT ON;

  SELECT
    r.[Role],
    CONVERT(varchar(12), bd.fromDate, 103) AS fromDate,
    CONVERT(varchar(12), bd.ToDate, 103)
    AS ToDate,
    bd.PlannedHours,
    bd.ActualHours,
    rs.ResourceName,
    bd.ResourceId,
    p.ProjectName,
    p.ProjectId,
    bd.RoleId,
    bd.UBT,
    tt1.ActualSpentHours,
    bd.Comments
  FROM BillingDetails bd
  LEFT JOIN [Resource] rs
    ON rs.ResourceId = bd.ResourceId
  INNER JOIN [Role] r
    ON r.RoleId = bd.RoleId
  INNER JOIN Project p
    ON p.ProjectId = bd.ProjectID
  LEFT JOIN (SELECT
    ResourceId,
    ProjectId,
    roleid,
    ISNULL(SUM(tt.ActualHours), 0) ActualSpentHours
  FROM Timetracker tt
  WHERE tt.ProjectId = @projectId
  AND DATEPART(MONTH, fromDate) = @month
  AND DATEPART(YEAR, fromDate) = @year
  GROUP BY ResourceId,
           ProjectId,
           roleid) tt1
    ON tt1.ResourceId = rs.ResourceId
    AND tt1.ProjectId = bd.ProjectId
    AND tt1.roleid = r.roleid
  WHERE bd.ResourceID = @editId
  AND bd.ProjectID = @projectId
  AND BillingID = @billingId

END
GO
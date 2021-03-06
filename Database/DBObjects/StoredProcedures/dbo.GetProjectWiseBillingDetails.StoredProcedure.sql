IF EXISTS (SELECT
    *
  FROM sys.objects
  WHERE object_id = OBJECT_ID(N'GetProjectWiseBillingDetails')
  AND type IN (N'P', N'PC'))
  DROP PROCEDURE [dbo].[GetProjectWiseBillingDetails]
/****** Object:  StoredProcedure [dbo].[GetProjectWiseBillingDetails]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Change History:
-------------------------
-- Name                    Date             Comments
--------------------------------------------------------
-- Joshwa George           08/06/2018      Modified to get actual spent from time tracker

--get billing details

CREATE PROCEDURE [dbo].[GetProjectWiseBillingDetails]
-- Add the parameters for the stored procedure here

@month nvarchar(50),
@year int,
@projectId int

AS
BEGIN
  -- SET NOCOUNT ON added to prevent extra result sets from
  -- interfering with SELECT statements.
  SET NOCOUNT ON;


  SELECT DISTINCT
    r.[Role],
    SUM(bd.PlannedHours) AS p,
    SUM(bd.ActualHours) AS a,
    SUM(CASE
      WHEN ISNULL(bd.UBT, 'No') = 'Yes' THEN ISNULL(bd.ActualHours, 0)
      ELSE 0
    END) UBT,
    SUM(bd2.ActualHours) AS ActualSpentHours
  FROM BillingDetails bd
  LEFT JOIN (SELECT
    resourceid,
    roleid,
    ProjectId,
    ISNULL(SUM(tt.ActualHours), 0) AS ActualHours
  FROM Timetracker tt
  WHERE tt.ProjectId = @projectId
  AND DATEPART(MONTH, fromDate) = @month
  AND DATEPART(YEAR, fromDate) = @year
  GROUP BY ProjectId,
           RoleId,
           ResourceId) bd2
    ON bd2.ProjectId = bd.ProjectId
    AND bd2.RoleId = bd.RoleID
    AND bd2.ResourceId = bd.ResourceID
  INNER JOIN [Role] r
    ON r.RoleId = bd.RoleId
  WHERE DATEPART(MONTH, bd.fromDate) = @month
  AND bd.ProjectID = @projectId
  AND DATEPART(YEAR, bd.fromDate) = @year
  GROUP BY r.[Role]

END

GO
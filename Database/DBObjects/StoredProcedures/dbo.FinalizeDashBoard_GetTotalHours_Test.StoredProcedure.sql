IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'FinalizeDashBoard_GetTotalHours_Test') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FinalizeDashBoard_GetTotalHours_Test]
/****** Object:  StoredProcedure [dbo].[FinalizeDashBoard_GetTotalHours_Test]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FinalizeDashBoard_GetTotalHours_Test]
@dayfrom VARCHAR(15),
@dayto VARCHAR(15)
AS
BEGIN
	SELECT 
		COUNT(DISTINCT R.ResourceId) NoOfResources
		,CASE WHEN COUNT(DISTINCT R.ResourceId)  > 0 THEN COUNT(DISTINCT R.ResourceId)*40 ELSE 0 END AS TotalHours
		,CASE WHEN Sum(TimeTracker.EstimatedHours) > 0 THEN Sum(TimeTracker.EstimatedHours) ELSE 0 END AS EstimatedHours
		,CASE WHEN Sum(TimeTracker.ActualHours) > 0 THEN Sum(TimeTracker.ActualHours) ELSE 0 END AS ActualHours 
	FROM
	TimeTracker
		JOIN [Resource] R  On TimeTracker.ResourceId = R.ResourceId
		INNER JOIN [Role] On TimeTracker.RoleId = Role.RoleId 
		INNER JOIN Project On Project.ProjectId = TimeTracker.ProjectId 
	WHERE ( --Year(TimeTracker.FromDate)=YEAR(@dayfrom) AND Year(TimeTracker.ToDate)=YEAR(@dayto) AND 
			TimeTracker.FromDate>=@dayfrom AND TimeTracker.ToDate<=@dayto ) 
END
GO

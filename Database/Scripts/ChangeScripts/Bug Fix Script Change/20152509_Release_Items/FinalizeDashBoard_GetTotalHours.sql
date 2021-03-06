IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FinalizeDashBoard_GetTotalHours]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FinalizeDashBoard_GetTotalHours]
GO   

--FinalizeDashBoard_GetTotalHours '2015-08-10 00:00:00.000','2015-08-16 00:00:00.000'

CREATE PROCEDURE [dbo].[FinalizeDashBoard_GetTotalHours]
@dayfrom VARCHAR(15),
@dayto VARCHAR(15)
AS
BEGIN
	DECLARE @TotalHours INT
	
	SELECT @TotalHours = (SELECT SUM(ISNULL(WeeklyHour,40)) 
									FROM  [Resource]
									WHERE ResourceID IN
									(SELECT DISTINCT ResourceID FROM TimeTracker
									WHERE TimeTracker.FromDate>=@dayfrom AND TimeTracker.ToDate<=@dayto))
	
	SELECT 
		COUNT(DISTINCT R.ResourceId) NoOfResources
		,CASE WHEN COUNT(DISTINCT R.ResourceId)  > 0 THEN @TotalHours ELSE 0 END AS TotalHours
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


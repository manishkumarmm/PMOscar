-- Create procedure to get total hours of week of Resource by resourceId.
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================
-- Author     :	Riya
-- Create Date: 30-08-2013
-- Description:	Procedure to get total hours of week of Resource by resourceId.
-- ===========================================================================
--EXEC GetTotalHoursOfResource 41,11139,'2013-08-26','2013-08-31',1
CREATE PROCEDURE [dbo].[GetTotalHoursOfResource]
	@ResourceId INT, 
	@TimeTrackerId INT,
	@FromDate DATETIME,          
	@ToDate DATETIME,
	@Mode INT
AS
	BEGIN
		IF(@Mode = 1)
		BEGIN
			SELECT rs.ResourceName,tt.FromDate,tt.ToDate
			,ISNULL(sum(tt.EstimatedHours),0) AS EstimatedHours,ISNULL(sum(tt.ActualHours),0) AS ActualHours
			,tt.ResourceId
			FROM [dbo].[TimeTracker] tt
			INNER JOIN Project pr ON pr.ProjectId=tt.ProjectId
			INNER JOIN [Resource] rs ON rs.ResourceId=tt.ResourceId
			WHERE (tt.FromDate)>=@FromDate and (tt.ToDate)<=@ToDate AND tt.ResourceId = @ResourceID
			GROUP BY tt.ResourceId,rs.ResourceName,tt.FromDate,tt.ToDate
		END
		ELSE IF(@Mode = 2)
		BEGIN
			SELECT rs.ResourceName,tt.FromDate,tt.ToDate
			,ISNULL(sum(tt.EstimatedHours),0) AS EstimatedHours,ISNULL(sum(tt.ActualHours),0) AS ActualHours
			,tt.ResourceId
			FROM [dbo].[TimeTracker] tt
			INNER JOIN Project pr ON pr.ProjectId=tt.ProjectId
			INNER JOIN [Resource] rs ON rs.ResourceId=tt.ResourceId
			WHERE (tt.FromDate)>=@FromDate and (tt.ToDate)<=@ToDate AND tt.ResourceId = @ResourceID AND tt.TimeTrackerId <> @TimeTrackerId
			GROUP BY tt.ResourceId,rs.ResourceName,tt.FromDate,tt.ToDate
		END
	END
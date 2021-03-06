IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetTotalHoursOfResource') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetTotalHoursOfResource]
/****** Object:  StoredProcedure [dbo].[GetTotalHoursOfResource]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO

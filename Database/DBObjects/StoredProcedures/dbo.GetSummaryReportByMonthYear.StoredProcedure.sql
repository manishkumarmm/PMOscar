IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetSummaryReportByMonthYear') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSummaryReportByMonthYear]
/****** Object:  StoredProcedure [dbo].[GetSummaryReportByMonthYear]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSummaryReportByMonthYear]
@Year INT,          
@Month INT
AS
BEGIN
	DECLARE @date1 DATETIME 
	SET @date1 = CONVERT(VARCHAR(10), DATEADD(MONTH,@Month-1,DATEADD(YEAR,@Year-1900,0)), 101);  
	
	SELECT [Date]
	,SUM(AvailableHours) AS AvailableHours
	,SUM(ActualHours) AS ActualHours
	,CASE WHEN VAS IS NOT NULL THEN 0 ELSE 
	 ISNULL(BilledHours,0) END AS BilledHours,[Admin],[Open],[Proposal],VAS
	,ProjectId,ProjectName
	--,CASE WHEN PhaseId IS NULL THEN 0 ELSE PhaseId END AS PhaseId
	,ResourceId
	,RoleId,TeamId
	FROM
		(SELECT * FROM	
			(SELECT * FROM
				(SELECT * FROM
					(SELECT * FROM
						(SELECT 
						/*Get project details*/
						@date1 AS [Date]
						,pr.ProjectId ProjectId,pr.ProjectName ProjectName
						,tt.EstimatedHours AvailableHours
						,tt.ActualHours ActualHours
						,ph.PhaseId PhaseId
						,rs.ResourceId ResourceId
						,rl.RoleId RoleId
						,tm.TeamId TeamId,tt.TimeTrackerId
						FROM TimeTracker tt
						JOIN [Resource] rs ON rs.ResourceId=tt.ResourceId
						JOIN [Role] rl ON rl.RoleId=tt.RoleId
						LEFT JOIN Phase ph ON ph.PhaseId=tt.PhaseId
						JOIN Project pr ON pr.ProjectId=tt.ProjectId
						JOIN Team tm on tm.TeamId=tt.TeamID
						WHERE (YEAR(tt.FromDate) = @Year AND MONTH(tt.FromDate) = @Month AND @Year >= YEAR(rs.BillingStartDate) AND  @Month >= MONTH(rs.BillingStartDate))
						)tbl
						
						LEFT JOIN 
						/*Get actual billable of each project in given month*/
						(SELECT DISTINCT bd.ProjectId AS BillProjectId,bd.ActualHours AS BilledHours,rl.RoleId AS BillRoleId,rs.ResourceId BillResourceId
						FROM BillingDetails bd
						JOIN [Resource] rs ON rs.ResourceId=bd.ResourceId
						JOIN Team tm ON tm.TeamID = rs.TeamID
						JOIN [Role] rl ON rl.RoleId=bd.RoleId
						WHERE (YEAR(bd.FromDate) = @Year and MONTH(bd.FromDate) = @Month AND @Year >= YEAR(rs.BillingStartDate) AND  @Month >= MONTH(rs.BillingStartDate))
						)bill
						ON bill.BillProjectId = tbl.ProjectId AND bill.BillRoleId = tbl.RoleId AND tbl.ResourceId = bill.BillResourceId
					LEFT JOIN 
					/*Get hours spent in Project 'Admin' in a month*/
					(SELECT 
					tt.ActualHours AS [Admin],tt.TimeTrackerId AS AdTimeTrackerId
					FROM TimeTracker tt
					JOIN [Resource] rs ON rs.ResourceId=tt.ResourceId
					JOIN [Role] rl ON rl.RoleId=tt.RoleId
					LEFT JOIN Phase ph ON ph.PhaseId=tt.PhaseId
					JOIN Project pr ON pr.ProjectId=tt.ProjectId AND ( pr.ProjectId = 2) /* ProjectId = 2 ProjectId  of Admin */
					JOIN Team tm on tm.TeamId=tt.TeamID
					WHERE (YEAR(tt.FromDate) = @Year and MONTH(tt.FromDate) = @Month AND @Year >= YEAR(rs.BillingStartDate) AND  @Month >= MONTH(rs.BillingStartDate))
					)a
					ON a.AdTimeTrackerId = tbl.TimeTrackerId
					) tblAdmin
				LEFT JOIN 
				/*Get hours spent in Project 'Open' in a month*/
				(SELECT 
				tt.ActualHours AS [Open],tt.TimeTrackerId AS OpTimeTrackerId
				FROM TimeTracker tt
				JOIN [Resource] rs ON rs.ResourceId=tt.ResourceId
				JOIN [Role] rl ON rl.RoleId=tt.RoleId
				LEFT JOIN Phase ph ON ph.PhaseId=tt.PhaseId
				JOIN Project pr ON pr.ProjectId=tt.ProjectId AND ( pr.ProjectId = 16) /* ProjectId = 16 ProjectId  of Open */
				JOIN Team tm on tm.TeamId=tt.TeamID
				WHERE (YEAR(tt.FromDate) = @Year and MONTH(tt.FromDate) = @Month AND @Year >= YEAR(rs.BillingStartDate) AND  @Month >= MONTH(rs.BillingStartDate))
				)o	
				ON o.OpTimeTrackerId = tblAdmin.TimeTrackerId
				)tblOpen
			LEFT JOIN
			/*Get hours spent in Project 'Proposal' in a month*/
			(SELECT 
			tt.ActualHours AS [Proposal],tt.TimeTrackerId AS ProTimeTrackerId
			FROM TimeTracker tt
			JOIN [Resource] rs ON rs.ResourceId=tt.ResourceId
			JOIN [Role] rl ON rl.RoleId=tt.RoleId
			LEFT JOIN Phase ph ON ph.PhaseId=tt.PhaseId
			JOIN Project pr ON pr.ProjectId=tt.ProjectId AND ( pr.ProjectId = 57) /* ProjectId = 57 ProjectId  of Proposal */
			JOIN Team tm on tm.TeamId=tt.TeamID
			WHERE (YEAR(tt.FromDate) = @Year and MONTH(tt.FromDate) = @Month AND @Year >= YEAR(rs.BillingStartDate) AND  @Month >= MONTH(rs.BillingStartDate))
			)p
			ON p.ProTimeTrackerId = tblOpen.TimeTrackerId
			)tblProposal
		LEFT JOIN
		/*Get total hours spent in a project's phase - VAS in a month*/
		(SELECT 
		tt.ActualHours VAS,tt.TimeTrackerId AS VASTimeTrackerId
		FROM TimeTracker tt
		JOIN [Resource] rs ON rs.ResourceId=tt.ResourceId
		JOIN [Role] rl ON rl.RoleId=tt.RoleId
		JOIN Phase ph ON ph.PhaseId=tt.PhaseId AND ph.PhaseId = 6 /* PhaseId = 6 PhaseId of VAS */
		JOIN Project pr ON pr.ProjectId=tt.ProjectId
		JOIN Team tm on tm.TeamId=tt.TeamID
		WHERE (YEAR(tt.FromDate) = @Year and MONTH(tt.FromDate) = @Month AND @Year >= YEAR(rs.BillingStartDate) AND  @Month >= MONTH(rs.BillingStartDate))
		)vas
		ON vas.VASTimeTrackerId = tblProposal.TimeTrackerId
		)
		JoinedDetails
		GROUP BY
		ResourceId
		,[Date]
		,BilledHours,[Admin],[Open],[Proposal],VAS
		,ProjectId,ProjectName --,ActualHours,PhaseId
		,RoleId,TeamId
END
GO

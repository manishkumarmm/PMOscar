/*=================================================================*/
/* PMOscar Phase-5 database changes script	                       */
/* Date: 16-04-2012					                               */
/*=================================================================*/

--Dropping column  UtilizedButNotBilledHours to table CompanyUtilizationReport
ALTER TABLE CompanyUtilizationReport
   DROP COLUMN UtilizedButNotBilledHours
   
-- Adding column ActualHours to table CompanyUtilizationReport
ALTER TABLE dbo.CompanyUtilizationReport
ADD ActualHours INT NULL

-- Updated Procedure to select ActualHours 

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--EXEC GetSummaryReportByMonthYear 2013,4
ALTER PROCEDURE [dbo].[GetSummaryReportByMonthYear]
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

/*********************************************************************************************/

-- Updated Procedure to added UtilizedButNotBilledHours 

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--EXEC InsertSummaryReportDetails '2013-03-01 00:00:00.000',83,6,3,1,1,168,NULL,1,NULL ,NULL ,NULL ,50,20
ALTER PROCEDURE [dbo].[InsertSummaryReportDetails]
	@Date DATETIME,
	@ProjectID INT,
	--@PhaseID INT,
	@ResourceID INT,
	@RoleID INT,
	@TeamID INT,
	@AvailableHours INT=NULL,
	@BilledHours INT=NULL,
	@Finalize BIT,
	@Admin INT=0,
	@Open INT=0,
	@VAS INT=0,
	@Proposal INT=0,
	@ActualHours INT=0
AS
BEGIN	
	INSERT 
	INTO CompanyUtilizationReport ([Date],ProjectID
	--,PhaseID
	,ResourceID,RoleID,TeamID,AvailableHours,BilledHours,Finalize,[Admin],[Open],VAS,Proposal,ActualHours)
	VALUES (@Date,@ProjectID
	--,@PhaseID
	,@ResourceID,@RoleID,@TeamID,@AvailableHours,@BilledHours,@Finalize,@Admin,@Open,@VAS,@Proposal,@ActualHours)
END

/*********************************************************************************************/

-- Updated Procedure to modify UtilizedButNotBilledHours 

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--EXEC GetCompanyUtilization_Summary
ALTER PROCEDURE [dbo].[GetCompanyUtilization_Summary]
AS
BEGIN
	
	SELECT [Date],Month_Year,AvailableHours
	,BilledHours
	,((ActualHours) - (BilledHours+[Admin]+[Open+VAS]+Proposal+Finalize)) AS UtilizedButNotBilledHours
	,[Admin],[Open+VAS],Proposal,Finalize 
	FROM
		(SELECT 
			[Date]
			,datename(month,dateadd(month,MONTH([Date]) - 1, 0))+ ' '+cast(YEAR([Date]) as char(4)) AS Month_Year
			,SUM(AvailableHours) AS AvailableHours
			,ISNULL(SUM(ActualHours), 0 ) AS ActualHours
			,ISNULL(SUM(BilledHours), 0 ) AS BilledHours
			,Finalize
			,ISNULL(SUM([Admin]), 0 ) AS [Admin]
			,SUM(ISNULL([Open], 0 )+ISNULL([VAS], 0 )) AS [Open+VAS]
			,ISNULL(SUM(Proposal), 0 ) AS Proposal
		FROM CompanyUtilizationReport
		GROUP BY [Date]
		,Finalize
		)tbl1
	--	LEFT JOIN
	--	(
	--		SELECT t.[Date] AS tbl2Date,SUM(t.BilledHours) AS BilledHours
	--		FROM
	--			(SELECT [Date],[BilledHours] 
	--			FROM CompanyUtilizationReport
	--			WHERE BilledHours IS NOT NULL
	--			GROUP BY ProjectId,[BilledHours],[Date])t
	--		GROUP BY t.[Date]
	--	)tbl2
	--ON tbl1.[Date] = tbl2.tbl2Date
END

/*********************************************************************************************/

-- Updated Procedure to modify UtilizedButNotBilledHours 

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--EXEC GetCompanyUtilization_Detailed 2013,4
ALTER PROCEDURE [dbo].[GetCompanyUtilization_Detailed]
@Year INT,          
@Month INT 

AS
BEGIN
	SELECT Team,[Role],RoleId,
	AvailableHours,BilledHours,
	(ActualHours - (BilledHours+[Admin]+[Open+VAS]+Proposal)) AS UtilizedButNotBilledHours
	,[Admin],[Open+VAS],Proposal
	FROM
	(
	SELECT t.Team AS Team ,t.[Role] AS [Role],t.RoleId AS RoleId
	,SUM(AvailableHours) AS AvailableHours,SUM(BilledHours) AS BilledHours
	,SUM(ActualHours) AS ActualHours
	,SUM([Admin]) AS [Admin],SUM([Open+VAS]) AS [Open+VAS],SUM(Proposal) AS Proposal
	FROM
	(
		SELECT 
			rs.ResourceName,rs.ResourceId ResourceId
			,tm.Team Team,rl.[Role] [Role],rl.RoleId RoleId
			,SUM(cu.AvailableHours) AvailableHours
			,CASE WHEN cu.BilledHours IS NULL THEN 0 ELSE cu.BilledHours END AS BilledHours
			,SUM(cu.ActualHours) ActualHours
			,ISNULL(SUM(cu.[Admin]), 0 ) AS [Admin]
			,SUM(ISNULL(cu.[Open], 0 )+ISNULL(cu.[VAS], 0 )) AS [Open+VAS]
			,ISNULL(SUM(cu.Proposal), 0 ) AS Proposal
		FROM CompanyUtilizationReport cu
		JOIN Team tm ON tm.TeamID = cu.TeamID
		JOIN [Role] rl ON rl.RoleId = cu.RoleId
		JOIN [Resource] rs ON rs.ResourceID = cu.ResourceID
		WHERE (YEAR([Date]) = @Year and MONTH([Date]) = @Month)
		GROUP BY cu.AvailableHours,tm.Team,rl.[Role],rs.ResourceName,rs.ResourceId,rl.RoleId,BilledHours
	)t
	GROUP BY t.Team,t.[Role],t.RoleId
	)tbl
END

/*********************************************************************************************/

-- Procedure added get CompanyUtilization_Summary By Month and Year

--EXEC GetCompanyUtilization_SummaryByMonthYear 2013,4
CREATE PROCEDURE [dbo].[GetCompanyUtilization_SummaryByMonthYear]
@Year INT,          
@Month INT
AS
BEGIN
	SELECT [Date],Month_Year,AvailableHours
	,BilledHours
	,((ActualHours) - (BilledHours+[Admin]+[Open+VAS]+Proposal+Finalize)) AS UtilizedButNotBilledHours
	,[Admin],[Open+VAS],Proposal,Finalize 
	FROM
		(SELECT 
			[Date]
			,datename(month,dateadd(month,MONTH([Date]) - 1, 0))+ ' '+cast(YEAR([Date]) as char(4)) AS Month_Year
			,SUM(AvailableHours) AS AvailableHours
			,ISNULL(SUM(ActualHours), 0 ) AS ActualHours
			,ISNULL(SUM(BilledHours), 0 ) AS BilledHours
			,Finalize
			,ISNULL(SUM([Admin]), 0 ) AS [Admin]
			,SUM(ISNULL([Open], 0 )+ISNULL([VAS], 0 )) AS [Open+VAS]
			,ISNULL(SUM(Proposal), 0 ) AS Proposal
		FROM CompanyUtilizationReport
		GROUP BY [Date]
		,Finalize
		)tbl1
	WHERE YEAR([Date]) = @year AND MONTH([Date]) = @Month 
END


/*********************************************************************************************/

-- Procedure to check if an allocation exists in resource planning for the given project,month and year with roleId.

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--EXEC Resource_ExistORNotInTimeTracker 192,2013,4,10,8
CREATE PROCEDURE [dbo].[Resource_ExistORNotInTimeTracker]
@ProjectId INT,
@Year INT,          
@Month INT,
@ResourceId INT,
@RoleId INT

AS
	DECLARE @returnValue INT
	SET @returnValue  = 0
	
BEGIN	
	IF EXISTS (
				SELECT 1 
				FROM TimeTracker tt
				INNER JOIN [Resource] rs ON tt.ResourceId = rs.ResourceId
				WHERE tt.ProjectId = @ProjectId 
				AND YEAR(tt.FromDate) = @Year 
				AND MONTH(tt.FromDate) = @Month 
				AND YEAR(tt.ToDate) = @Year 
				AND MONTH(tt.ToDate) = @Month 
				AND tt.ResourceId = @ResourceId 
				AND tt.RoleId = @RoleId 
				AND @Year >= YEAR(rs.BillingStartDate) 
				AND @Month >= MONTH(rs.BillingStartDate)
			)
	BEGIN
	SET @returnValue = 1
	END
	SELECT @returnValue	AS ResourceExists		
END






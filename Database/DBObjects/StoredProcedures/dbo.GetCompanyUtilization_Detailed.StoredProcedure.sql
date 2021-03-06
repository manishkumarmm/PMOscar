IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetCompanyUtilization_Detailed') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCompanyUtilization_Detailed]
/****** Object:  StoredProcedure [dbo].[GetCompanyUtilization_Detailed]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCompanyUtilization_Detailed]
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
GO

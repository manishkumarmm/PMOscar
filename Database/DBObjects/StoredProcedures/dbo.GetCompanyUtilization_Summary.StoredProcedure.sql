IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetCompanyUtilization_Summary') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCompanyUtilization_Summary]
/****** Object:  StoredProcedure [dbo].[GetCompanyUtilization_Summary]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--EXEC GetCompanyUtilization_Summary
CREATE PROCEDURE [dbo].[GetCompanyUtilization_Summary]
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
GO

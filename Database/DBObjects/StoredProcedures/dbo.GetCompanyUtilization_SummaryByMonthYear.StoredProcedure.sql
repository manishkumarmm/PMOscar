IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetCompanyUtilization_SummaryByMonthYear') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCompanyUtilization_SummaryByMonthYear]
/****** Object:  StoredProcedure [dbo].[GetCompanyUtilization_SummaryByMonthYear]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO

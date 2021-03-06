IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetDashboardByProgramIDAndProjectID') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDashboardByProgramIDAndProjectID]
/****** Object:  StoredProcedure [dbo].[GetDashboardByProgramIDAndProjectID]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Description:	Get period start date and date end
-- Modified By:Haritha E.S
--Modified On:2018-05-07
-- =============================================
-- Exec GetDashboardByProgramIDAndProjectID 1,0 
CREATE PROCEDURE [dbo].[GetDashboardByProgramIDAndProjectID]
	@ProgramID int=0,
	@ProjectID int=0
AS
BEGIN
		SELECT DISTINCT D.DashboardID, Name,convert(datetime,LEFT([Name], CHARINDEX('-', [Name]) - 1 ),103) as toSort, CONVERT(VARCHAR(10),FromDate,105)as FromDate,
	CONVERT(VARCHAR(10),ToDate,105)as ToDate,PeriodType, Status  
	FROM Dashboard D
	INNER JOIN dbo.ProjectDashboard PD ON PD.DashboardID = D.DashboardID 
	INNER JOIN dbo.ProjectDashboardEstimation PDE ON PD.ProjectID=PDE.ProjectID AND PD.DashboardID=PDE.DashboardID
	INNER JOIN Project P ON P.ProjectID = PD.ProjectID
	WHERE (P.ProgID=@ProgramID OR @ProgramID=0) AND (P.ProjectID=@ProjectID OR @ProjectID=0)
	ORDER BY D.DashboardID DESC 
END
GO

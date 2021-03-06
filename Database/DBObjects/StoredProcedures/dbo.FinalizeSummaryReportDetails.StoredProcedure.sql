IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'FinalizeSummaryReportDetails') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FinalizeSummaryReportDetails]
/****** Object:  StoredProcedure [dbo].[FinalizeSummaryReportDetails]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FinalizeSummaryReportDetails] 
	@Year INT,
	@Month INT
AS
BEGIN
	UPDATE CompanyUtilizationReport 
	SET Finalize=1
	WHERE (YEAR(Date) = @Year and MONTH(Date) = @Month)
END
GO

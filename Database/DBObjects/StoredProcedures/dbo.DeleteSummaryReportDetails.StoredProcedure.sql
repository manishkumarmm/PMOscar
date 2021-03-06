IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DeleteSummaryReportDetails') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteSummaryReportDetails]
/****** Object:  StoredProcedure [dbo].[DeleteSummaryReportDetails]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteSummaryReportDetails]
	@Year INT,          
	@Month INT
AS
BEGIN	
	DELETE 
	FROM CompanyUtilizationReport
	WHERE (YEAR([Date]) = @Year and MONTH([Date]) = @Month)
END
GO

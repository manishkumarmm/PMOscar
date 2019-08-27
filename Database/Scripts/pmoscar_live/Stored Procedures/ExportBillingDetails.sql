
/****** Object:  StoredProcedure [dbo].[ExportBillingDetails]    Script Date: 09/19/2012 10:15:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExportBillingDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ExportBillingDetails]
GO


/****** Object:  StoredProcedure [dbo].[ExportBillingDetails]    Script Date: 09/19/2012 10:15:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Dona
-- Create date: 8-5-2012
-- Description:	procedure to get team details for a resource
-- =============================================
--sp to get the billing details for selected month and year.... used for exporting data

CREATE PROCEDURE [dbo].[ExportBillingDetails]
	-- Add the parameters for the stored procedure here
	
	@month int,
	@year int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
select p.ProjectName,SUBSTRING(CONVERT(VARCHAR(11),bd.fromDate,106),3,9) as Month,rs.ResourceName,r.Role,
bd.PlannedHours as PlannedBillable,bd.ActualHours as ActualBillable
  FROM BillingDetails bd
  INNER JOIN [Resource] rs ON rs.ResourceId =bd.ResourceId
  INNER JOIN [Role] r ON r.RoleId = bd.RoleId
  INNER JOIN [Project] p ON p.ProjectId = bd.ProjectID
  where DATEPART(month,fromDate)=@month AND DATEPART(year,fromDate)=@year order by p.ProjectName

END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetYearlyProjectBillingDetails') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetYearlyProjectBillingDetails]
/****** Object:  StoredProcedure [dbo].[GetYearlyProjectBillingDetails]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetYearlyProjectBillingDetails]
	@ProjectID int,
	@Year int	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 
	SELECT DISTINCT R.[Role],sum(bd.PlannedHours) AS PlannedBillable,sum(bd.ActualHours) AS ActualBillable,
		SUM(CASE WHEN ISNULL(BD.UBT,'No') ='Yes' THEN ISNULL(bd.ActualSpentHours, 0) ELSE 0 END) UBT,
		SUM(CASE WHEN ISNULL(BD.UBT,'No') !='Yes' THEN ISNULL(bd.ActualSpentHours, 0) ELSE 0 END) ActualSpent
	FROM BillingDetails BD 
	INNER JOIN [Role] R ON R.RoleId = BD.RoleId 
		WHERE BD.ProjectID = @ProjectID  AND DATEPART(year, BD.fromDate) = @Year
	GROUP BY R.[Role]

END

GO

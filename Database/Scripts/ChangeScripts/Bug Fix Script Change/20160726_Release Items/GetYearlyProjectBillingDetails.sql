-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--get billing details
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetYearlyProjectBillingDetails')
     DROP PROCEDURE GetYearlyProjectBillingDetails
go
CREATE PROCEDURE GetYearlyProjectBillingDetails
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


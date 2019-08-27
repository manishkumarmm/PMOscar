IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProjectWiseBillingDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetProjectWiseBillingDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--get billing details

CREATE PROCEDURE [dbo].[GetProjectWiseBillingDetails]
	-- Add the parameters for the stored procedure here
	
	@Month nvarchar(50),
	@ProjectID int,
	@Year int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 
   SELECT DISTINCT r.[Role],sum(bd.PlannedHours) AS p,sum(bd.ActualHours) AS a,
    SUM(CASE WHEN ISNULL(bd.UBT,'No') ='Yes' THEN ISNULL(bd.ActualSpentHours, 0) ELSE 0 END) ActualSpentHours,
	SUM(CASE WHEN ISNULL(bd.UBT,'No') !='Yes' THEN ISNULL(bd.ActualSpentHours, 0) ELSE 0 END) UBT
   FROM BillingDetails bd 
   INNER JOIN [Role] r ON r.RoleId = bd.RoleId 
   WHERE DATEPART(month,bd.fromDate)=@Month and bd.ProjectID =@ProjectID  and DATEPART(year,bd.fromDate) =@Year
   GROUP BY r.[Role]

END


GO



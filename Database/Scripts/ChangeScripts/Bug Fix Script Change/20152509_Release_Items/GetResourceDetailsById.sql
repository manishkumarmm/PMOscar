IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetResourceDetailsById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetResourceDetailsById]
GO   
                   
CREATE PROCEDURE [dbo].[GetResourceDetailsById] 
		@ResourceID int
AS
BEGIN
		SELECT ResourceId,ResourceName,R.RoleId,T.TeamID,IsActive,BillingStartDate,COALESCE(emp_Code, '') as Emp_code,CC.CostCentreID, WeeklyHour
		FROM Resource RES 
		INNER JOIN Role R ON R.RoleID = RES.RoleID   
		LEFT JOIN  Team T ON T.TeamID = RES.TeamID OR T.TeamID = NULL left join CostCentre CC on CC.CostCentreID=RES.CostCentreID OR CC.CostCentreID = NULL
		WHERE ResourceId=@ResourceID 
END

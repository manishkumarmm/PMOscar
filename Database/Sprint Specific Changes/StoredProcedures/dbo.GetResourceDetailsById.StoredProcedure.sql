IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetResourceDetailsById') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetResourceDetailsById]
/****** Object:  StoredProcedure [dbo].[GetResourceDetailsById]    Script Date: 4/19/2018 1:02:16 PM ******/

-- ==============================================================================
-- Description: Get Resource details By Id
-- ==============================================================================
-- Modified By:  Vibin M B
-- Modified date: 2018-08-06
-- ==============================================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
                   
CREATE PROCEDURE [dbo].[GetResourceDetailsById] 
		@ResourceID int
AS
BEGIN
		SELECT ResourceId,ResourceName,R.RoleId,T.TeamID,IsActive,BillingStartDate,COALESCE(emp_Code, '') as Emp_code,CC.CostCentreID, WeeklyHour,
		Convert(varchar(10),CONVERT(date,JoinDate,106),103),Convert(varchar(10),CONVERT(date,ExitDate,106),103),
		Convert(varchar(10),CONVERT(date,AvailableHoursStartDate,106),103)
		FROM Resource RES 
		INNER JOIN Role R ON R.RoleID = RES.RoleID   
		LEFT JOIN  Team T ON T.TeamID = RES.TeamID OR T.TeamID = NULL left join CostCentre CC on CC.CostCentreID=RES.CostCentreID OR CC.CostCentreID = NULL
		WHERE ResourceId=@ResourceID 
END
GO

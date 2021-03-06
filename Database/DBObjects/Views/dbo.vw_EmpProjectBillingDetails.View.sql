IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'vw_EmpProjectBillingDetails') AND type in (N'P', N'PC'))
DROP VIEW [dbo].[vw_EmpProjectBillingDetails]
/****** Object:  View [dbo].[vw_EmpProjectBillingDetails]    Script Date: 4/19/2018 1:01:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create view [dbo].[vw_EmpProjectBillingDetails] 
as
select 
	rc.ResourceName EmployeeName,r.Role, br.Role BillingRole
	, tm.Team, p.ProjectName Project, bd.PlannedHours PlannedBillable, bd.ActualHours ActualBilledHours
	,t.ActualHoursSpent, convert(varchar,year(bd.FromDate) )+'-'+convert(varchar,month(bd.FromDate)) as YearMonth
from BillingDetails bd
join Project p on p.ProjectID=bd.ProjectID
join Resource rc on rc.ResourceID=bd.ResourceID
join Role br on br.RoleID=bd.RoleID
join Role r on r.RoleID=rc.RoleID
join Team tm on tm.TeamID=rc.TeamID
Join
	(
		select 
			ResourceID,RoleID,ProjectID
			,convert(varchar,year([FromDate]) )+convert(varchar,month(FromDate)) YearMonth,sum( ActualHours) ActualHoursSpent
		from TimeTracker
		group by ResourceID,RoleID,ProjectID,convert(varchar,year([FromDate]) )+convert(varchar,month(FromDate))
	) as t on t.ResourceID=bd.ResourceID and t.RoleID=bd.RoleID and t.ProjectID=bd.ProjectID  
				and t.YearMonth=convert(varchar,year(bd.FromDate) )+convert(varchar,month(bd.FromDate))
GO

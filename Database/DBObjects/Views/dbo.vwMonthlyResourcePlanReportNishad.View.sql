IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'vwMonthlyResourcePlanReportNishad') AND type in (N'P', N'PC'))
DROP VIEW [dbo].[vwMonthlyResourcePlanReportNishad]
/****** Object:  View [dbo].[vwMonthlyResourcePlanReportNishad]    Script Date: 4/19/2018 1:01:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create View [dbo].[vwMonthlyResourcePlanReportNishad] as 
select 
	p.ProjectId, p.ProjectName
	, isnull(u.FirstName+' ','')+isnull(u.MiddleName+'','')+isnull(u.LastName,'') as 'ProjectManager'
	, cc.CostCentre, case Utilization when 'I' then 'Internal' when 'S' then 'Semi-Commercial' 
		when 'G' then 'GIS' 
		when 'P' then 'Product' when 'C' then 'Commercial' end as Category
	, pr.ProgName
	, isnull(uo.FirstName+' ','')+isnull(uo.MiddleName+'','')+isnull(uo.LastName,'') as 'ProjectOwner'
	, case when ProjectType='F' then 'Fixed Cost' else 'T&M' end as ProjectType
	, c.ClientName,pe.BillableHours,pe.VASBudgetHours,pe.Non_VASBudgetHours
	, pe.VASRevisedBudgetHours, pe.Non_VASRevisedBudgetHours
	, isnull(at.VASActualHours,0) as VASActualHours
	, isnull(at.Non_VASActualHours,0) as Non_ActualHours
	, isnull(at.Total_ActualHours,0) as Total_ActualHours
from dbo.Project p 
left Join dbo.[User] u on u.UserId=p.ProjectManager 
left Join dbo.[User] uo on uo.UserId=p.ProjectOwner
left Join dbo.CostCentre cc on cc.CostCentreID=p.CostCentreID
left Join dbo.Program pr on pr.ProgId=p.ProgId
left join dbo.Client c on c.ClientId=p.ClientId
join (select ProjectID, sum(BillableHours) as BillableHours
		, Sum(case when PhaseID=6 then BudgetHours else 0 end) as VASBudgetHours
		, Sum(case when PhaseID!=6 then BudgetHours else 0 end) as Non_VASBudgetHours
		, Sum(case when PhaseID=6 then RevisedBudgetHours else 0 end) as VASRevisedBudgetHours
		, Sum(case when PhaseID!=6 then RevisedBudgetHours else 0 end) as Non_VASRevisedBudgetHours
		from dbo.ProjectEstimation group by ProjectID) as pe on pe.ProjectID=p.ProjectID
left join
(
	select tt.ProjectId
	,sum(case when tt.PhaseID=6 then ActualHours else 0 end) as VASActualHours
	,sum(case when tt.PhaseID!=6 then ActualHours else 0 end) as Non_VASActualHours
	,isnull(sum(ActualHours),0) AS Total_ActualHours
	from dbo.TimeTracker tt
	group by tt.ProjectId
) as at on at.ProjectId=pe.ProjectID
GO

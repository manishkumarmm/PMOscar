IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'vwMothlyResourcePlanRpt') AND type in (N'P', N'PC'))
DROP VIEW [dbo].[vwMothlyResourcePlanRpt]
/****** Object:  View [dbo].[vwMothlyResourcePlanRpt]    Script Date: 4/19/2018 1:01:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[vwMothlyResourcePlanRpt] as

select r.ResourceName,p.ProjectName, isnull(ph.Phase,'') Phase, rl.Role, Month(tt.FromDate) [Month], Year(tt.FromDate) [Year]
, isnull(Sum(EstimatedHours),0) EstimatedHours, isnull(Sum(ActualHours),0) ActualHours
from dbo.TimeTracker tt
join dbo.Project p on p.ProjectID=tt.ProjectID
left Join dbo.Phase ph on ph.PhaseID=tt.PhaseID
Join dbo.Resource r on r.ResourceID=tt.ResourceID
Join [Role] rl on rl.RoleID=tt.RoleID
where Year(tt.FromDate)=Year(tt.ToDate) and Month(tt.FromDate)=Month(tt.ToDate)
group by r.ResourceName,p.ProjectName, ph.Phase, rl.Role, Month(tt.FromDate), Year(tt.FromDate)

GO

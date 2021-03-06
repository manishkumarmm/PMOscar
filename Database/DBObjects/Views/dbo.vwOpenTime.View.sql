IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'vwOpenTime') AND type in (N'P', N'PC'))
DROP VIEW [dbo].[vwOpenTime]
/****** Object:  View [dbo].[vwOpenTime]    Script Date: 4/19/2018 1:01:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE view [dbo].[vwOpenTime] as

select t.Team,r.[ResourceName],'Open' as Phase,tt.FromDate,tt.ToDate,tt.EstimatedHours,tt.ActualHours
from [dbo].[TimeTracker] tt
join [dbo].[Resource] r on r.ResourceID=tt.ResourceID
join [dbo].[Team] t on t.TeamID=r.TeamID
where ProjectId=16 and FromDate>='2014-03-10'
UNION ALL
select t.Team,r.[ResourceName],'VAS' as Phase,tt.FromDate,tt.ToDate,tt.EstimatedHours,tt.ActualHours
from [dbo].[TimeTracker] tt
join [dbo].[Resource] r on r.ResourceID=tt.ResourceID
join [dbo].[Team] t on t.TeamID=r.TeamID
where tt.[PhaseID]=6 and FromDate>='2014-03-10'
UNION ALL
select t.Team,r.[ResourceName],'Internal Projects' as Phase,tt.FromDate,tt.ToDate,tt.EstimatedHours,tt.ActualHours
from [dbo].[TimeTracker] tt
join [dbo].[Resource] r on r.ResourceID=tt.ResourceID
join [dbo].[Team] t on t.TeamID=r.TeamID
join Project p on p.ProjectID=tt.ProjectID
where p.Utilization='I' and p.ProjectID not in (16,57,2) and tt.PhaseID!=6 and FromDate>='2014-03-10'




GO

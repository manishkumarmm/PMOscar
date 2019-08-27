
/****** Object:  View [dbo].[vw_ResourceDataForNishad_PowerBI]    Script Date: 12/12/2018 2:30:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER view [dbo].[vw_ResourceDataForNishad_PowerBI]
as 

select 
R.ResourceName 
,p.ProjectName
, rl.Role
, case P.Utilization when 'I' then 'Internal' when 'S' then 'Semi-Commercial' 
		when 'G' then 'GIS' 
		when 'P' then 'Product' when 'C' then 'Commercial' end as Category

,case 
		when tt.ProjectId =16 
		then 'Open' 
		else 			
			case 
				when tt.PhaseID =6 then 'VAS' 
				else 
					case P.Utilization 
							when 'I' then 'Internal' 
							when 'P' then 'Product'
					else ' Client Projects'
					end
			end
	end as Category2



,convert(varchar,Fromdate,101) +' - '+ convert(varchar,ToDate,101)  [Week]
,sum(tt.EstimatedHours)  EstimatedHours
,sum(tt.ActualHours)  ActualHours
,month(fromdate) [Month]
,Year(Fromdate) [Year]
,Fromdate
,Todate
,cc.CostCentre ProjectCostCenter
,cc1.CostCentre ResourceCostCenter
,isnull(u.FirstName+' ','')+isnull(u.MiddleName+'','')+isnull(u.LastName,'') as 'ProjectManager'
from Resource R 
join TimeTracker tt on tt.ResourceId =R.ResourceId 
join dbo.Project p on p.ProjectId=tt.projectID
left join dbo.Role Rl on Rl.RoleId=tt.RoleId
left Join dbo.CostCentre cc on cc.CostCentreID=p.CostCentreID
left Join dbo.CostCentre cc1 on cc1.CostCentreID=r.CostCentreID
left Join dbo.[User] u on u.UserId=p.ProjectManager 
 where tt.FromDate >='2018-11-01'  and (isnull(tt.actualhours,0)>0 or isnull(tt.estimatedhours,0)>0)
 group by 
R.ResourceName 
,p.ProjectName
, rl.Role
, case P.Utilization when 'I' then 'Internal' when 'S' then 'Semi-Commercial' 
		when 'G' then 'GIS' 
		when 'P' then 'Product' when 'C' then 'Commercial' end 
,convert(varchar,Fromdate,101) +' - '+ convert(varchar,ToDate,101)  
,month(fromdate) 
,Year(Fromdate) 
,cc.CostCentre 
,cc1.CostCentre 
,isnull(u.FirstName+' ','')+isnull(u.MiddleName+'','')+isnull(u.LastName,'')
,Fromdate
,Todate
,case 
		when tt.ProjectId =16 
		then 'Open' 
		else 			
			case 
				when tt.PhaseID =6 then 'VAS' 
				else 
					case P.Utilization 
							when 'I' then 'Internal' 
							when 'P' then 'Product'
					else ' Client Projects'
					end
			end
	end 
GO



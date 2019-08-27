


alter view [dbo].[vw_GetPMODataforBugMatrixForPowerBI]
as


select 
 Year([FromDate]) [Year], Month([FromDate]) [Month],p.[ProjectId],r.[Role]
 ,sum(case when r.[Role] in ('Jr. DB Developer','Jr. Developer','Jr. Quality Analyst') then tt.[ActualHours]*0.75
  when r.[Role] in ('Sr. DB Developer','Sr. Developer','Sr. Quality Analyst') then  tt.[ActualHours]*1.25
   when r.[Role] in ('Developer','DB Developer','Quality Analyst') then tt.[ActualHours]
  end) HrsSpentNormalized
  ,sum(tt.[ActualHours]) HrsSpent
  ,P.BugzillaProjectID
  ,t.Team
from [dbo].[TimeTracker] tt
join [dbo].[Project] p on p.[ProjectId]=tt.[ProjectId]
join [dbo].[Role] r on r.[RoleId]=tt.[RoleId]
join team t on t.TeamID =tt.TeamID 
where Year([FromDate])>=2018 /*Change the year as needed*/ --and Month([FromDate])=4 /*Change the month as needed*/ 
and p.[ProjectName]Not in ('Admin','Naico Client Ad-hoc Activities','Open')
and r.[Role] in ('Jr. DB Developer','Jr. Developer','Jr. Quality Analyst','Sr. DB Developer','Sr. Developer','Sr. Quality Analyst'
,'Developer','DB Developer','Quality Analyst')
group by p.[ProjectName],r.[Role],Year([FromDate]), Month([FromDate]),p.[ProjectId],P.BugzillaProjectID,t.Team
having sum(tt.[ActualHours])>0
--order by Year([FromDate]), Month([FromDate]),p.[ProjectName]
GO



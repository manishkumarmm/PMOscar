create view vw_GetQAResourcesForPowerBI
as
select ResourceName, u.UserName
from Resource r
join [user] u on u.EmployeeCode=r.emp_Code
join team  t on t.TeamID=r.TeamID
where t.TeamID=6-- QA--giving ID as this is not supposed to change in future.


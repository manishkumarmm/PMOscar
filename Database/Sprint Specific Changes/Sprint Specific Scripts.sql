
--Adjustment factor--
 alter table [Resource] add JoinDate DATETIME
 alter table [Resource] add ExitDate DATETIME



---------------------------------------------------------------------------------


----------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------

-----------------------------------Back population Of JoinDate To Resource-------------------
select * into [dbo].[Temp_EmployeeDetails] from [PMOscar_Dev].[dbo].[Temp_EmployeeDetails] 

 update  [Resource] set 
JoinDate = te.date_of_joining
from Temp_EmployeeDetails te join
Resource re on
re.emp_Code=te.employeeidcode
where re.emp_Code=te.employeeidcode

-----------------------------------------------------------------------------------------------


--------------------------------------------------------------------------------------------------
 update  [Resource] set 
JoinDate = te.date_of_joining
from (select resourceid,min(fromdate) date_of_joining 
from billingdetails group by resourceid) te
join
Resource re on
re.resourceid=te.resourceid
where re.isactive=0


 update  [Resource]  set 
JoinDate = createddate
where isactive=0 and joindate is null

update [resource] set
exitdate = '2099-12-31'
where isactive = 1

 update re set 
exitdate = te.date_of_Exit
from (select resourceid,max(fromdate) date_of_Exit 
from billingdetails group by resourceid) te
join
Resource re on
re.resourceid=te.resourceid
where re.isactive=0
and exitdate is null

update [resource] set
exitdate = updateddate
where isactive = 0 and exitdate is null


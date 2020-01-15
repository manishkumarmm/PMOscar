
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


Update Resource set JoinDate='2019-04-01' where ResourceName='Aji Gopal V'
Update Resource set JoinDate='2019-03-11' where ResourceName='Niya K R'
Update Resource set JoinDate='2019-03-11' where ResourceName='Jibin Joseph'
Update Resource set JoinDate='2019-08-05' where ResourceName='Rajesh Kummil'
Update Resource set JoinDate='2019-05-13' where ResourceName='Anjaly Jose'
Update Resource set JoinDate='2019-08-26' where ResourceName='Jinu Jose'
Update Resource set JoinDate='2019-09-02' where ResourceName='Sarath K J'
Update Resource set JoinDate='2019-09-02' where ResourceName='Dona K Johny'

insert into ResourceUtilizationPercentage(ResourceId,StartDate,EndDate,UtilizationPercentage,AdjustmentFactor,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate)
select Table_A.ResourceId,isnull(table_a.JoinDate,'1900-01-01') as StartDate,'2099-12-31' as EndDate,100 as UtilizationPercentage,
0 as AdjustmentFactor,Table_A.CreatedBy,Table_A.CreatedDate,null UpdatedBy,null UpdatedDate

FROM resource join 
(select ResourceId,JoinDate,CreatedBy,createddate from Resource where ResourceId not in(select distinct resourceid from ResourceUtilizationPercentage)) Table_A
on resource.ResourceId = Table_A.ResourceId





--Budget revision--

ALTER TABLE [BudgetRevisionLog]
ADD ApprovedDate datetime ,RequestedDate datetime,BudgetRevisionName varchar(500);

------------------------- alter table to add fields-------------------
ALTER TABLE BudgetRevisionLog
ADD RequestedBy int not null default 0;

ALTER TABLE BudgetRevisionLog
ADD ApprovedBy int not null default 0;

ALTER TABLE budgetrevisionLog add Status varchar(max) null


insert into UserRole (UserRoleID,UserRole) values (6,'COO')
--------------set permission to the role-------------------
insert into Permission ([Namespace],Controller,Action,DateCreated,Name) values ('PMOSCAR.Application.Web.Controllers.API','AddEditBudgetRevision','BudgetApproval',getdate(),'BudgetApproval')
insert into Permission ([Namespace],Controller,Action,DateCreated,Name) values ('PMOSCAR.Application.Web.Controllers.API','AddEditBudgetRevision','SendForApproval',getdate(),'SendForApproval')

insert into RolePermission (RoleID,PermissionID) values ((select UserRoleId from [UserRole] where UserRole='Project Manager'),(select PermissionID FROM Permission WHERE Action='SendForApproval') )

insert into RolePermission (RoleID,PermissionID) values ((select UserRoleId from [UserRole] where UserRole='Project Owner'),(select PermissionID FROM Permission WHERE Action='SendForApproval') )

insert into RolePermission (RoleID,PermissionID) values ((select UserRoleId from [UserRole] where UserRole='COO'),(select PermissionID FROM Permission WHERE Action='BudgetApproval') )



-------------created by:Haritha E.S
-------------Bug fixes------------------
insert into Permission ([Namespace],Controller,Action,DateCreated,Name) values ('PMOSCAR.Application.Web.Controllers.API','ListBudgetRevisionController','AddBudgetRevision',getdate(),'AddBudgetRevision')

insert into RolePermission (RoleID,PermissionID) values ((select UserRoleId from [UserRole] where UserRole='Project Manager'),(select PermissionID FROM Permission WHERE Action='AddBudgetRevision') )

insert into RolePermission (RoleID,PermissionID) values ((select UserRoleId from [UserRole] where UserRole='Project Owner'),(select PermissionID FROM Permission WHERE Action='AddBudgetRevision') )











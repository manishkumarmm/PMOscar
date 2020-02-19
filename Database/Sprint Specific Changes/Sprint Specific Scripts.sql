


ALTER TABLE [dbo].[Resource] ADD [AvailableHoursStartDate] [datetime] NOT NULL DEFAULT GetDate()

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
Update Resource set JoinDate='2019-11-18' where ResourceName='Abilash Babu K V'
Update Resource set JoinDate='2019-08-19' where ResourceName='Anu James'
Update Resource set JoinDate='2019-12-02' where ResourceName='Varun K V'
Update Resource set JoinDate='2019-11-25' where ResourceName='Francis Arokiya Raj'
Update Resource set JoinDate='2020-01-02' where ResourceName='Sijoy C O'
Update Resource set JoinDate='2020-01-02' where ResourceName='Nithin Jacob Joseph'
Update Resource set JoinDate='2020-01-06' where ResourceName='Nithin Varghese'
Update Resource set JoinDate='2020-01-13' where ResourceName='Anup Zach Cherian'
Update Resource set JoinDate='2019-10-14' where ResourceName='Akshara James'
Update Resource set JoinDate='2019-10-14' where ResourceName='Bibin P S'
Update Resource set JoinDate='2019-10-14' where ResourceName='Rachana Das'
Update Resource set JoinDate='2019-10-14' where ResourceName='Reshma K Sunil'
Update Resource set JoinDate='2019-10-14' where ResourceName='Veena Ponnappan'
Update Resource set JoinDate='2018-10-25' where ResourceName='Alex Babu'
Update Resource set JoinDate='2019-09-03' where ResourceName='Sooraj T'
Update Resource set JoinDate='2019-09-18' where ResourceName='Nidesh K K'
Update Resource set JoinDate='2019-10-23' where ResourceName='Gibin Jacob Job'
Update Resource set JoinDate='2019-11-27' where ResourceName like '%limna%'
Update Resource set JoinDate='2019-11-27' where ResourceName='Kiran P'


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











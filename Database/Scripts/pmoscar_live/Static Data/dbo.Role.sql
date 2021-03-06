/*******************************************************************************************************************************
*
*            File created by DB Ghost - data and schema scripter
*            This application can be downloaded from www.dbghost.com
*
*******************************************************************************************************************************/
set nocount on

if object_id('tempdb..#ErrorCapture')is not null
	drop table #ErrorCapture

create table #ErrorCapture(Error int)
insert into #ErrorCapture(Error)values(0)

begin transaction
GO

set identity_insert [dbo].[Role] on
GO

declare @error int set @error = 0

if (select Error from #ErrorCapture) <> 0 goto ErrorHandler

delete from [dbo].[Role]
set @error = @@error if @error<>0 goto ErrorHandler

insert [dbo].[Role]([RoleId],[Role],[ShortName],[Desription],[EstimationPercentage],[EstimationRoleID])
values(1,'Developer','Dev',null,0.75,1)
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Role]([RoleId],[Role],[ShortName],[Desription],[EstimationPercentage],[EstimationRoleID])
values(2,'Graphic Designer','GD',null,1,6)
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Role]([RoleId],[Role],[ShortName],[Desription],[EstimationPercentage],[EstimationRoleID])
values(3,'UI Designer','UIDev',null,1,6)
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Role]([RoleId],[Role],[ShortName],[Desription],[EstimationPercentage],[EstimationRoleID])
values(4,'Quality Analyst','QA',null,0.8,2)
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Role]([RoleId],[Role],[ShortName],[Desription],[EstimationPercentage],[EstimationRoleID])
values(5,'Business System Analyst','BSA',null,1,7)
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Role]([RoleId],[Role],[ShortName],[Desription],[EstimationPercentage],[EstimationRoleID])
values(6,'Architect','Arch',null,1,3)
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Role]([RoleId],[Role],[ShortName],[Desription],[EstimationPercentage],[EstimationRoleID])
values(7,'SInfrastructure','Infra',null,1,8)
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Role]([RoleId],[Role],[ShortName],[Desription],[EstimationPercentage],[EstimationRoleID])
values(8,'Project Manager','PM',null,1,5)
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Role]([RoleId],[Role],[ShortName],[Desription],[EstimationPercentage],[EstimationRoleID])
values(9,'DB Developer','DBDev',null,0.75,9)
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Role]([RoleId],[Role],[ShortName],[Desription],[EstimationPercentage],[EstimationRoleID])
values(10,'Business Developer','BD',null,1,10)
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Role]([RoleId],[Role],[ShortName],[Desription],[EstimationPercentage],[EstimationRoleID])
values(11,'Jr. Developer ','JrDev',null,0.5,1)
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Role]([RoleId],[Role],[ShortName],[Desription],[EstimationPercentage],[EstimationRoleID])
values(12,'Sr. Developer','SrDev',null,1,1)
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Role]([RoleId],[Role],[ShortName],[Desription],[EstimationPercentage],[EstimationRoleID])
values(13,'Sr. Quality Analyst','SrQA',null,1,2)
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Role]([RoleId],[Role],[ShortName],[Desription],[EstimationPercentage],[EstimationRoleID])
values(14,'Jr. Quality Analyst','JrQA',null,0.6,2)
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Role]([RoleId],[Role],[ShortName],[Desription],[EstimationPercentage],[EstimationRoleID])
values(15,'Team Lead','TL',null,1,4)
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Role]([RoleId],[Role],[ShortName],[Desription],[EstimationPercentage],[EstimationRoleID])
values(16,'Sr. DB Developer','SrDBDev',null,1,11)
set @error = @@error if @error<>0 goto ErrorHandler

ErrorHandler: if @error <> 0 update #ErrorCapture set Error = @error
GO

set identity_insert [dbo].[Role] off
GO

--check to see if we should rollback or commit the transaction
if @@trancount <> 0 begin
	if (select Error from #ErrorCapture) = 0
		commit transaction
	else
		rollback transaction
end
GO

--if you are in the wrong database, you will end up with an open transaction as the error handler
--will not pick up a schema error, so here we check the transaction count and rollback if above 0
--this will only happen in query analyzer as the connection would stay alive.
if @@trancount<>0 rollback
GO


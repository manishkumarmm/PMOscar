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

set identity_insert [dbo].[EstimationRole] on
GO

declare @error int set @error = 0

if (select Error from #ErrorCapture) <> 0 goto ErrorHandler

delete from [dbo].[EstimationRole]
set @error = @@error if @error<>0 goto ErrorHandler

insert [dbo].[EstimationRole]([EstimationRoleID],[RoleName],[ShortName])
values(1,N'Sr. Developer',N'SrDev')
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[EstimationRole]([EstimationRoleID],[RoleName],[ShortName])
values(2,N'Sr. Quality Analyst',N'SrQA')
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[EstimationRole]([EstimationRoleID],[RoleName],[ShortName])
values(3,N'Architect',N'Arch')
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[EstimationRole]([EstimationRoleID],[RoleName],[ShortName])
values(4,N'Team Lead',N'TL')
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[EstimationRole]([EstimationRoleID],[RoleName],[ShortName])
values(5,N'Project Manager',N'PM')
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[EstimationRole]([EstimationRoleID],[RoleName],[ShortName])
values(6,N'Graphic Designer',N'GD')
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[EstimationRole]([EstimationRoleID],[RoleName],[ShortName])
values(7,N'Business System Analyst',N'BSA')
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[EstimationRole]([EstimationRoleID],[RoleName],[ShortName])
values(8,N'SI Infrastructure',N'Infra')
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[EstimationRole]([EstimationRoleID],[RoleName],[ShortName])
values(9,N'DB Developer',N'DBDev')
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[EstimationRole]([EstimationRoleID],[RoleName],[ShortName])
values(10,N'Business Developer',N'BD')
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[EstimationRole]([EstimationRoleID],[RoleName],[ShortName])
values(11,N'Sr. DB Developer',N'SrDBDev')
set @error = @@error if @error<>0 goto ErrorHandler

ErrorHandler: if @error <> 0 update #ErrorCapture set Error = @error
GO

set identity_insert [dbo].[EstimationRole] off
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


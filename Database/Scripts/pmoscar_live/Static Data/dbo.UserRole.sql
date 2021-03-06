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

declare @error int set @error = 0

if (select Error from #ErrorCapture) <> 0 goto ErrorHandler

delete from [dbo].[UserRole]
set @error = @@error if @error<>0 goto ErrorHandler

insert [dbo].[UserRole]([UserRoleID],[UserRole])
values(1,'Project Owner')
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[UserRole]([UserRoleID],[UserRole])
values(2,'Project Manager')
set @error = @@error if @error<>0 goto ErrorHandler

ErrorHandler: if @error <> 0 update #ErrorCapture set Error = @error
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


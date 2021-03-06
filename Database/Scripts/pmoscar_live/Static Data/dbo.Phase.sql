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

delete from [dbo].[Phase]
set @error = @@error if @error<>0 goto ErrorHandler

insert [dbo].[Phase]([PhaseID],[Phase],[ShortName],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate])
values(1,'Proposal','Proposal',1,'2011-03-08T00:00:00',1,'2011-03-08T00:00:00')
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Phase]([PhaseID],[Phase],[ShortName],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate])
values(2,'Execution','Execution',1,'2011-03-08T00:00:00',1,'2011-03-08T00:00:00')
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Phase]([PhaseID],[Phase],[ShortName],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate])
values(3,'Deployment','Deployment',1,'2011-03-08T00:00:00',1,'2011-03-08T00:00:00')
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Phase]([PhaseID],[Phase],[ShortName],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate])
values(4,'Maintenance','Maintenance',1,'2011-03-08T00:00:00',1,'2011-03-08T00:00:00')
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Phase]([PhaseID],[Phase],[ShortName],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate])
values(5,'Change Requests','Change Requests',1,'2011-03-08T00:00:00',1,'2011-03-08T00:00:00')
set @error = @@error if @error<>0 goto ErrorHandler
insert [dbo].[Phase]([PhaseID],[Phase],[ShortName],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate])
values(6,'Value Added Services','Value Added Services',1,'2011-03-08T00:00:00',1,'2011-03-08T00:00:00')
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


alter table [dbo].[Project] add SVNPath nvarchar(500)   
alter table [dbo].[Project] add ProjectDetails nvarchar(max) 

ALTER TABLE DBO.[Resource]  DROP CONSTRAINT DF__Resource__Weekly__498EEC8D
ALTER TABLE DBO.[Resource] ALTER COLUMN WeeklyHour DECIMAL(10,1)
ALTER TABLE DBO.[Resource]  ADD CONSTRAINT DF__Resource__Weekly DEFAULT 40.0 FOR WeeklyHour

ALTER TABLE [Resource] ADD WeeklyHour INT NULL DEFAULT 40

UPDATE [Resource] SET WeeklyHour = 40 WHERE ResourceID NOT IN (SELECT ResourceID FROM  [Resource] where ResourceName in('Najimunissa A.','Abdul Raniz'))
UPDATE [Resource] SET WeeklyHour = 40 WHERE ResourceID IN (SELECT ResourceID FROM  [Resource] where ResourceName in('Najimunissa A.','Abdul Raniz'))
									
									
alter table [dbo].[Project] add ShowInDashboard int
ALTER TABLE BillingDetails ADD UBT varchar(10)
alter table [dbo].[BillingDetails] add Comments nvarchar(MAX)   
ALTER TABLE [dbo].[BillingDetails] ADD ActualSpentHours int

update [dbo].[Project] set ShowInDashboard =1
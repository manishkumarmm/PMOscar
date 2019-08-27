
GO

Create table SyncProcessTracker
(
SyncProcessTrackerId  int IDENTITY(1,1) primary key  
 ,Startdate datetime
 , EndDate datetime
 , SyncUserId int
 , Status varchar(15) 
 ,ErrorDescription varchar(250)
 );


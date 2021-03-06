
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ImportMySQLTimeTracker') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ImportMySQLTimeTracker]
GO
/****** Object:  StoredProcedure [dbo].[ImportMySQLTimeTracker]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[ImportMySQLTimeTracker]

as 

-- =============================================
-- Author:		Aswathy R
-- Create date: 2018-03-13
-- Modified by : Haritha E.S
-- Modified On : 06-06-2018
-- Modified By : Joshwa George
-- Modified On : 2018/06/22
-- Modified On : 2018/07/12
-- Description:	Sync
-- =============================================

BEGIN

		 declare @DBName varchar(50)
		 select @DBName= db_name()
		SET NOCOUNT ON;

		truncate table TimeTracker_MySQL
		if(@DBName='PMOscar_Dev')
		Begin
			INSERT INTO TimeTracker_MySQL 
			(
			 ID ,
			 UserId ,
			 [Date] ,
			 [Start]  ,
			 Duration  ,
			 Clientid  ,
			 Projectid  ,
			 Activityid ,
			 Taskid  ,
			 Invoiceid  , 
			 Billable ,
			 PMOscarProjectID,
			 EmployeeCode,
			 Comment,
			 PMOscarPhaseID
			 )
			 select  ID ,
			 [User_Id] ,
			 [Date] ,
			 [Start]  ,
			 Duration  ,
			 Client_id  ,
			 Project_id  ,
			 Activity_id ,
			 Task_id  ,
			 Invoice_id  , 
			 Billable ,
			 PMOscarProjectID,
			 empl_code,
             activtiy+'/'+task +' (' +Comment +')'as Comment,
			 PMOscarPhaseID 
			 FROM openquery( MYSQL_TIMETRACKER, 'SELECT  
			  l.ID ,
			 User_Id ,
			 Date ,
			 Start  ,
			 Duration  ,
			 l.Client_id  ,
			 Project_id  ,
			 Activity_id ,
			 Task_id  ,
			 Invoice_id  , 
			 Billable ,
			 p.PMOscarProjectID,
			 u.empl_code,
			 case when PMOscarProjectID in (2,16) then convert (l.Comment,char(8000)) else null end as Comment,
			 l.PMOscarPhaseID,
			 a.name activtiy,
			 t.name task
			 FROM tt_log l
			 left join tt_activities a on a.id = l.activity_id
			 left join tt_tasks t on t.id = l.task_id
			 inner join tt_projects p on p.id=l.project_Id
			 inner join tt_users u on u.id=l.user_id
			 where year(date)>=year(now())-1
			 ')
			-- WHERE [Date]>=DATEADD(wk ,DATEDIFF(wk ,14 ,GETDATE()) ,0) and [Date]<=GETDATE()

			 End

			 if(@DBName='PMOscar_Test')
		Begin
			INSERT INTO TimeTracker_MySQL 
			(
			 ID ,
			 UserId ,
			 [Date] ,
			 [Start]  ,
			 Duration  ,
			 Clientid  ,
			 Projectid  ,
			 Activityid ,
			 Taskid  ,
			 Invoiceid  , 
			 Billable ,
			 PMOscarProjectID,
			 EmployeeCode,
			 Comment,
			 PMOscarPhaseID
			 )
			 select  ID ,
			 [User_Id] ,
			 [Date] ,
			 [Start]  ,
			 Duration  ,
			 Client_id  ,
			 Project_id  ,
			 Activity_id ,
			 Task_id  ,
			 Invoice_id  , 
			 Billable ,
			 PMOscarProjectID,
			 empl_code,
			 activtiy+'/'+task +' (' +Comment +')'as Comment,
			 PMOscarPhaseID
			 FROM openquery( MYSQL_TIMETRACKER_QA, 'SELECT  
			  l.ID ,
			 User_Id ,
			 Date ,
			 Start  ,
			 Duration  ,
			 l.Client_id  ,
			 Project_id  ,
			 Activity_id ,
			 Task_id  ,
			 Invoice_id  , 
			 Billable ,
			 p.PMOscarProjectID,
			 u.empl_code,
			 case when PMOscarProjectID in (2,16) then convert (l.Comment ,char(8000)) else null end as Comment,
			 l.PMOscarPhaseID,
			 a.name activtiy,
			 t.name task
			 FROM tt_log l
			 left join tt_activities a on a.id = l.activity_id
			 left join tt_tasks t on t.id = l.task_id
			 inner join tt_projects p on p.id=l.project_Id
			 inner join tt_users u on u.id=l.user_id
			 where year(date)>=year(now())-1
			 ')
			-- WHERE [Date]>=DATEADD(wk ,DATEDIFF(wk ,14 ,GETDATE()) ,0) and [Date]<=GETDATE()

			 End

			 if(@DBName='PMOscar_Live')
		Begin
			INSERT INTO TimeTracker_MySQL 
			(
			 ID ,
			 UserId ,
			 [Date] ,
			 [Start]  ,
			 Duration  ,
			 Clientid  ,
			 Projectid  ,
			 Activityid ,
			 Taskid  ,
			 Invoiceid  , 
			 Billable ,
			 PMOscarProjectID,
			 EmployeeCode,
			 Comment,
			 PMOscarPhaseID
			 )
			 select  ID ,
			 [User_Id] ,
			 [Date] ,
			 [Start]  ,
			 Duration  ,
			 Client_id  ,
			 Project_id  ,
			 Activity_id ,
			 Task_id  ,
			 Invoice_id  , 
			 Billable ,
			 PMOscarProjectID,
			 empl_code,
			 activtiy+'/'+task +' (' +Comment +')'as Comment,
			 PMOscarPhaseID
		  
			 FROM openquery( MYSQLTIMETRACKER_Live, 'SELECT  
			  l.ID ,
			 User_Id ,
			 Date ,
			 Start  ,
			 Duration  ,
			 l.Client_id  ,
			 Project_id  ,
			 Activity_id ,
			 Task_id  ,
			 Invoice_id  , 
			 Billable ,
			 p.PMOscarProjectID,
			 u.empl_code,
			 case when PMOscarProjectID in (2,16) then convert (l.Comment ,char(8000)) else null end as Comment,
			 l.PMOscarPhaseID,
			 a.name activtiy,
			 t.name task
			 FROM tt_log l
			 left join tt_activities a on a.id = l.activity_id
			 left join tt_tasks t on t.id = l.task_id
			 inner join tt_projects p on p.id=l.project_Id
			 inner join tt_users u on u.id=l.user_id
			 where year(date)>=year(now())-1
			 ')
			 WHERE [Date]>=DATEADD(wk ,DATEDIFF(wk ,14 ,GETDATE()) ,0) and [Date]<=GETDATE()

			 End
			 
			   if(@DBName='PMOscarN2NTestEvn')
		Begin
			INSERT INTO TimeTracker_MySQL 
			(
			 ID ,
			 UserId ,
			 [Date] ,
			 [Start]  ,
			 Duration  ,
			 Clientid  ,
			 Projectid  ,
			 Activityid ,
			 Taskid  ,
			 Invoiceid  , 
			 Billable ,
			 PMOscarProjectID,
			 EmployeeCode,
			 Comment,
			 PMOscarPhaseID
			 )
			 select  ID ,
			 [User_Id] ,
			 [Date] ,
			 [Start]  ,
			 Duration  ,
			 Client_id  ,
			 Project_id  ,
			 Activity_id ,
			 Task_id  ,
			 Invoice_id  , 
			 Billable ,
			 PMOscarProjectID,
			 empl_code,
			 activtiy+'/'+task +' (' +Comment +')'as Comment,
			 PMOscarPhaseID
		  
			 FROM openquery( MYSQL_TIMETRACKERN2N, 'SELECT  
			  l.ID ,
			 User_Id ,
			 Date ,
			 Start  ,
			 Duration  ,
			 l.Client_id  ,
			 Project_id  ,
			 Activity_id ,
			 Task_id  ,
			 Invoice_id  , 
			 Billable ,
			 p.PMOscarProjectID,
			 u.empl_code,
			 case when PMOscarProjectID in (2,16) then convert (l.Comment ,char(8000)) else null end as Comment,
			 l.PMOscarPhaseID,
			 a.name activtiy,
			 t.name task
			 FROM tt_log l
			 left join tt_activities a on a.id = l.activity_id
			 left join tt_tasks t on t.id = l.task_id
			 inner join tt_projects p on p.id=l.project_Id
			 inner join tt_users u on u.id=l.user_id
			 where year(date)>=year(now())-1
			 ')
			 --WHERE [Date]>=DATEADD(wk ,DATEDIFF(wk ,14 ,GETDATE()) ,0) and [Date]<=GETDATE()

			 End
			 if(@DBName='PMOscar_Hotfix')
		Begin
			INSERT INTO TimeTracker_MySQL 
			(
			 ID ,
			 UserId ,
			 [Date] ,
			 [Start]  ,
			 Duration  ,
			 Clientid  ,
			 Projectid  ,
			 Activityid ,
			 Taskid  ,
			 Invoiceid  , 
			 Billable ,
			 PMOscarProjectID,
			 EmployeeCode,
			 Comment,
			 PMOscarPhaseID
			 )
			 select  ID ,
			 [User_Id] ,
			 [Date] ,
			 [Start]  ,
			 Duration  ,
			 Client_id  ,
			 Project_id  ,
			 Activity_id ,
			 Task_id  ,
			 Invoice_id  , 
			 Billable ,
			 PMOscarProjectID,
			 empl_code,
			 activtiy+'/'+task +' (' +Comment +')'as Comment,
			 PMOscarPhaseID
		  
			 FROM openquery( MYSQL_TIMETRACKER_HotFix, 'SELECT  
			  l.ID ,
			 User_Id ,
			 Date ,
			 Start  ,
			 Duration  ,
			 l.Client_id  ,
			 Project_id  ,
			 Activity_id ,
			 Task_id  ,
			 Invoice_id  , 
			 Billable ,
			 p.PMOscarProjectID,
			 u.empl_code,
			 case when PMOscarProjectID in (2,16) then convert (l.Comment ,char(8000)) else null end as Comment,
			 l.PMOscarPhaseID,
			 a.name activtiy,
			 t.name task
			 FROM tt_log l
			 left join tt_activities a on a.id = l.activity_id
			 left join tt_tasks t on t.id = l.task_id
			 inner join tt_projects p on p.id=l.project_Id
			 inner join tt_users u on u.id=l.user_id
			 where year(date)>=year(now())-1
			 ')
			 WHERE [Date]>=DATEADD(wk ,DATEDIFF(wk ,14 ,GETDATE()) ,0) and [Date]<=GETDATE()

			 End
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'AddProjectResources') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddProjectResources]
GO
--AddProjectResources 122,'7'


-- ==============================================================================
-- Description: Add Project Resources
--   -101: Interanl System Error
-- ==============================================================================
-- Author: Maysarah  
-- Create date: 2018-04-13
-- Modified By : Vibin MB
-- Modified date : 2018/06/20
-- Modified By : Joshwa
-- Modified date : 2018/07/06
-- ==============================================================================


CREATE PROCEDURE AddProjectResources
  @ProjectID INT,@ResourceID VARCHAR(max)
  AS 
  
  BEGIN

  DECLARE @DBName varchar(50)
		 select @DBName= db_name()
  DECLARE @TimeTrackerProjectID int 
  DECLARE @TimetrackuserList VARCHAR(MAX)
  DECLARE @Toactivelist VARCHAR(MAX)    
  DECLARE @QUERY varchar(max)
  DECLARE @QUERY1 varchar(max)
  create table #TempTbl (ResourceID int );
  create table #pmoscarinactiveusers(ResourceID int,emp_Code varchar(max))

  insert into #TempTbl(ResourceID)
  SELECT Item FROM  SplitString(@ResourceID, ',');


   INSERT INTO ProjectResources ([ProjectID],[ResourceID])
   SELECT @ProjectID,ResourceID
   FROM #TempTbl T
   WHERE not exists  (SELECT 1 
                   FROM   ProjectResources PR Where PR.ProjectID=@ProjectID
				   and T.ResourceID=Pr.ResourceID 
				   );
  


UPDATE ProjectResources SET IsActive = 0 WHERE 
projectid =@ProjectID and 
ResourceID NOT IN ( SELECT ResourceID
   FROM #TempTbl)

UPDATE ProjectResources  SET IsActive = 1 WHERE projectid =@ProjectID 
and ResourceID IN ( SELECT ResourceID
   FROM #TempTbl) 
  
 INSERT INTO #pmoscarinactiveusers(ResourceID,emp_Code) 
   SELECT p.ResourceID,r.emp_Code 
   from dbo.Resource r
   join dbo.ProjectResources p on p.ResourceID = r.ResourceId  
   where ProjectID = @ProjectID 
   and p.IsActive = 0
      
  ---Add Resource to MySQL Timetracker 
   if(@DBName='PMOscar_Dev')
		Begin
 SELECT * into #TmpUsers FROM OPENQUERY (MYSQL_TIMETRACKER, '
   SELECT 
     u.id as UserID
   , up.project_id ProjectID
   ,P.PMOscarProjectID
   ,u.empl_code
   ,up.status
   FROM  tt_users u
   LEFT JOIN tt_user_project_binds up on u.ID=up.USer_ID
   LEFT JOIN tt_Projects p on up.project_ID=p.ID
   WHERE u.status=1; 
')

create table #TempInactUsers(ResourceID int,IsActive int,UserID int,ProjectID int);
INSERT INTO #TempInactUsers([ResourceID],[UserID],[ProjectID])
   SELECT p.Resourceid,T.UserID,T.ProjectID
   FROM #pmoscarinactiveusers P 
   JOIN #TmpUsers T ON T.PMOscarProjectID = @ProjectID
   AND T.empl_code = P.emp_Code

     
 SELECT @TimetrackuserList  = COALESCE(@TimetrackuserList +',' ,'') + convert(varchar,userid )
                FROM 
                 #TempInactUsers

 
     
  SELECT @Toactivelist  = COALESCE(@Toactivelist +',' ,'') + convert(varchar,UserID )
                 FROM ProjectResources p 
				 join dbo.Resource R on R.ResourceId = P.ResourceID 
				 join #TmpUsers T on T.empl_code = R.emp_Code 
				 and T.PMOscarProjectID = p.ProjectID
				 WHERE T.status = 0 and p.IsActive = 1
				 and p.ProjectID = @ProjectID



SELECT @TimeTrackerProjectID=ID 
 FROM OPENQUERY (MYSQL_TIMETRACKER, '
   SELECT 
    P.ID 
   ,P.PMOscarProjectID
   FROM tt_Projects p; 
')
WHERE  PMOscarProjectID=@ProjectID

if (isnull(@TimeTrackerProjectID,0)<>0)
BEGIN

SET @QUERY1 ='
UPDATE OPENQUERY(MYSQL_TIMETRACKER,
''SELECT ID,status from tt_user_project_binds
WHERE project_id = '+convert(varchar,@TimeTrackerProjectID)+'
AND user_id IN ('+@Toactivelist+')'')
SET status = 1'
EXEC (@QUERY1)
				

INSERT INTO OPENQUERY (MYSQL_TIMETRACKER, '
SELECT 
 user_id
, project_id
, rate
, status 
FROM tt_user_project_binds; 
')
SELECT distinct t.UserID,@TimeTrackerProjectID,0,1
FROM #TmpUsers t
join dbo.Resource R on t.empl_code=R.emp_Code
JOIN dbo.ProjectResources PR on PR.ResourceID=R.ResourceID
left join #TmpUsers t1 on t.userID=t1.USerID 
		and t1.PMOscarProjectID=PR.ProjectID 
		and t1.projectID=@TimeTrackerProjectID
		and  t1.PMOscarProjectID is not null
WHERE PR.ProjectID=@ProjectID
and t1.userid is null

SET @QUERY ='
UPDATE OPENQUERY(MYSQL_TIMETRACKER,
''SELECT ID,status from tt_user_project_binds
WHERE project_id = '+convert(varchar,@TimeTrackerProjectID)+'
AND user_id IN ('+@TimetrackuserList+')'')
SET status = 0'
EXEC (@QUERY)


End
End
------
  if(@DBName='PMOscar_Test')
		Begin
 SELECT * into #TmpUsers_test FROM OPENQUERY (MYSQL_TIMETRACKER_QA, '
   SELECT 
     u.id as UserID
   , up.project_id ProjectID
   ,P.PMOscarProjectID
   ,u.empl_code
   ,up.status
   FROM  tt_users u
   LEFT JOIN tt_user_project_binds up on u.ID=up.USer_ID
   LEFT JOIN tt_Projects p on up.project_ID=p.ID
   WHERE u.status=1; 
')

create table #TempInactUsers_Test(ResourceID int,IsActive int,UserID int,ProjectID int);
INSERT INTO #TempInactUsers_Test([ResourceID],[UserID],[ProjectID])
   SELECT p.Resourceid,T.UserID,T.ProjectID
   FROM #pmoscarinactiveusers P 
   JOIN #TmpUsers_test T ON T.PMOscarProjectID = @ProjectID
   AND T.empl_code = P.emp_Code

   SELECT @TimetrackuserList  = COALESCE(@TimetrackuserList +',' ,'') + convert(varchar,userid )
                FROM 
                 #TempInactUsers_Test


  SELECT @Toactivelist  = COALESCE(@Toactivelist +',' ,'') + convert(varchar,UserID )
                 FROM ProjectResources p 
				 join dbo.Resource R on R.ResourceId = P.ResourceID 
				 join #TmpUsers_test T on T.empl_code = R.emp_Code
				 and T.PMOscarProjectID = p.ProjectID
				 WHERE T.status = 0 and p.IsActive = 1
				 and p.ProjectID = @ProjectID

SELECT @TimeTrackerProjectID=ID 
 FROM OPENQUERY (MYSQL_TIMETRACKER_QA, '
   SELECT 
    P.ID 
   ,P.PMOscarProjectID
   FROM tt_Projects p; 
')
WHERE  PMOscarProjectID=@ProjectID

if (isnull(@TimeTrackerProjectID,0)<>0)
BEGIN

SET @QUERY1 ='
UPDATE OPENQUERY(MYSQL_TIMETRACKER_QA,
''SELECT ID,status from tt_user_project_binds
WHERE project_id = '+convert(varchar,@TimeTrackerProjectID)+'
AND user_id IN ('+@Toactivelist+')'')
SET status = 1'
EXEC (@QUERY1)

INSERT INTO OPENQUERY (MYSQL_TIMETRACKER_QA, '
SELECT 
 user_id
, project_id
, rate
, status 
FROM tt_user_project_binds; 
')
SELECT distinct t.UserID,@TimeTrackerProjectID,0,1
FROM #TmpUsers_test t
join dbo.Resource R on t.empl_code=R.emp_Code
JOIN dbo.ProjectResources PR on PR.ResourceID=R.ResourceID
left join #TmpUsers_test t1 on t.userID=t1.USerID 
		and t1.PMOscarProjectID=PR.ProjectID 
		and t1.projectID=@TimeTrackerProjectID
		and  t1.PMOscarProjectID is not null
WHERE PR.ProjectID=@ProjectID
and t1.userid is null

SET @QUERY ='
UPDATE OPENQUERY(MYSQL_TIMETRACKER_QA,
''SELECT ID,status from tt_user_project_binds
WHERE project_id = '+convert(varchar,@TimeTrackerProjectID)+'
AND user_id IN ('+@TimetrackuserList+')'')
SET status = 0'
EXEC (@QUERY)


End
End
------------
  if(@DBName='PMOscar_Live')
		Begin
						 SELECT * into #TmpUsers_Live FROM OPENQUERY (MYSQLTIMETRACKER_Live, '
						   SELECT 
							 u.id as UserID
						   , up.project_id ProjectID
						   ,P.PMOscarProjectID
						   ,u.empl_code
						   ,up.status
						   FROM  tt_users u
						   LEFT JOIN tt_user_project_binds up on u.ID=up.USer_ID
						   LEFT JOIN tt_Projects p on up.project_ID=p.ID
						   WHERE u.status=1; 
						')

						create table #TempInactUsers_Live(ResourceID int,IsActive int,UserID int,ProjectID int);
						INSERT INTO #TempInactUsers_Live([ResourceID],[UserID],[ProjectID])
						   SELECT p.Resourceid,T.UserID,T.ProjectID
						   FROM #pmoscarinactiveusers P 
						   JOIN #TmpUsers_Live T ON T.PMOscarProjectID = @ProjectID
						   AND T.empl_code = P.emp_Code

						SELECT @TimetrackuserList  = COALESCE(@TimetrackuserList +',' ,'') + convert(varchar,userid )
							FROM #TempInactUsers_Live

							SELECT @Toactivelist  = COALESCE(@Toactivelist +',' ,'') + convert(varchar,UserID )
								 FROM ProjectResources p 
								 join dbo.Resource R on R.ResourceId = P.ResourceID 
								 join #TmpUsers_Live T on T.empl_code = R.emp_Code
								 and T.PMOscarProjectID = p.ProjectID
								 WHERE T.status = 0 and p.IsActive = 1
								 and p.ProjectID = @ProjectID


						SELECT @TimeTrackerProjectID=ID 
						 FROM OPENQUERY (MYSQLTIMETRACKER_Live, '
						   SELECT 
							P.ID 
						   ,P.PMOscarProjectID
						   FROM tt_Projects p; 
						')
						WHERE  PMOscarProjectID=@ProjectID

						if (isnull(@TimeTrackerProjectID,0)<>0)
						BEGIN

						SET @QUERY1 ='
							UPDATE OPENQUERY(MYSQLTIMETRACKER_Live,
							''SELECT ID,status from tt_user_project_binds
							WHERE project_id = '+convert(varchar,@TimeTrackerProjectID)+'
							AND user_id IN ('+@Toactivelist+')'')
							SET status = 1'
							EXEC (@QUERY1)

						INSERT INTO OPENQUERY (MYSQLTIMETRACKER_Live, '
						SELECT 
						 user_id
						, project_id
						, rate
						, status 
						FROM tt_user_project_binds; 
						')
						SELECT distinct t.UserID,@TimeTrackerProjectID,0,1
						FROM #TmpUsers_Live t
						join dbo.Resource R on t.empl_code=R.emp_Code
						JOIN dbo.ProjectResources PR on PR.ResourceID=R.ResourceID
						left join #TmpUsers_Live t1 on t.userID=t1.USerID 
								and t1.PMOscarProjectID=PR.ProjectID 
								and t1.projectID=@TimeTrackerProjectID
								and  t1.PMOscarProjectID is not null
						WHERE PR.ProjectID=@ProjectID
						and t1.userid is null

						SET @QUERY ='
							UPDATE OPENQUERY(MYSQLTIMETRACKER_Live,
							''SELECT ID,status from tt_user_project_binds
							WHERE project_id = '+convert(varchar,@TimeTrackerProjectID)+'
							AND user_id IN ('+@TimetrackuserList+')'')
							SET status = 0'
							EXEC (@QUERY)

						End
					END
-----------------
		 if(@DBName='PMOscarN2NTestEvn')
		Begin
			 SELECT * into #TmpUsers_test_N2N FROM OPENQUERY (MYSQL_TIMETRACKERN2N, '
			   SELECT 
				 u.id as UserID
			   , up.project_id ProjectID
			   ,P.PMOscarProjectID
			   ,u.empl_code
			   ,up.status
			   FROM  tt_users u
			   LEFT JOIN tt_user_project_binds up on u.ID=up.USer_ID
			   LEFT JOIN tt_Projects p on up.project_ID=p.ID
			   WHERE u.status=1; 
			')

			create table #TempInactUsers_test_N2N(ResourceID int,IsActive int,UserID int,ProjectID int);
			INSERT INTO #TempInactUsers_test_N2N([ResourceID],[UserID],[ProjectID])
						   SELECT p.Resourceid,T.UserID,T.ProjectID
						   FROM #pmoscarinactiveusers P 
						   JOIN #TmpUsers_test_N2N T ON T.PMOscarProjectID = @ProjectID
						   AND T.empl_code = P.emp_Code

						SELECT @TimetrackuserList  = COALESCE(@TimetrackuserList +',' ,'') + convert(varchar,userid )
							FROM #TempInactUsers_test_N2N


							SELECT @Toactivelist  = COALESCE(@Toactivelist +',' ,'') + convert(varchar,UserID )
								 FROM ProjectResources p 
								 join dbo.Resource R on R.ResourceId = P.ResourceID 
								 join #TmpUsers_test_N2N T on T.empl_code = R.emp_Code
								 and T.PMOscarProjectID = p.ProjectID
								 WHERE T.status = 0 and p.IsActive = 1
								 and p.ProjectID = @ProjectID


			SELECT @TimeTrackerProjectID=ID 
			 FROM OPENQUERY (MYSQL_TIMETRACKERN2N, '
			   SELECT 
				P.ID 
			   ,P.PMOscarProjectID
			   FROM tt_Projects p; 
			')
			WHERE  PMOscarProjectID=@ProjectID

			if (isnull(@TimeTrackerProjectID,0)<>0)
			BEGIN

			SET @QUERY1 ='
					UPDATE OPENQUERY(MYSQL_TIMETRACKERN2N,
					''SELECT ID,status from tt_user_project_binds
					WHERE project_id = '+convert(varchar,@TimeTrackerProjectID)+'
					AND user_id IN ('+@Toactivelist+')'')
					SET status = 1'
					EXEC (@QUERY1)

			INSERT INTO OPENQUERY (MYSQL_TIMETRACKERN2N, '
			SELECT 
			 user_id
			, project_id
			, rate
			, status 
			FROM tt_user_project_binds; 
			')
			SELECT distinct t.UserID,@TimeTrackerProjectID,0,1
			FROM #TmpUsers_test_N2N t
			join dbo.Resource R on t.empl_code=R.emp_Code
			JOIN dbo.ProjectResources PR on PR.ResourceID=R.ResourceID
			left join #TmpUsers_test_N2N t1 on t.userID=t1.USerID 
					and t1.PMOscarProjectID=PR.ProjectID 
					and t1.projectID=@TimeTrackerProjectID
					and  t1.PMOscarProjectID is not null
			WHERE PR.ProjectID=@ProjectID
			and t1.userid is null

			SET @QUERY ='
					UPDATE OPENQUERY(MYSQL_TIMETRACKERN2N,
					''SELECT ID,status from tt_user_project_binds
					WHERE project_id = '+convert(varchar,@TimeTrackerProjectID)+'
					AND user_id IN ('+@TimetrackuserList+')'')
					SET status = 0'
					EXEC (@QUERY)

	End
End

----------------------------------
if(@DBName='PMOscar_Hotfix')
		Begin
 SELECT * into #TmpUsers_Hotfix FROM OPENQUERY (MYSQL_TIMETRACKER_HotFix, '
   SELECT 
     u.id as UserID
   , up.project_id ProjectID
   ,P.PMOscarProjectID
   ,u.empl_code
   ,up.status
   FROM  tt_users u
   LEFT JOIN tt_user_project_binds up on u.ID=up.USer_ID
   LEFT JOIN tt_Projects p on up.project_ID=p.ID
   WHERE u.status=1; 
')

create table #TempInactUsers_Hotfix(ResourceID int,IsActive int,UserID int,ProjectID int);
INSERT INTO #TempInactUsers_Hotfix([ResourceID],[UserID],[ProjectID])
   SELECT p.Resourceid,T.UserID,T.ProjectID
   FROM #pmoscarinactiveusers P 
   JOIN #TmpUsers_Hotfix T ON T.PMOscarProjectID = @ProjectID
   AND T.empl_code = P.emp_Code

   SELECT @TimetrackuserList  = COALESCE(@TimetrackuserList +',' ,'') + convert(varchar,userid )
                FROM 
                 #TempInactUsers_Hotfix


  SELECT @Toactivelist  = COALESCE(@Toactivelist +',' ,'') + convert(varchar,UserID )
                 FROM ProjectResources p 
				 join dbo.Resource R on R.ResourceId = P.ResourceID 
				 join #TmpUsers_Hotfix T on T.empl_code = R.emp_Code
				 and T.PMOscarProjectID = p.ProjectID
				 WHERE T.status = 0 and p.IsActive = 1
				 and p.ProjectID = @ProjectID

SELECT @TimeTrackerProjectID=ID 
 FROM OPENQUERY (MYSQL_TIMETRACKER_HotFix, '
   SELECT 
    P.ID 
   ,P.PMOscarProjectID
   FROM tt_Projects p; 
')
WHERE  PMOscarProjectID=@ProjectID

if (isnull(@TimeTrackerProjectID,0)<>0)
BEGIN

SET @QUERY1 ='
UPDATE OPENQUERY(MYSQL_TIMETRACKER_HotFix,
''SELECT ID,status from tt_user_project_binds
WHERE project_id = '+convert(varchar,@TimeTrackerProjectID)+'
AND user_id IN ('+@Toactivelist+')'')
SET status = 1'
EXEC (@QUERY1)

INSERT INTO OPENQUERY (MYSQL_TIMETRACKER_HotFix, '
SELECT 
 user_id
, project_id
, rate
, status 
FROM tt_user_project_binds; 
')
SELECT distinct t.UserID,@TimeTrackerProjectID,0,1
FROM #TmpUsers_Hotfix t
join dbo.Resource R on t.empl_code=R.emp_Code
JOIN dbo.ProjectResources PR on PR.ResourceID=R.ResourceID
left join #TmpUsers_Hotfix t1 on t.userID=t1.USerID 
		and t1.PMOscarProjectID=PR.ProjectID 
		and t1.projectID=@TimeTrackerProjectID
		and  t1.PMOscarProjectID is not null
WHERE PR.ProjectID=@ProjectID
and t1.userid is null

SET @QUERY ='
UPDATE OPENQUERY(MYSQL_TIMETRACKER_HotFix,
''SELECT ID,status from tt_user_project_binds
WHERE project_id = '+convert(varchar,@TimeTrackerProjectID)+'
AND user_id IN ('+@TimetrackuserList+')'')
SET status = 0'
EXEC (@QUERY)


End
End


END





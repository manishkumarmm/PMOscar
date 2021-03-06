IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'UpdateTimeTrackerProject') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateTimeTrackerProject]
GO
/****** Object:  StoredProcedure [dbo].[UpdateTimeTrackerProject]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--UpdateTimeTrackerProject 1154

CREATE PROC [dbo].[UpdateTimeTrackerProject]

@ProjectID INT
AS
BEGIN


DECLARE @OpenQuery VARCHAR(5000)
DECLARE @SQL VARCHAR(max)

declare @DBName varchar(50)
		 select @DBName= db_name()

SET @SQL='UPDATE B
SET B.activities=activity_ID
FROM   OPENQUERY ('+iif(@DBName='PMOscar_Live','MYSQLTIMETRACKER_Live',iif(@DBName='PMOscar_Test','MYSQL_TIMETRACKER_QA',iif(@DBName='PMOscarN2NTestEvn','MYSQL_TIMETRACKERN2N',iif(@DBName='PMOscar_Hotfix','MYSQL_TIMETRACKER_HotFix','MYSQL_TIMETRACKER'))))+','

SET @OpenQuery='''select 	GROUP_CONCAT(distinct activity_ID) activity_ID
,p.ID as ProjectID 	,P.Tasks,P.	activities
		FROM  tt_Projects P
		INNER JOIN tt_project_activity_binds a on P.ID =a.Project_ID 
			
    where p.PMOscarProjectId='+CONVERT(VARCHAR,@ProjectID)+'
    group by p.ID  '''

SET @SQL =@SQL+@OpenQuery+') B'

EXEC (@SQL)



	 END
GO

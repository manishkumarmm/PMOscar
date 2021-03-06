

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'CreateProjectInTimeTracker') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateProjectInTimeTracker]
GO


/****** Object:  StoredProcedure [dbo].[CreateProjectInTimeTracker]    Script Date: 28-03-2018 10:56:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ==============================================================================
-- Description: Create Project In TimeTracker
--   -101: Interanl System Error
-- ==============================================================================
-- Author: Maysarah  
-- Create date: 2018-04-13
-- Modified By : Joshwa George
-- Modified On : 2018-06-14
-- ==============================================================================



CREATE proc [dbo].[CreateProjectInTimeTracker]
@ProjectName varchar(100)
,@ProjectID int
,@StartDate varchar(25)
,@EndDate varchar(25)
,@IsUpdate bit=0
,@IsActive bit=1
,@phaseId int
as 
Begin
declare @DBName varchar(50)
		 select @DBName= db_name()
--EXEC CreateProjectInTimeTracker 'TestEstimation10',1884,'01/06/2018','31/07/2018',1,0,1
if (@ISUpdate=0)
begin

if(@DBName='PMOscar_Dev')
		Begin

INSERT INTO OPENQUERY (MYSQL_TIMETRACKER, 'SELECT team_id ,
  name ,
  description ,
  tasks ,
  activities ,
  status ,
  PMOscarProjectID,
  StartDate,
  EndDate,
  PMOscarPhaseID
  FROM tt_projects; 
')
SELECT 2, @ProjectName,@ProjectName,'','' ,@IsActive ,@ProjectID,CONVERT(VARCHAR(10), CONVERT(date, @StartDate, 105), 23) , CONVERT(VARCHAR(10), CONVERT(date, @EndDate, 105), 23),@phaseId
End



if(@DBName='PMOscar_Test')
		Begin

INSERT INTO OPENQUERY (MYSQL_TIMETRACKER_QA, 'SELECT team_id ,
  name ,
  description ,
  tasks ,
  activities ,
  status ,
  PMOscarProjectID,
  StartDate,
  EndDate,
  PMOscarPhaseID
  FROM tt_projects; 
')
SELECT 2, @ProjectName,@ProjectName,'','' ,@IsActive ,@ProjectID,CONVERT(VARCHAR(10), CONVERT(date, @StartDate, 105), 23) , CONVERT(VARCHAR(10), CONVERT(date, @EndDate, 105), 23),@phaseId
End


if(@DBName='PMOscar_Live')
		Begin

INSERT INTO OPENQUERY (MYSQLTIMETRACKER_Live, 'SELECT team_id ,
  name ,
  description ,
  tasks ,
  activities ,
  status ,
  PMOscarProjectID,
  StartDate,
  EndDate,
  PMOscarPhaseID
  FROM tt_projects; 
')
SELECT 2, @ProjectName,@ProjectName,'','' ,@IsActive ,@ProjectID,CONVERT(VARCHAR(10), CONVERT(date, @StartDate, 105), 23) , CONVERT(VARCHAR(10), CONVERT(date, @EndDate, 105), 23),@phaseId

End

if(@DBName='PMOscarN2NTestEvn')
		Begin

INSERT INTO OPENQUERY (MYSQL_TIMETRACKERN2N, 'SELECT team_id ,
  name ,
  description ,
  tasks ,
  activities ,
  status ,
  PMOscarProjectID,
  StartDate,
  EndDate,
  PMOscarPhaseID
  FROM tt_projects; 
')
SELECT 2, @ProjectName,@ProjectName,'','' ,@IsActive ,@ProjectID,CONVERT(VARCHAR(10), CONVERT(date, @StartDate, 105), 23) , CONVERT(VARCHAR(10), CONVERT(date, @EndDate, 105), 23),@phaseId

End

if(@DBName='PMOscar_Hotfix')
		Begin

INSERT INTO OPENQUERY (MYSQL_TIMETRACKER_HotFix, 'SELECT team_id ,
  name ,
  description ,
  tasks ,
  activities ,
  status ,
  PMOscarProjectID,
  StartDate,
  EndDate,
  PMOscarPhaseID
  FROM tt_projects; 
')
SELECT 2, @ProjectName,@ProjectName,'','' ,@IsActive ,@ProjectID,CONVERT(VARCHAR(10), CONVERT(date, @StartDate, 105), 23) , CONVERT(VARCHAR(10), CONVERT(date, @EndDate, 105), 23),@phaseId

End



end

else if (@ISUpdate=1)
begin
declare @SQL varchar(max)
SET @SQL ='
UPDATE  OPENQUERY ('+iif(@DBName='PMOscar_Live','MYSQLTIMETRACKER_Live',iif(@DBName='PMOscar_Hotfix','MYSQL_TIMETRACKER_HotFix',iif(@DBName='PMOscar_Test','MYSQL_TIMETRACKER_QA',iif(@DBName='PMOscarN2NTestEvn','MYSQL_TIMETRACKERN2N','MYSQL_TIMETRACKER'))))+', ''SELECT team_id ,
  name ,
  description ,
  tasks ,
  activities ,
  status ,
  PMOscarProjectID,
  StartDate,
  EndDate,
  PMOscarPhaseID
  FROM tt_projects
  WHERE PMOscarProjectID='+convert(varchar,@ProjectID)+' 
'')
SET name ='''+@ProjectName+
''',description='''+@ProjectName+'''
,status='+convert(varchar,   @IsActive )+ '
,StartDate= CONVERT(VARCHAR(10), CONVERT(date, '''+@StartDate+''', 105), 23) 
,EndDate=CONVERT(VARCHAR(10), CONVERT(date, '''+@EndDate+''', 105), 23)
,PMOscarPhaseID='+convert(varchar,   @phaseId )

EXEC (@SQL)
end
End


GO



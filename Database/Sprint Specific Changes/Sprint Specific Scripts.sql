---history table modification changes------
---modified by :kochurani ---------------------


SELECT * INTO TimeTrackerAudit20180711
FROM TimeTrackerAudit; 

sp_RENAME 'TimeTrackerAudit.EstimatedHours', 'OldEstimatedHours' , 'COLUMN'

sp_RENAME 'TimeTrackerAudit.ActualHours', 'OldActualHours' , 'COLUMN'

ALTER TABLE TimeTrackerAudit
ADD NewEstimatedHours int; 


ALTER TABLE TimeTrackerAudit
ADD NewActualHours decimal(18,2);


ALTER TABLE TimeTrackerAudit
ADD Action NVARCHAR(100);

SELECT TTA.Timetrackerauditid,TTA.ProjectID,tta.timetrackerid,tt.Oldestimatedhours,tt.Oldactualhours,tta.next_estimated,tta.next_actual,tta.updateddate
	into temp_timetrackeraudit
    from TimeTrackerAudit tt 
    inner join (
			Select 
				TimeTrackerAuditID,timetrackerid,projectid,Oldestimatedhours,Oldactualhours,Newestimatedhours,Newactualhours,updateddate,
				LAST_VALUE(Oldestimatedhours) OVER (PARTITION BY timetrackerid ORDER BY updateddate  ROWS BETWEEN CURRENT ROW AND 1 FOLLOWING) AS next_estimated,
				LAST_VALUE(Oldactualhours) OVER (PARTITION BY timetrackerid ORDER BY updateddate  ROWS BETWEEN CURRENT ROW AND 1 FOLLOWING) AS next_actual
			From  dbo.TimeTrackerAudit TA 
			)TTA on TTA.Timetrackerauditid=tt.Timetrackerauditid 

UPDATE
		 tt
	SET
		tt.Newestimatedhours = temp.next_estimated,
		tt.Newactualhours = temp.next_actual
	FROM
		TimeTrackeraudit AS tt
		INNER JOIN temp_timetrackeraudit AS temp
			ON tt.Timetrackerauditid = temp.Timetrackerauditid

UPDATE 	 TT
SET Newestimatedhours = TTA.EstimatedHours,
	Newactualhours = TTA.ActualHours
from TimeTrackerAudit tt inner join
(select  tta.TimeTrackerAuditID ,tt.TimeTrackerId as a,tta.TimeTrackerID as b,tt.EstimatedHours,tt.ActualHours 
from timetracker tt 
inner join  
	(
		select row_number() over(partition by timetrackerid,projectid order by timetrackerid,timetrackerauditid desc) as rn,* 
		from dbo.TimeTrackerAudit TA 
	)tta on  tta.rn=1 and tt.TimeTrackerId=tta.TimeTrackerID )TTA on TTA.Timetrackerauditid=tt.Timetrackerauditid  

sp_RENAME 'TimeTrackerAudit.PhaseID', 'OldPhaseID' , 'COLUMN'


ALTER TABLE TimeTrackerAudit
ADD NewPhaseID int; 



sp_RENAME 'TimeTrackerAudit.ProjectID', 'OldProjectID' , 'COLUMN'


ALTER TABLE TimeTrackerAudit
ADD NewProjectID int; 


ALTER TABLE TimeTrackerAudit
ADD InsertedDate DATETIME NOT NULL DEFAULT (GETDATE());



update TimeTrackerAudit set InsertedDate=UpdatedDate where UpdatedDate is not null

--Hot FixRelease 2017-07-27

select p.phaseid,*  from TimeTracker t
join project p on p.projectid=t.ProjectId
where t.PhaseID<>p.PhaseID
and t.FromDate>'2018-07-20'


--update t set t.phaseid=p.phaseid

----select *
--from  TimeTracker t
--join project p on p.projectid=t.ProjectId
--where t.PhaseID<>p.PhaseID
--and t.FromDate>'2018-07-20'
--and t.phaseid=1
-------------


-----------------ALTER TABLE TIMETRACKER--------------
ALTER TABLE TimeTracker
        ADD ActHrsUpdated Bit NULL 
 CONSTRAINT df_actHrsUpdated 
    DEFAULT (0)


update TimeTracker set ActHrsUpdated = 0 where ActHrsUpdated is null

----------------------ALTER TABLE Project---------------------------------------------
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Project]') 
  AND name = 'BugProjectId'
)
BEGIN
	Alter table [dbo].[Project] Add BugProjectId  int
END
	
-----------------------Alter table program-----------------------------------------------

	ALTER TABLE program
    ADD ClientId int,
    FOREIGN KEY(ClientId) REFERENCES Client(ClientId);

	---------------------backpopulation---------------------------------------------------------
	UPDATE Program
   SET Program.ClientID =[PMOscar_Hotfix].[dbo].Client_Program.ClientID
   FROM Program INNER JOIN [PMOscar_Hotfix].[dbo].Client_Program ON Program.ProgId= Client_Program.ProgId 
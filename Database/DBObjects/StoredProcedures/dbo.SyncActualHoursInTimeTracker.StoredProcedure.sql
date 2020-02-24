
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SyncActualHoursInTimeTracker') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SyncActualHoursInTimeTracker]
GO
--SyncActualHoursInTimeTracker '2018-06-04','2018-06-08',null,126


-- ==============================================================================
-- Description: Sync Actual Hours In TimeTracker
-- ==============================================================================
-- Author:  Aswathy
-- Create date: 2018-04-13
-- Modified By : Joshwa George
-- Modified On : 2018-06-13
-- Modified On : 2018/06/20
-- Modified On : 2018/06/22
-- ==============================================================================


--SyncActualHoursInTimeTracker '2018-06-04','2018-06-08',null,126


-- ==============================================================================
-- Description: Sync Actual Hours In TimeTracker
-- ==============================================================================
-- Author:  Aswathy
-- Create date: 2018-04-13
-- Modified By : Joshwa George
-- Modified On : 2018-06-13
-- Modified On : 2018/06/20
-- Modified On : 2018/06/22
-- ==============================================================================


CREATE proc [dbo].[SyncActualHoursInTimeTracker]
@FromDate varchar(25)=NULL
,@ToDate varchar(25)=NULL
,@UserId int=NULL
,@ResourceID int=NULL
as
Begin

Declare @SystemUSerID int
,@Error varchar(100)

Select @SystemUSerID=userid 
From dbo.[user] 
where UserName ='SystemUSer';

		if @FromDate is null
		begin
			SET @FromDate =DATEADD(wk ,DATEDIFF(wk ,6 ,getdate()) ,0)
			SET @UserId=@SystemUSerID
		end
		if @ToDate is null
		begin
			SET @ToDate= DATEADD(wk ,DATEDIFF(wk ,7 ,getdate()) ,6)
		end

		
EXEC InsertSyncProcessTracker @userId,0;

BEGIN TRY 
--Import data from MySQL Timetracker to PMOcscar



if(@ResourceID!=0)
begin
MERGE [dbo].[TimeTracker] T
USING 
(SELECT  R.ResourceID
		,case when SUM(DATEDIFF(mi,CONVERT(time,'00:00:00'),CONVERT(time,Duration))/60.0)='0.000000' then '24.0000000' else SUM(DATEDIFF(mi,CONVERT(time,'00:00:00'),CONVERT(time,Duration))/60.0) end as ActualHours
		, MySQLT.PMOscarProjectID		
		,Ph.PhaseID		
        ,R.[RoleId]
		,R.[TeamID] 
		,month(MySQLT.Date) Month_Date
		, STUFF((    SELECT  DISTINCT '' +   CONVERT(VARCHAR,t2.dATE)+ ' - '+ COMMENT  +char(10)
					 FROM TimeTracker_MySQL T2 
					 WHERE T2.EmployeeCode =MySQLT .EmployeeCode 
						 AND T2.PMOscarProjectID =MySQLT .PMOscarProjectID 
						 and  T2.Date>=@FromDate
					     and T2.Date<=@ToDate
						 AND T2.PMOscarProjectID IN (2,16)
						  and month(MySQLT.Date)=month(T2.Date)
                         FOR XML PATH('')
                     ), 1, 0, '' ) Comment
	FROM [Resource] R 	 
	JOIN TimeTracker_MySQL MySQLT on R.emp_Code=MySQLT.EmployeeCode
	JOIN Project P on P.ProjectID=MySQLT.PMOscarProjectID
	JOIN Phase Ph on Ph.PhaseID =  MySQLT.PMOscarPhaseID
	WHERE MySQLT.Date>=@FromDate--DATEADD(wk ,DATEDIFF(wk ,6 ,GETDATE()) ,0) 
	and MySQLT.Date<=@ToDate--DATEADD(wk ,DATEDIFF(wk ,7 ,GETDATE()) ,6)
	and R.ResourceId =@ResourceID
	GROUP BY  R.ResourceID, MySQLT.PMOscarProjectID,Ph.PhaseID
		
        ,R.[RoleId]
		,R.[TeamID] ,month(MySQLT.Date)
		,MySQLT .EmployeeCode 

) LG
on  (LG.ResourceID=T.ResourceID) AND (LG.PMOscarProjectID = T.ProjectID)
and T.Fromdate>=@FromDate--DATEADD(wk ,DATEDIFF(wk ,6 ,GETDATE()) ,0) 
and T.Todate<=@ToDate--DATEADD(wk ,DATEDIFF(wk ,7 ,GETDATE()) ,6)
and month(T.[ToDate])=LG.Month_Date 
and LG.phaseid= T.PhaseID
WHEN MATCHED
THEN UPDATE 
   SET   
      [ActualHours] = case when T.ActHrsUpdated = 0 THEN LG.ActualHours ELSE T.[ActualHours] END
      ,[UpdatedBy] = @SystemUSerID
      ,[UpdatedDate] = getdate()
	   ,[WeeklyComments]=LG.Comment
   
WHEN NOT MATCHED 
THEN INSERT 
           ([ProjectId]
           ,[ResourceId]
           ,[RoleId]
           ,[FromDate]
           ,[ToDate]
           ,[EstimatedHours]
           ,[ActualHours]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[UpdatedBy]
           ,[UpdatedDate]
           ,[PhaseID]
           ,[WeeklyComments]
           ,[TeamID])
VALUES
		
		   (
		   LG.PMOscarProjectID 
		   ,LG.[ResourceId]
           ,LG.[RoleId]
           --,@FromDate--DATEADD(wk ,DATEDIFF(wk ,6 ,GETDATE()) ,0) 
           --,@ToDate--DATEADD(wk ,DATEDIFF(wk ,7 ,GETDATE()) ,6)
		   ,iif(LG.Month_Date=month(@FromDate),@FromDate,CAST(DATEADD(DAY,-DAY(@ToDate)+1, CAST(@ToDate as date)) as datetime))
           ,iif(LG.Month_Date=month(@ToDate),@ToDate, DATEADD(dd,-(DAY(DATEADD(mm,1,@FromDate))),DATEADD(mm,1,@FromDate))) 
           ,0 
           ,ActualHours 
           ,@SystemUSerID
           ,getdate() 
           ,@SystemUSerID
           ,getdate()
           ,LG.PhaseID 
           ,LG.[Comment]
           ,LG.[TeamID]    
		   
		    );	
			
INSERT INTO ProjectEstimation([ProjectID] ,
	        [PhaseID] ,
			[EstimationRoleID] ,
			[BillableHours] ,
			[BudgetHours] ,
			[RevisedBudgetHours] ,
			[CreatedBy] ,
			[CreatedDate] ,
			[UpdatedBy] ,
			[UpdatedDate] ,
			[Comments] )

	SELECT  P.ProjectID		
			,Ph.PhaseID	
			,B.EstimationRoleID
			,0 as BillableHours
			,0 AS BudgetedHours
			,0 as RevisedBudgetedHours		
			,NULL as CreatedBy
			,GETDATE() AS CreatedDate
			,NULL as UpdatedBy
			,NULL as UpdatedDate
			,NULL as Comments
		
FROM [Resource] R 	
	JOIN TimeTracker_MySQL MySQLT on R.emp_Code=MySQLT.EmployeeCode
	JOIN Project P on p.ProjectId = MySQLT.PMOscarProjectID
	JOIN [Role] B on B.RoleId=R.RoleId
	JOIN Phase ph on ph.phaseID = MySQLT.PMOscarPhaseID
	JOIN EstimationRole C on B.EstimationRoleID=C.EstimationRoleID
WHERE MySQLT.Date>=@FromDate--DATEADD(wk ,DATEDIFF(wk ,6 ,GETDATE()) ,0) 
	and MySQLT.Date<=@ToDate--DATEADD(wk ,DATEDIFF(wk ,7 ,GETDATE()) ,6)
	and R.ResourceId = @ResourceID
	and not exists (select 1  from dbo.ProjectEstimation A 
					where A.PhaseID = Ph.PhaseID  
					and A.EstimationRoleID = B.EstimationRoleID
					and A.ProjectID = MySQLT.PMOscarProjectID)
GROUP BY  R.ResourceID, P.ProjectID,Ph.PhaseID		
          ,R.[RoleId]
		  ,R.[TeamID] ,month(MySQLT.Date)
		  ,MySQLT .EmployeeCode 
		  ,B.EstimationRoleID 
end

else
begin
EXEC ImportMySQLTimeTracker;
	begin
		MERGE [dbo].[TimeTracker] T
USING 
(SELECT  R.ResourceID
		,case when SUM(DATEDIFF(mi,CONVERT(time,'00:00:00'),CONVERT(time,Duration))/60.0)='0.000000' then '24.0000000'  else SUM(DATEDIFF(mi,CONVERT(time,'00:00:00'),CONVERT(time,Duration))/60.0) end as ActualHours
		, MySQLT.PMOscarProjectID		
		,Ph.PhaseID		
        ,R.[RoleId]
		,R.[TeamID] 
		,month(MySQLT.Date) Month_Date
		, STUFF((    SELECT  DISTINCT '' +   CONVERT(VARCHAR,t2.dATE)+ ' - '+ COMMENT  +char(10)
					 FROM TimeTracker_MySQL T2 
					 WHERE T2.EmployeeCode =MySQLT .EmployeeCode 
						 AND T2.PMOscarProjectID =MySQLT .PMOscarProjectID 
						 and  T2.Date>=@FromDate
					     and T2.Date<=@ToDate
						 AND T2.PMOscarProjectID IN (2,16)
						  and month(MySQLT.Date)=month(T2.Date)
                         FOR XML PATH('')
                     ), 1, 0, '' ) Comment
	FROM [Resource] R 	 
	JOIN TimeTracker_MySQL MySQLT on R.emp_Code=MySQLT.EmployeeCode
	JOIN Project P on P.ProjectID=MySQLT.PMOscarProjectID
	JOIN Phase Ph on Ph.PhaseID =  MySQLT.PMOscarPhaseID 
	WHERE MySQLT.Date>=@FromDate--DATEADD(wk ,DATEDIFF(wk ,6 ,GETDATE()) ,0) 
	and MySQLT.Date<=@ToDate--DATEADD(wk ,DATEDIFF(wk ,7 ,GETDATE()) ,6)	
	GROUP BY  R.ResourceID, MySQLT.PMOscarProjectID,Ph.PhaseID
		
        ,R.[RoleId]
		,R.[TeamID] ,month(MySQLT.Date)
		,MySQLT .EmployeeCode 

) LG
on  (LG.ResourceID=T.ResourceID) AND (LG.PMOscarProjectID = T.ProjectID)
and T.Fromdate>=@FromDate--DATEADD(wk ,DATEDIFF(wk ,6 ,GETDATE()) ,0) 
and T.Todate<=@ToDate--DATEADD(wk ,DATEDIFF(wk ,7 ,GETDATE()) ,6)
and month(T.[ToDate])=LG.Month_Date 
and LG.phaseid= T.PhaseID
WHEN MATCHED
THEN UPDATE 
   SET   
      [ActualHours] = case when T.ActHrsUpdated = 0 THEN LG.ActualHours ELSE T.[ActualHours] END 
      ,[UpdatedBy] = @SystemUSerID
      ,[UpdatedDate] = getdate()
	  ,[WeeklyComments]=LG.Comment
   
WHEN NOT MATCHED 
THEN INSERT 
           ([ProjectId]
           ,[ResourceId]
           ,[RoleId]
           ,[FromDate]
           ,[ToDate]
           ,[EstimatedHours]
           ,[ActualHours]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[UpdatedBy]
           ,[UpdatedDate]
           ,[PhaseID]
           ,[WeeklyComments]
           ,[TeamID])
VALUES
		
		   (
		   LG.PMOscarProjectID 
		   ,LG.[ResourceId]
           ,LG.[RoleId]
           --,@FromDate--DATEADD(wk ,DATEDIFF(wk ,6 ,GETDATE()) ,0) 
           --,@ToDate--DATEADD(wk ,DATEDIFF(wk ,7 ,GETDATE()) ,6)
		   ,iif(LG.Month_Date=month(@FromDate),@FromDate,CAST(DATEADD(DAY,-DAY(@ToDate)+1, CAST(@ToDate as date)) as datetime))
           ,iif(LG.Month_Date=month(@ToDate),@ToDate, DATEADD(dd,-(DAY(DATEADD(mm,1,@FromDate))),DATEADD(mm,1,@FromDate))) 
           ,0 
           ,ActualHours 
           ,@SystemUSerID
           ,getdate() 
           ,@SystemUSerID
           ,getdate()
           ,LG.PhaseID 
           ,LG.[Comment]
           ,LG.[TeamID]    
		   
		    );


INSERT INTO ProjectEstimation([ProjectID] ,
			[PhaseID] ,
			[EstimationRoleID] ,
			[BillableHours] ,
			[BudgetHours] ,
			[RevisedBudgetHours] ,
			[CreatedBy] ,
			[CreatedDate] ,
			[UpdatedBy] ,
			[UpdatedDate] ,
			[Comments] )

	SELECT DISTINCT P.ProjectID		
			,Ph.PhaseID	
			,B.EstimationRoleID
			,0 as BillableHours
			,0 AS BudgetedHours
			,0 as RevisedBudgetedHours		
			,NULL as CreatedBy
			,GETDATE() AS CreatedDate
			,NULL as UpdatedBy
			,NULL as UpdatedDate
			,NULL as Comments
		
FROM [Resource] R 	
	JOIN TimeTracker_MySQL MySQLT on R.emp_Code=MySQLT.EmployeeCode
	JOIN Project P on p.ProjectId = MySQLT.PMOscarProjectID
	JOIN [Role] B on B.RoleId=R.RoleId
	JOIN Phase ph on ph.PhaseID = MySQLT.PMOscarPhaseID
	JOIN EstimationRole C on B.EstimationRoleID=C.EstimationRoleID
WHERE MySQLT.Date>=@FromDate --DATEADD(wk ,DATEDIFF(wk ,6 ,GETDATE()) ,0) 
	and MySQLT.Date<=@ToDate --DATEADD(wk ,DATEDIFF(wk ,7 ,GETDATE()) ,6)
	and not exists (select 1  from dbo.ProjectEstimation A 
					where A.PhaseID = Ph.PhaseID 
					and A.EstimationRoleID = B.EstimationRoleID 
					and A.ProjectID = MySQLT.PMOscarProjectID)
GROUP BY  R.ResourceID, P.ProjectID,Ph.PhaseID		
          ,R.[RoleId]
		  ,R.[TeamID] ,month(MySQLT.Date)
		  ,MySQLT .EmployeeCode 
		  ,B.EstimationRoleID

	end
	end

If(@ResourceID!=0)
Begin
	UPDATE  T

	SET ActualHours=0.0
	,[UpdatedBy] = @SystemUSerID
	,[UpdatedDate] = getdate()

	--select *  
	FROM  [dbo].[TimeTracker] T
	INNER JOIN [Resource] R 	on T.ResourceID=R.ResourceID	
	LEFT JOIN (select month(Date) Month_Date
						,PMOscarProjectID 
						,EmployeeCode EmployeeCode
					, Date [date]
					,PMOscarPhaseID
			  from  TimeTracker_MySQL
			  where  [Date]>=@FromDate
					and [Date]<=@ToDate)
			 MySQLT on R.emp_Code=MySQLT.EmployeeCode
		AND T.ProjectID=MySQLT.PMOscarProjectID
		and MySQLT.Month_Date=month(T.FromDate)
		and  MySQLT.Month_Date=month(T.ToDate)
		and MySQLT.PMOscarPhaseID =T.PhaseID
	WHERE MySQLT.EmployeeCode is null
	 AND T.FromDate>=@FromDate--DATEADD(wk ,DATEDIFF(wk ,6 ,GETDATE()) ,0) 
	 and T.ToDate<=@ToDate--DATEADD(wk ,DATEDIFF(wk ,7 ,GETDATE()) ,6)
	 and R.ResourceId =@ResourceID		
end

else

	Begin
	UPDATE  T

	SET ActualHours=0.0
	,[UpdatedBy] = @SystemUSerID
	,[UpdatedDate] = getdate()

	--select *  
	FROM  [dbo].[TimeTracker] T
	INNER JOIN [Resource] R 	on T.ResourceID=R.ResourceID	
	LEFT JOIN (select month(Date) Month_Date
						,PMOscarProjectID 
						,EmployeeCode EmployeeCode
					, Date [date]
					,PMOscarPhaseID
			  from  TimeTracker_MySQL
			  where  [Date]>=@FromDate
					and [Date]<=@ToDate)
			 MySQLT on R.emp_Code=MySQLT.EmployeeCode
		AND T.ProjectID=MySQLT.PMOscarProjectID
		and MySQLT.Month_Date=month(T.FromDate)
		and  MySQLT.Month_Date=month(T.ToDate)
		and MySQLT.PMOscarPhaseID =T.PhaseID
	WHERE MySQLT.EmployeeCode is null
	 AND T.FromDate>=@FromDate--DATEADD(wk ,DATEDIFF(wk ,6 ,GETDATE()) ,0) 
	 and T.ToDate<=@ToDate--DATEADD(wk ,DATEDIFF(wk ,7 ,GETDATE()) ,6)	
	 AND T.ActHrsUpdated = 0
	end
 
 EXEC InsertSyncProcessTracker @userId,1;

 END TRY

 BEGIN CATCH
	SET @Error=ERROR_MESSAGE()
	EXEC InsertSyncProcessTracker @userId,1,@Error

 END CATCH


End





GO


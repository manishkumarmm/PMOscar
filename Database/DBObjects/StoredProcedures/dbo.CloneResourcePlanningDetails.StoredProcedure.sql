IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'CloneResourcePlanningDetails') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CloneResourcePlanningDetails]

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CloneResourcePlanningDetails]
	@fromdate nvarchar(50),
	@todate nvarchar(50),
	@clonefromdate nvarchar(50),
	@clonetodate nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
if (DATEPART(DAY,@clonefromdate) > DATEPART(DAY,@clonetodate) and (DATEDIFF(DAY, DATEADD(m, DATEDIFF(m, 0, @clonetodate), 0), @clonetodate ))=0)
begin
	INSERT INTO TimeTracker(ProjectId,ResourceId,RoleId,FromDate,ToDate,EstimatedHours,ActualHours,CreatedBy,CreatedDate,UpdatedDate,PhaseID,WeeklyComments,TeamID)
	SELECT 
	ProjectId
	,ResourceId
	,RoleId, 
	@clonefromdate,
	EOMONTH ( @clonefromdate ) ,
	EstimatedHours,
	NULL,CreatedBy,GetDate(),GetDate(),PhaseID,NULL,TeamID
	from [dbo].[TimeTracker] 
	WHERE FromDate>=@fromdate AND ToDate<=@todate
	group by ProjectId,ResourceId,RoleId,CreatedBy,PhaseID,TeamID,EstimatedHours
	union
	
	SELECT ProjectId
	,ResourceId
	,RoleId,  
	DATEADD(m, DATEDIFF(m, 0, @clonetodate), 0),
	@clonetodate ,
	0,
	NULL,CreatedBy,GetDate(),GetDate(),PhaseID,NULL,TeamID
	 from [dbo].[TimeTracker] 
		WHERE FromDate>=@fromdate AND ToDate<=@todate
		group by ProjectId,ResourceId,RoleId,CreatedBy,PhaseID,TeamID
end

else if (DATEPART(DAY,@clonefromdate) > DATEPART(DAY,@clonetodate)and (DATEDIFF(DAY, DATEADD(m, DATEDIFF(m, 0, @clonetodate), 0), @clonetodate ))!=0)
begin
		INSERT INTO TimeTracker(ProjectId,ResourceId,RoleId,FromDate,ToDate,EstimatedHours,ActualHours,CreatedBy,CreatedDate,UpdatedDate,PhaseID,WeeklyComments,TeamID)
		SELECT 
		ProjectId
		,ResourceId
		,RoleId, 
		@clonefromdate,
		EOMONTH ( @clonefromdate ) ,
		((((DATEDIFF(day,@clonefromdate,EOMONTH ( @clonefromdate ))+1)*8)*EstimatedHours)/40) AS Estimatedhours,
		NULL,CreatedBy,GetDate(),GetDate(),PhaseID,NULL,TeamID
		 from [dbo].[TimeTracker] 
		 WHERE FromDate>=@fromdate AND ToDate<=@todate
		 group by ProjectId,ResourceId,RoleId,CreatedBy,PhaseID,TeamID,EstimatedHours

	union
	
	SELECT ProjectId
	,ResourceId
	,RoleId,  
	DATEADD(m, DATEDIFF(m, 0, @clonetodate), 0),
	@clonetodate ,
	 ((((DATEDIFF(day,DATEADD(m, DATEDIFF(m, 0, @clonetodate), 0),@clonetodate)-1)*8)*EstimatedHours)/40) AS Estimatedhours,
	NULL,CreatedBy,GetDate(),GetDate(),PhaseID,NULL,TeamID
	 from [dbo].[TimeTracker] 
		WHERE FromDate>=@fromdate AND ToDate<=@todate
		group by ProjectId,ResourceId,RoleId,CreatedBy,PhaseID,TeamID,EstimatedHours
	end


else

begin
	INSERT INTO TimeTracker(ProjectId,ResourceId,RoleId,FromDate,ToDate,EstimatedHours,ActualHours,CreatedBy,CreatedDate,UpdatedDate,PhaseID,WeeklyComments,TeamID)
	SELECT ProjectId,ResourceId,RoleId,@clonefromdate,@clonetodate,EstimatedHours,NULL,CreatedBy,GetDate(),GetDate(),PhaseID,NULL,TeamID
	FROM TimeTracker 
	WHERE FromDate>=@fromdate AND ToDate<=@todate
end

End
   


GO

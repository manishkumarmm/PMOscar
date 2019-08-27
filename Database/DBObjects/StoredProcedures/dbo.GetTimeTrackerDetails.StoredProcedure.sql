IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetTimeTrackerDetails') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetTimeTrackerDetails]
/****** Object:  StoredProcedure [dbo].[GetTimeTrackerDetails]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



   
--exec [GetTimeTrackerDetails] '2015','01','2016-1-11','2016-1-17'
                   
CREATE PROCEDURE [dbo].[GetTimeTrackerDetails]                        
 -- Add the parameters for the stored procedure here                        
 @Year int,                        
 @Month int,                        
 @dayfrom varchar(15),                        
 @dayto varchar(15),
 @EmployeeCode varchar(25)=null                      
                    
AS                        
BEGIN

--Declare variables here
Declare 
	@ResourceId int, @RoleId int , @ProjectId int
	,@WCTemp varchar(1500), @WeeklyComments varchar(max)
	

  SET NOCOUNT ON 
--	added to prevent extra result sets from                        
--  interfering with SELECT statements.                    
                     
 SET NOCOUNT ON; 
Create table #TempTT
(
	ResourceName varchar(250)
	,ResourceId int
	,Role varchar(250)
	,ProjectName varchar(250)
	,Estimated int
	,Actual decimal(18,2)
	,ESTTotal decimal(18,2)
	,ACTTotal decimal(18,2)
	,BudgetHours int
	,WeeklyComments varchar(max)
	,ProjectId int
	,EstimationPercentage int
	,EstimationRoleID int
	,Status varchar(2)
	,PHCount int
	,RoleID int
	,EmpCode varchar(50)
	,Leave decimal(10,2)
	,DefaultRole int
)

insert into #TempTT
(
	ResourceName
	,ResourceId
	,Role
	,ProjectName
	,Estimated
	,Actual
	,ESTTotal
	,ACTTotal
	,BudgetHours
	,WeeklyComments
	,ProjectId
	,EstimationPercentage
	,EstimationRoleID
	,Status
	,PHCount
	,RoleID
	,EmpCode
	,DefaultRole 
)                  
(            
	Select 
		R.ResourceName,R.ResourceId
		,Role.ShortName as Role,ProjectName
		,Sum(TimeTracker.EstimatedHours) As Estimated,(sum(TimeTracker.ActualHours)) As Actual
		,sum(TimeTracker.EstimatedHours) As ESTTotal,sum(TimeTracker.ActualHours) As ACTTotal  
		,isnull(Sum(ProjectEstimation.RevisedBudgetHours),0) As BudgetHours, null as WeeklyComments
		,Project.ProjectId,Role.EstimationPercentage
		,Role.EstimationRoleID,'N'[status],Count(TimeTracker.PhaseID) as PHCount,Role.RoleID as RoleID
		,R.emp_Code	as EmpCode, R.RoleID as DefaultRole
	From 
		[Resource]As R 
		left Join TimeTracker 
			On TimeTracker.ResourceId = R.ResourceId
			--And (Year(TimeTracker.FromDate)=@Year or Year(TimeTracker.ToDate)=@Year)
			and TimeTracker.FromDate>=@dayfrom 
			and TimeTracker.ToDate<=@dayto                   
		Inner join Role 
			On TimeTracker.RoleId = Role.RoleId 
		Inner join Project 
			On Project.ProjectId = TimeTracker.ProjectId                      
		Left join ProjectEstimation 
			on  ProjectEstimation.ProjectId = Project.ProjectId  
			and ProjectEstimation.EstimationRoleID=Role.EstimationRoleID          
			and ProjectEstimation.PhaseID=TimeTracker.PhaseID   
	Group by  
		R.ResourceName,R.ResourceId,Role.ShortName
		,ProjectName,Project.ProjectId
		,Role.EstimationPercentage,Role.EstimationRoleID,Role.RoleID,R.emp_Code, R.RoleID 
)
Union all               
(                        
	Select 
		distinct R.ResourceName,R.ResourceId
		,NULL As Role,NULL ProjectName
		,NULL  As Estimated,NULL As Actual
		,NULL As ESTTotal,NULL As ACTTotal
		,NULL  As BudgetHours,Null As WeeklyComments
		,0 ProjectId,0 EstimationPercentage 
		,0  EstimationRoleID,Null as Status,0 as PHCount,null as RoleID
		,R.emp_Code	as EmpCode, NULL as DefaultRole                
	From 
		[Resource]as R 
	where 
		R.ResourceId Not In
			(
				Select TimeTracker.ResourceId 
				From TimeTracker                        
					Inner join Project On Project.ProjectId = TimeTracker.ProjectId             
				where 
					--(Year(TimeTracker.FromDate)=@Year or Year(TimeTracker.ToDate)=@Year) and 
					TimeTracker.FromDate>=@dayfrom and TimeTracker.ToDate<=@dayto 
			) 
			And R.IsActive = 1
)order by  ResourceName ,ProjectName 


update temp
set temp.WeeklyComments=tt.WeeklyComments
from 
	#TempTT temp
	join TimeTracker tt 
		on tt.ResourceId = temp.ResourceId
		and tt.RoleId = temp.RoleId 
		and tt.ProjectId = temp.ProjectId
where temp.PHCount<2
	--And (Year(tt.FromDate)=@Year or Year(tt.ToDate)=@Year)
	and tt.FromDate>=@dayfrom and tt.ToDate<=@dayto                   

SELECT EmployeeCode, NumberOfLeave	
INTO #TempEmployeelaeve	
FROM 
 (
	select EmployeeCode, ISNULL(
		(CASE WHEN LeaveFromDate != LeaveToDate THEN 
				(CASE WHEN LeaveFromDate BETWEEN @dayfrom AND @dayto AND LeaveToDate BETWEEN @dayfrom AND @dayto THEN  		
						DATEDIFF(day, LeaveFromDate, LeaveToDate) + 1
					when LeaveFromDate<@dayfrom and LeaveToDate>@dayto then --su-20170529 
						DATEDIFF(day, @dayfrom, DATEADD(dd, -2, DATEADD(wk, DATEDIFF(wk, 0, @dayfrom),6))) + 1 --su-20170529 
					WHEN LeaveFromDate BETWEEN @dayfrom AND @dayto THEN 
						DATEDIFF(day, LeaveFromDate, DATEADD(dd, -2, DATEADD(wk, DATEDIFF(wk, 0, LeaveFromDate),6))) + 1
					WHEN LeaveToDate BETWEEN @dayfrom AND @dayto THEN 
						DATEDIFF(day, DATEADD(wk,DATEDIFF(wk,0,LeaveToDate),0), LeaveToDate) + 1 
					END)
			ELSE ISNULL(Sum(el.NumberOfLeave),0)
			END), 0) NumberOfLeave
			,LeaveFromDate,LeaveToDate
			
			from EmployeeLeave el
			group by LeaveFromDate, LeaveToDate, el.EmployeeCode
		 ) as a  
where (LeaveFromDate between @dayfrom and @dayto) or  (LeaveToDate between @dayfrom and @dayto) or (LeaveFromDate<@dayfrom and LeaveToDate>@dayto)  --su-20170529
order by EmployeeCode
		 
update temp
set temp.Leave = 
(SELECT  ISNULL(Sum(el.NumberOfLeave),0)	
	FROM #TempEmployeelaeve el  where el.EmployeeCode = temp.EmpCode
	 )
from #TempTT temp
    
Declare csr1 cursor for 
	Select ResourceId,RoleId,ProjectId
	from #TempTT
	where PHCount>1

Open csr1
Fetch Next from csr1 into @ResourceId, @RoleId, @ProjectId
While @@Fetch_Status =0
begin 
	SET @WeeklyComments=''
	SET @WCTemp=''
	Declare csr2 Cursor for 
		select WeeklyComments
		from TimeTracker
		where ResourceId = @ResourceId
			and RoleId = @RoleId 
			and ProjectId = @ProjectId
			--And (Year(FromDate)=@Year or Year(ToDate)=@Year)
			and FromDate>=@dayfrom 
			and ToDate<=@dayto
	open csr2
	Fetch Next from csr2 into @WCTemp
	While @@Fetch_Status = 0
	begin
		SEt @WeeklyComments=@WeeklyComments+@WCTemp
		SET @WCTemp=''
		Fetch Next from csr2 into @WCTemp
		if @@Fetch_Status = 0 and (@WeeklyComments!='' and @WCTemp!='')
		begin
			SET @WeeklyComments=@WeeklyComments+'/ '
		end
	End
	Close csr2
	deallocate csr2	
	
	update #TempTT
	set WeeklyComments=@WeeklyComments
	where ResourceId = @ResourceId
		and RoleId = @RoleId 
		and ProjectId = @ProjectId
		and PHCount>1
	Fetch Next from csr1 into @ResourceId, @RoleId, @ProjectId
end
Close csr1
deallocate csr1

--vibin03/26/2018 to get details for a specific employee
if(@EmployeeCode is not null)
begin

	select *
	from #TempTT
	where EmpCode=@EmployeeCode

end

else

begin
select t1.*
from
(
	select *
	from #TempTT
) as t1 order by  ResourceName ,ProjectName                 
 end 
                       
END 





GO
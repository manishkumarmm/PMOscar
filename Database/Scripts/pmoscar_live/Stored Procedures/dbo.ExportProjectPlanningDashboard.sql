SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ExportProjectPlanningDashboard]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ExportProjectPlanningDashboard]
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ExportProjectPlanningDashboard]
	
@dayfrom varchar(15),
@dayto varchar(15),	
@uptodate varchar(15),
@status char(1)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
if(@status='1')
begin
Select distinct Project.ProjectName,sum(D.BudgetHours)BudgetHours,Sum(D.RevisedBudgetHours)RevisedBudgetHours, isnull(B.ActualHours ,0)+ isnull(sum(TimeTracker.ActualHours),0)
as SpentHours,sum(TimeTracker.EstimatedHours) As Allocated,sum(TimeTracker.ActualHours) As Actual
From  TimeTracker  inner join Project on Project.ProjectId = TimeTracker.ProjectId   left join
(Select sum(B.ActualHours)ActualHours,A.ProjectId from Project A inner join dbo.TimeTracker B    
on A.ProjectId=B.ProjectId      where   
B.FromDate<@uptodate group by A.ProjectId) as B  
on TimeTracker.ProjectId=B.ProjectId 
left Join ProjectEstimation D on D.ProjectId=TimeTracker.projectID
where  TimeTracker.FromDate>=@dayfrom and  TimeTracker.ToDate<=@dayto 
group by  B.ActualHours, ProjectName order by    Project.ProjectName 

end 

else if (@status='2')
begin


Select T.Role,ProjectName,SpentHours from
(
Select distinct Role.ShortName Role,Project.ProjectName, sum(TimeTracker.ActualHours) as SpentHours From  TimeTracker inner join Project on 
Project.ProjectId = TimeTracker.ProjectId 
inner join Role on Role.RoleId=TimeTracker.RoleId where 
TimeTracker.FromDate<=@uptodate  and Role.ShortName Not in('Dev','DBDEv')
group by  Role.ShortName,ProjectName 

union all

Select distinct 'DEV'  Role,Project.ProjectName, sum(TimeTracker.ActualHours) as SpentHours From  TimeTracker inner join Project on 
Project.ProjectId = TimeTracker.ProjectId 
inner join Role on Role.RoleId=TimeTracker.RoleId where 
TimeTracker.FromDate<=@uptodate  and Role.ShortName IN ('Dev','DBDEv')
group by  ProjectName 
) AS T
order by ProjectName

end 
	
else
begin

Select distinct B.ShortName as Role from TimeTracker A inner join Role B on A.RoleID=B.RoleID where B.ShortName<>'DBDev';

end 

	
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


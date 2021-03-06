IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ProjectDashboardEstimationOperations') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProjectDashboardEstimationOperations]
/****** Object:  StoredProcedure [dbo].[ProjectDashboardEstimationOperations]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ProjectDashboardEstimationOperations]
	-- Add the parameters for the stored procedure here
	@DashboardID int ,
	@ProjectID int,
	@PhaseID int,
	@EstimationRoleID int,
	@BillableHours int,
	@BudgetHours int,
	@RevisedBudgetHours int,
	@ActualHrs int,
	@PeriodEstimetedHrs int ,
	@PeriodActualHrs int,
	@PeriodEstimetedHrsAdj decimal(18,4) ,
	@PeriodActualHrsAdj decimal(18,4),
	@EstRoleName nvarchar(100),
	@EstRoleShrtName nvarchar(100),
	@RoleName nvarchar(100),
	@RoleShrtName nvarchar(100),
	@ActualHrsCorrected decimal(18,4),
	@RevisedComments nvarchar(max)
	
AS
BEGIN
   
  INSERT INTO [ProjectDashboardEstimation]
           ([ProjectID]
           ,[PhaseID]
           ,EstimationRoleID
           ,[BillableHours]
           ,[BudgetHours]
           ,[RevisedBudgetHours]
           ,[ActualHrs]
           ,[PeriodEstimetedHrs]
           ,[PeriodActualHrs]
           ,[PeriodEstimetedHrsAdjusted]
           ,PeriodActualHrsAdjusted
           ,EstRoleName
           ,EstRoleShrtName
           ,RoleName
           ,RoleShrtName
           ,[DashboardID]
           ,ActualHrsAdjusted
           ,RevisedComments)
     VALUES
           (@ProjectID
           ,@PhaseID
           ,@EstimationRoleID
           ,@BillableHours
           ,@BudgetHours
           ,@RevisedBudgetHours
           ,@ActualHrs
           ,@PeriodEstimetedHrs
           ,@PeriodActualHrs
           ,@PeriodEstimetedHrsAdj
           ,@PeriodActualHrsAdj
           ,@EstRoleName
           ,@EstRoleShrtName
           ,@RoleName
           ,@RoleShrtName
           ,@DashboardID
           ,@ActualHrsCorrected
           ,@RevisedComments)
	
END

GO

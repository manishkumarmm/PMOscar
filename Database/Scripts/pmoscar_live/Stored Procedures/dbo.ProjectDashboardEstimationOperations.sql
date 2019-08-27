SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProjectDashboardEstimationOperations]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProjectDashboardEstimationOperations]
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
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
	@ActualHrsCorrected decimal(18,4)
	
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
           ,ActualHrsAdjusted)
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
           ,@ActualHrsCorrected)

	
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


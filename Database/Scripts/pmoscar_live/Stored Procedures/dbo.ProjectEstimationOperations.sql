SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProjectEstimationOperations]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProjectEstimationOperations]
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[ProjectEstimationOperations]
	-- Add the parameters for the stored procedure here
@ProjectId int,  
@PhaseID int,  
@RoleID int,
@BillableHours int,  
@BudgetHours int,  
@RevisedBudgetHours int, 
@CreatedBy int,
@CreatedDate datetime,
@UpdatedBy int,
@UpdatedDate datetime,
@Status char(1),
@ProjectEstimationID int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

     if (@Status='I')      
       begin
         insert into dbo.ProjectEstimation(ProjectID,PhaseID,BillableHours,BudgetHours,RevisedBudgetHours,CreatedBy,
                                            CreatedDate,UpdatedBy,UpdatedDate,EstimationRoleID )
          values(@ProjectId,@PhaseID,@BillableHours,@BudgetHours,@RevisedBudgetHours,@CreatedBy,@CreatedDate,@UpdatedBy,@UpdatedDate,@RoleID);                                    
       end 
    else if  (@Status='U')
       begin
       
         update ProjectEstimation set PhaseID=@PhaseID,BillableHours=@BillableHours,BudgetHours=@BudgetHours,RevisedBudgetHours=@RevisedBudgetHours,UpdatedBy=@UpdatedBy,
                    UpdatedDate=@UpdatedDate,EstimationRoleID=@RoleID where ProjectEstimationID=@ProjectEstimationID;              
       end  
    else
       begin
        insert into  dbo.ProjectEstimationAudit
          select ProjectEstimationID,ProjectID,PhaseID,EstimationRoleID,BillableHours,BudgetHours,RevisedBudgetHours,CreatedBy,CreatedDate from ProjectEstimation  where ProjectID=@ProjectId;
  
        Delete from dbo.ProjectEstimation where ProjectEstimationID=@ProjectEstimationID;
       end  
       
   
END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


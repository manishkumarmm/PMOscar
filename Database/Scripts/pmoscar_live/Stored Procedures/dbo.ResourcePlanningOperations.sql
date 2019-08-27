SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ResourcePlanningOperations]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ResourcePlanningOperations]
GO


                
CREATE Procedure [dbo].[ResourcePlanningOperations]                    
(                 
                   
@TimeTrackerID int,                    
@Proj_Id int,                     
@Res_Id int,                    
@Role_Id int,                    
@Time_EHours int,                    
@Time_AHours int,                  
@Time_UpdatedBy Varchar(50),                  
@Time_UpdatedDate datetime,                  
@Time_FromDate datetime,                  
@Time_ToDate datetime,                
@Time_CreatedBy Varchar(50),                  
@Time_CreatedDate datetime,            
@PhaseId int,                
@OpMode varchar(10),  
      
@WeeklyComments varchar(500)                 
                    
                    
)                    
AS                    
                    
Begin                    
                    
     If @OpMode = 'INSERT'                
                  
   Begin     
     
    IF not exists(select ProjectEstimationID  from dbo.ProjectEstimation A   
                    inner Join EstimationRole C on A.EstimationRoleID=C.EstimationRoleID  
                    inner join [Role] B on A.EstimationRoleID=B.EstimationRoleID  
                   where A.PhaseID= @PhaseID and A.ProjectID=@Proj_Id and B.RoleID=@Role_Id)      
     begin  
       
      Declare @estRolID int ;  
      set @estRolID=(Select EstimationRoleID from Role Where RoleID=@Role_Id);  
        
      insert into ProjectEstimation(ProjectID,PhaseID,EstimationRoleID,BillableHours,BudgetHours,RevisedBudgetHours,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate)  
      values(@Proj_Id,@PhaseID,@estRolID,0,0,0,@Time_CreatedBy,@Time_CreatedDate,@Time_UpdatedBy,@Time_UpdatedDate);  
       
     end   
                
    Insert into TimeTracker                    
    (                    
                    
     ProjectId,                    
     ResourceId,                    
     RoleId,                    
     EstimatedHours,                    
     ActualHours,                  
     UpdatedBy,                  
     UpdatedDate,                 
     FromDate,                  
     ToDate,                
     CreatedBy,                
     CreatedDate,            
     PhaseID,      
     WeeklyComments           
                 
    )                     
    Values                    
    (                    
                    
     @Proj_Id,                    
     @Res_Id,                    
     @Role_Id,                    
     @Time_EHours,                    
     @Time_AHours,                  
     @Time_UpdatedBy,                  
     @Time_UpdatedDate,                 
     @Time_FromDate,                  
     @Time_ToDate ,                
     @Time_CreatedBy,                
     @Time_CreatedDate,            
     @PhaseID,      
     @WeeklyComments  
                  
                
    )                    
    return SCOPE_IDENTITY()      
                    
   End                
                   
   If @OpMode = 'UPDATE'                
                  
   Begin                
                   
    Update  TimeTracker                      
                    
    Set                    
                      
     EstimatedHours = @Time_EHours,                      
     ActualHours = @Time_AHours,                    
     UpdatedBy = @Time_UpdatedBy,                  
     UpdatedDate = @Time_UpdatedDate,                  
     FromDate = @Time_FromDate,                  
     ToDate  = @Time_ToDate,          
     PhaseID = @PhaseID,      
     WeeklyComments = @WeeklyComments,               
     RoleId= @Role_Id,
     ResourceId = @Res_Id ,
  ProjectId = @Proj_Id
     
                      
    Where                  
   
      TimeTrackerId=@TimeTrackerID           
      return    @TimeTrackerID      
   End                
                   
   If @OpMode = 'DELETE'                
                  
   Begin                
                   
    DELETE FROM TimeTracker                 
    WHERE TimeTrackerId=@TimeTrackerID                
                  
   End                
End

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ResourcePlanningOperations') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ResourcePlanningOperations]
/****** Object:  StoredProcedure [dbo].[ResourcePlanningOperations]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                    
CREATE PROCEDURE [dbo].[ResourcePlanningOperations]                        
(                     
                       
@TimeTrackerID int,                        
@Proj_Id int,                         
@Res_Id int,                        
@Role_Id int,                        
@Time_EHours int,                        
@Time_AHours Decimal(18,2),                      
@Time_UpdatedBy Varchar(50),                      
@Time_UpdatedDate datetime,                      
@Time_FromDate datetime,                      
@Time_ToDate datetime,                    
@Time_CreatedBy Varchar(50),                      
@Time_CreatedDate datetime,                
@PhaseId int,                    
@OpMode varchar(10),     
@ActualHours_Updated bit, 
          
@WeeklyComments varchar(500),                     
                        
 @TeamId int      
     
                        
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
     WeeklyComments,
     TeamID,
     ActHrsUpdated  
                     
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
     @WeeklyComments,
     @TeamId,       
     @ActualHours_Updated         
                    
    )                        
    return SCOPE_IDENTITY()          
                        
   End                    
                       
   If @OpMode = 'UPDATE'                    
                      
   Begin       
       
   IF not exists(select ProjectEstimationID  from dbo.ProjectEstimation A       
                    inner Join EstimationRole C on A.EstimationRoleID=C.EstimationRoleID      
                    inner join [Role] B on A.EstimationRoleID=B.EstimationRoleID      
                   where A.PhaseID= @PhaseID and A.ProjectID=@Proj_Id and B.RoleID=@Role_Id)          
     begin      
           
      Declare @estRolID1 int ;      
      set @estRolID1=(Select EstimationRoleID from Role Where RoleID=@Role_Id);      
            
      insert into ProjectEstimation(ProjectID,PhaseID,EstimationRoleID,BillableHours,BudgetHours,RevisedBudgetHours,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate)      
      values(@Proj_Id,@PhaseID,@estRolID1,0,0,0,@Time_CreatedBy,@Time_CreatedDate,@Time_UpdatedBy,@Time_UpdatedDate);      
           
     end             
                       
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
  ProjectId = @Proj_Id,    
    TeamID=@TeamId,
	ActHrsUpdated=@ActualHours_Updated          
                          
    Where                      
       
      TimeTrackerId=@TimeTrackerID               
      return    @TimeTrackerID          
   End                    
                       
   If @OpMode = 'DELETE'                    
                      
   Begin       
       
       
    insert into  dbo.TimeTrackerAudit (TimeTrackerID,OldProjectID,ResourceID,RoleID,OldPhaseID,FromDate,ToDate,OldEstimatedHours,OldActualHours,CreatedBy,CreatedDate,WeeklyComments,UpdatedBy,UpdatedDate,TeamID,NewEstimatedHours,NewActualHours,[Action],NewPhaseID,NewProjectID)   
          select TimeTrackerID,ProjectID as OldProjectID,ResourceID,RoleID,PhaseID as OldPhaseID,FromDate,ToDate,EstimatedHours as OldEstimatedHours,ActualHours as OldActualHours, CreatedBy,CreatedDate,WeeklyComments,@Time_UpdatedBy as UpdatedBy,UpdatedDate,TeamID,NULL as NewEstimatedHours,NULL as NewActualHours,'Delete' as [Action],PhaseID as NewPhaseID,ProjectID as NewProjectID from dbo.TimeTracker  where TimeTrackerId=@TimeTrackerID;           
                   
                       
    DELETE FROM TimeTracker                     
    WHERE TimeTrackerId=@TimeTrackerID                    
                      
   End                    
End
GO

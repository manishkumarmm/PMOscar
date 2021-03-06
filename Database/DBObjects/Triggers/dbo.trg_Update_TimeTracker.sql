
/****** Object:  Trigger [dbo].[trg_Update_TimeTracker]    Script Date: 6/5/2018 2:19:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'trg_Update_TimeTracker') AND [type]='TR')
DROP TRIGGER [dbo].[trg_Update_TimeTracker]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==============================================================================
-- Description: Track Sync Process

-- ==============================================================================
-- Author:  
-- Create date: 2018-04-19
--Modified By : Haritha E.S
--Modified On : 2018-06-05
-- ==============================================================================

CREATE  TRIGGER [dbo].[trg_Update_TimeTracker] ON     
  [dbo].[TimeTracker]     
FOR UPDATE         
AS        
BEGIN        
       
IF((SELECT COUNT(*) FROM deleted )>1)
 
 BEGIN
 INSERT INTO dbo.TimeTrackerAudit([TimeTrackerID]
      ,[OldProjectID]
	  ,[NewProjectID]
      ,[ResourceID]
      ,[RoleID]
      ,[OldPhaseID]
	  ,[NewPhaseID]
      ,[FromDate]
      ,[ToDate]
      ,[OldEstimatedHours]
      ,[OldActualHours]
	  ,[NewEstimatedHours]
      ,[NewActualHours]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[WeeklyComments]
      ,[UpdatedBy]
      ,[UpdatedDate]
      ,[TeamID],[Action])    
 SELECT D.TimeTrackerID
      ,D.ProjectID
	  ,N.ProjectId
      ,D.ResourceID
      ,D.RoleID
      ,D.PhaseID
	  ,N.PhaseID
      ,D.FromDate
      ,D.ToDate
      ,D.EstimatedHours
      ,D.ActualHours
	  ,N.EstimatedHours
	  ,N.ActualHours
      ,D.CreatedBy
      ,D.CreatedDate
      ,D.WeeklyComments
      ,N.UpdatedBy
      ,GETDATE()
      ,D.TeamID,'Edit' FROM deleted D join inserted N on N.TimeTrackerId=D.TimeTrackerId 
where (D.ActualHours <> N.ActualHours ) or( D.PhaseID<>N.PhaseID) or (D.ProjectId<>N.ProjectId);


 END
ELSE
 
BEGIN     
 DECLARE @TimeTrackerId INT;        
 DECLARE @OldProjectID INT; 
 DECLARE @NewProjectID INT;        
 DECLARE @RoleID INT;        
 DECLARE @ResourceId INT;        
 DECLARE @FromDate DATETIME;        
 DECLARE @ToDate DATETIME;        
 DECLARE @OldEstimatedHours INT;     
 DECLARE @OldActualHours decimal(18,2)  
 DECLARE @NewEstimatedHours INT;     
 DECLARE @NewActualHours decimal(18,2)   
 DECLARE @OldPhaseID INT     
 DECLARE @NewPhaseID INT
 DECLARE @CreatedBy INT;        
 DECLARE @CreatedDate DATETIME;    
 DECLARE @Comments NVARCHAR(1000); 
 DECLARE @UpdatedBy INT;
 DECLARE @UpdatedDate DATETIME;     
 DECLARE @TeamID INT;  
 DECLARE @Action NVARCHAR(1000);    
if (select count(1) from inserted)=1
 BEGIN
         
SELECT @TimeTrackerId =D.TimeTrackerId , @OldProjectID = D.ProjectId, @RoleID =D.RoleId, @OldPhaseID = D.PhaseID,@ResourceId=D.ResourceId,    
 @FromDate=D.FromDate,@ToDate=D.ToDate ,@OldEstimatedHours = D.EstimatedHours,@OldActualHours =D.ActualHours,@NewEstimatedHours = N.EstimatedHours,@NewActualHours =N.ActualHours,@CreatedBy=D.CreatedBy,@CreatedDate=D.CreatedDate,  
 @Comments=D.WeeklyComments,@UpdatedBy=N.UpdatedBy,@UpdatedDate= D.UpdatedDate,@TeamID=D.TeamID,@Action='Edit',@NewPhaseID = N.PhaseID,@NewProjectID=N.ProjectId  FROM deleted D ,inserted N WHERE N.TimeTrackerId=D.TimeTrackerId 
or D.ActualHours <> N.ActualHours or  D.EstimatedHours <> N.EstimatedHours   or D.PhaseID<>N.PhaseID  or D.ProjectId<>N.ProjectId;   

 IF @TimeTrackerId!=0    
 BEGIN        
          
  INSERT INTO dbo.TimeTrackerAudit (TimeTrackerId,OldProjectID,NewProjectID,RoleID,OldPhaseID,NewPhaseID,ResourceId,FromDate,ToDate,OldEstimatedHours,OldActualHours,NewEstimatedHours,NewActualHours,CreatedBy,CreatedDate,WeeklyComments,UpdatedBy,UpdatedDate,TeamID,[Action])    
  VALUES(@TimeTrackerId,@OldProjectID,@NewProjectID,@RoleID,@OldPhaseID,@NewPhaseID,@ResourceId,@FromDate,@ToDate,@OldEstimatedHours,@OldActualHours,@NewEstimatedHours,@NewActualHours,@CreatedBy,@CreatedDate,@Comments,
  @UpdatedBy,@UpdatedDate,@TeamID,@Action);          
      
  END  
  END  
  ELSE
  BEGIN
  SELECT @TimeTrackerId =D.TimeTrackerId , @OldProjectID = D.ProjectId, @RoleID =D.RoleId, @OldPhaseID = D.PhaseID,@ResourceId=D.ResourceId,    
 @FromDate=D.FromDate,@ToDate=D.ToDate ,@OldEstimatedHours = D.EstimatedHours,@OldActualHours =D.ActualHours,@NewEstimatedHours = N.EstimatedHours,@NewActualHours =N.ActualHours,@CreatedBy=D.CreatedBy,@CreatedDate=D.CreatedDate,  
 @Comments=D.WeeklyComments,@UpdatedBy=N.UpdatedBy,@UpdatedDate= D.UpdatedDate,@TeamID=D.TeamID,@Action='Edit',@NewPhaseID = N.PhaseID,@NewProjectID=N.ProjectId  FROM deleted D ,inserted N WHERE N.TimeTrackerId=D.TimeTrackerId 
or D.ActualHours <> N.ActualHours or  D.EstimatedHours <> N.EstimatedHours  or D.PhaseID<>N.PhaseID or D.ProjectId<>N.ProjectId;        

 IF @TimeTrackerId!=0    
    BEGIN        
          
  INSERT INTO dbo.TimeTrackerAudit (TimeTrackerId,OldProjectID,NewProjectID,RoleID,OldPhaseID,NewPhaseID,ResourceId,FromDate,ToDate,OldEstimatedHours,OldActualHours,NewEstimatedHours,NewActualHours,CreatedBy,CreatedDate,WeeklyComments,UpdatedBy,UpdatedDate,TeamID,[Action])    
  VALUES(@TimeTrackerId,@OldProjectID,@NewProjectID,@RoleID,@OldPhaseID,@NewPhaseID,@ResourceId,@FromDate,@ToDate,@OldEstimatedHours,@OldActualHours,@NewEstimatedHours,@NewActualHours,@CreatedBy,@CreatedDate,@Comments,
  @UpdatedBy,@UpdatedDate,@TeamID,@Action);          
      
   END 
  END 
 END        
         
END

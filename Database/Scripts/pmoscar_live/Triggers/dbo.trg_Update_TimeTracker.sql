SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[trg_Update_TimeTracker]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[trg_Update_TimeTracker]
GO

CREATE  trigger trg_Update_TimeTracker on   
  TimeTracker   
for update       
as      
begin      
       
 declare @TimeTrackerId int;      
 declare @ProjectID int;      
 declare @RoleID int;      
 declare @ResourceId int;      
 declare @FromDate datetime;      
 declare @ToDate datetime;      
 declare @EstimatedHours int;   
 declare @ActualHours int  
 declare @PhaseID int   
 declare @CreatedBy int;      
 declare @CreatedDate datetime;  
 declare @Comments nvarchar(1000);    
       
 select @TimeTrackerId =TimeTrackerId , @ProjectID = ProjectId, @RoleID =RoleId, @PhaseID = PhaseID,@ResourceId=ResourceId,  
 @FromDate=FromDate,@ToDate=ToDate ,@EstimatedHours = EstimatedHours,@ActualHours =ActualHours,@CreatedBy=CreatedBy,@CreatedDate=CreatedDate,
 @Comments=WeeklyComments   from deleted;      
       
 if @TimeTrackerId!=0  
 begin      
        
  insert into dbo.TimeTrackerAudit (TimeTrackerId,ProjectID,RoleID,PhaseID,ResourceId,FromDate,ToDate,EstimatedHours,ActualHours,CreatedBy,CreatedDate,WeeklyComments)  
  values(@TimeTrackerId,@ProjectID,@RoleID,@PhaseID,@ResourceId,@FromDate,@ToDate,@EstimatedHours,@ActualHours,@CreatedBy,@CreatedDate,@Comments);        
    
        
 end      
       
end
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


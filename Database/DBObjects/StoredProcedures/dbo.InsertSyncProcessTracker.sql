

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'InsertSyncProcessTracker') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertSyncProcessTracker]
GO


/****** Object:  StoredProcedure [dbo].[ResourcePlanningOperations]    Script Date: 3/29/2018 4:56:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- ==============================================================================
-- Description: Track Sync Process

-- ==============================================================================
-- Author: Fathima  
-- Create date: 2018-04-19
--Modified By : Aswathy
--Modified On : 2018-04-23
-- ==============================================================================

                    
CREATE PROCEDURE [dbo].[InsertSyncProcessTracker]                        
                     
                     
@UserId int,                 
@IsUpdate bit=0 ,
@Error varchar(100)=NULL    
                                  
AS                        
              
Begin                        
                        
    if (@ISUpdate=0)                  
                      
   Begin         
      insert into SyncProcessTracker([Startdate],[enddate],[SyncUserId],[Status])      
      values(getdate(),getdate(),@UserId,'InProgress');      
           
     end       
                         
            
   if (@ISUpdate=1 and @Error is null)                     
      Begin                

    Update  SyncProcessTracker                                                
    Set    [status]='Success' ,enddate=getdate()
    Where  SyncProcessTrackerId =(select max( SyncProcessTrackerId) from SyncProcessTracker) and Status='InProgress'                
       
      
   End    
   
   if (@ISUpdate=1 and @Error is not null)                     
      Begin                

    Update  SyncProcessTracker                                                
    Set    [status]='Failed' ,enddate=getdate()
	,  ErrorDescription  =@Error                          
    Where  SyncProcessTrackerId =(select max( SyncProcessTrackerId) from SyncProcessTracker) and Status='InProgress'                
       
      
   End                    
    
End
GO



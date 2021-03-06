IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetTeamUtilizationByPeriod') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetTeamUtilizationByPeriod]
/****** Object:  StoredProcedure [dbo].[GetTeamUtilizationByPeriod]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
-- =============================================          
-- Author:  Abid          
-- Create date: 26 April 2012          
-- Description: Procedure to get project details by ResourceId          
-- =============================================          
      
CREATE PROCEDURE [dbo].[GetTeamUtilizationByPeriod]          
       
        
 @FromDate datetime,          
 @ToDate datetime         
        
           
           
AS          
BEGIN          
 -- SET NOCOUNT ON added to prevent extra result sets from          
 -- interfering with SELECT statements.          
 SET NOCOUNT ON;          
    
select 
tm.Team,rs.ResourceName,tt.EstimatedHours as [PlannedHours],
isnull(tt.ActualHours,0) as [ActualHours]from Resource rs 
join TimeTracker tt on rs.ResourceId=tt.ResourceId
join Team tm on tm.TeamId=tt.TeamID
join Project pr on pr.ProjectId=tt.ProjectId

where
(tt.FromDate)>=@FromDate and (tt.ToDate)<=@ToDate
 
and tt.PhaseID not in(1,6) --VAS and Proposal excludes here

and pr.ProjectName Not In('Admin','Open')

group by 
 
 tt.ResourceId,tt.ProjectId,tt.FromDate,tt.ToDate,tt.ActualHours,tt.EstimatedHours,
rs.ResourceName,rs.ResourceId,tm.Team

order by rs.ResourceName
    
    
 END
    
    

GO

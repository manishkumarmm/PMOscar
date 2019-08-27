SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetProjectDetailsForProject]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetProjectDetailsForProject]
GO


  
-- =============================================      
-- Author:  Sajid      
-- Create date: 16 Feb 2011      
-- Description: Procedure to get project details by ResourceId      
-- =============================================      
  
CREATE PROCEDURE [dbo].[GetProjectDetailsForProject]      
   
 @ProjectID int,      
 @Year int,      
 @Month int,      
 @dayfrom varchar(15),      
 @dayto varchar(15)      
       
       
AS      
BEGIN      
 -- SET NOCOUNT ON added to prevent extra result sets from      
 -- interfering with SELECT statements.      
 SET NOCOUNT ON;      
      
    SELECT TT.TimeTrackerId AS TimeTrackerId,RS.ResourceId,RS.ResourceName,R.Role,TT.EstimatedHours As EstimatedHours,TT.ActualHours As ActualHours,PH.Phase       
    FROM TimeTracker TT      
    INNER JOIN  Resource RS on TT.ResourceId = RS.ResourceId      
    INNER JOIN Role R  on TT.RoleId = R.RoleId       
    INNER JOIN Project P on P.ProjectId = TT.ProjectId     
    Left JOIN Phase PH on TT.PhaseID = PH.PhaseID     
    WHERE P.ProjectId = @ProjectID  and (Year(TT.FromDate)=@Year or Year(TT.ToDate)=@Year)      
        
    and TT.FromDate>=@dayfrom and  TT.ToDate<=@dayto      
END 

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


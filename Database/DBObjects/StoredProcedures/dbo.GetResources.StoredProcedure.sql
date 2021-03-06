IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetResources') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetResources]
/****** Object:  StoredProcedure [dbo].[GetResources]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ==============================================================================
-- Description: Get Resources
-- ==============================================================================
-- Modified By : Joshwa
-- Modified date : 2018/07/11
-- ==============================================================================
    
CREATE Procedure [dbo].[GetResources]  
  
 @dayfrom varchar(15),          
 @dayto varchar(15)   
   
AS    
    
Begin    
      
Select resourcename,resourceid 
from resource r 
where IsActive = 1
union all
select distinct ResourceName,R.ResourceId 
from Resource R join TimeTracker tt ON R.ResourceId = tt.ResourceId 
where  tt.ResourceId = r.ResourceId  
and (Year(TT.FromDate)= Year(@dayfrom) or Year(TT.ToDate) = Year(@dayto))                   
and TT.FromDate>=@dayfrom and  TT.ToDate<=@dayto  and IsActive = 0 order by ResourceName
    
End 
GO

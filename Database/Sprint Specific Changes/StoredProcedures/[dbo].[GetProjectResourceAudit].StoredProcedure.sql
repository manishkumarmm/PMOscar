IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetProjectResourceAudit') AND type in (N'P', N'PC'))
DROP Procedure [dbo].[GetProjectResourceAudit]
/****** Object:  StoredProcedure [dbo].[GetProjectResourceAudit]    Script Date: 2018-05-21 16:01:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

                      
CREATE PROCEDURE [dbo].[GetProjectResourceAudit]                          
                       
 @ProjectID int                          
 --@Year int                      
                        
AS                          
BEGIN                          
           
Select distinct u.FirstName as 'Modified By',
CONVERT(varchar,ModifiedDate, 103) + ' ' + 
        CONVERT(varchar, DATEPART(hh, ModifiedDate)) + ':' + 
        RIGHT('0' + CONVERT(varchar, DATEPART(mi, ModifiedDate)), 2) + ':' + RIGHT('0' + CONVERT(varchar, DATEPART(SS, ModifiedDate)), 2)  As 'Modified Date',
ResourceName As 'Resource Name', 
case when IsAllocated = 0 then 'Deallocated' else 'Allocated' end AS 'Allocation Status'
from ProjectResourcesAudit pr 
inner join [User] u on u.UserId = pr.ModifiedBy
inner join [Resource] r on r.ResourceId = pr.ResourceID
where pr.ProjectID = @ProjectID
order by 'Modified Date' desc
                                       
END 
GO



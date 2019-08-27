

/****** Object:  View [dbo].[vw_ResourceUtilizationPercentageSrForPowerBI]    Script Date: 07/28/2019 7:59:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

alter view  [dbo].[vw_SrResourcesForPowerBI]
as
select distinct r.emp_Code Employeecode
 from  resource r --on r.ResourceId=RE.ResourceID
 where r.emp_Code is not null 
 
		AND r.ResourceId not in ( select resourceid 
								from  [dbo].[ResourceUtilizationPercentage] RE
							 where re.AdjustmentFactor=0 
							 group by resourceid
							 having count(1)>1)
GO



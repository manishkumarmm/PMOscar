
/****** Object:  View [dbo].[vw_ResourceUtilizationPercentageTraineesForPowerBI]    Script Date: 07/28/2019 8:00:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

alter view  [dbo].[vw_ResourceUtilizationPercentageTraineesForPowerBI]
as
select *
 from [dbo].[ResourceUtilizationPercentage]
 where  ResourceId  in ( select resourceid 
								from  [dbo].[ResourceUtilizationPercentage] RE
							 where re.AdjustmentFactor=0 
							 group by resourceid
							 having count(1)>1)
GO



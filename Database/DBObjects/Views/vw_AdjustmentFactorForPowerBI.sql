
/****** Object:  View [dbo].[vw_ResourceUtilizationPercentageTraineesForPowerBI]    Script Date: 07/28/2019 8:00:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

alter view  [dbo].[vw_AdjustmentFactorForPowerBI]
as
select R.emp_Code as EmployeeCode,AdjustmentFactor,RS.UtilizationPercentage
 from [dbo].[ResourceUtilizationPercentage] RS
 JOIN Resource R on R.ResourceId=RS.ResourceID
 where  AdjustmentFactor<>0 
							
GO



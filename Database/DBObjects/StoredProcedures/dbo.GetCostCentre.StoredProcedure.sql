IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetCostCentre') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCostCentre]
/****** Object:  StoredProcedure [dbo].[GetCostCentre]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================          
-- Author:  Anila          
-- Create date: 28 January 2015          
-- Description: Procedure to get cost centre details         
-- =============================================          
CREATE PROCEDURE [dbo].[GetCostCentre]

AS
BEGIN
	
	SELECT CostCentreID,CostCentre  from CostCentre order by CostCentre
END


GO

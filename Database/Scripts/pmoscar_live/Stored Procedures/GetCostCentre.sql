SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetCostCentre]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetCostCentre]
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



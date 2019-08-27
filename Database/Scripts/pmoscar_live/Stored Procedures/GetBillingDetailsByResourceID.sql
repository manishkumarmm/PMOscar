
/****** Object:  StoredProcedure [dbo].[GetBillingDetailsByResourceID]    Script Date: 09/19/2012 10:21:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBillingDetailsByResourceID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetBillingDetailsByResourceID]
GO


/****** Object:  StoredProcedure [dbo].[GetBillingDetailsByResourceID]    Script Date: 09/19/2012 10:21:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Dona
-- Create date: 8-5-2012
-- Description:	procedure to get team details for a resource
-- =============================================
-- display the billing details for a particular project

CREATE PROCEDURE [dbo].[GetBillingDetailsByResourceID]
	-- Add the parameters for the stored procedure here
	
	@ResourceID int,
	@RoleId int,
	@Project int,
	@month int,
	@year int
	
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 select bd.BillingID,rs.ResourceName,bd.ResourceId,p.ProjectId,bd.RoleId,r.[Role],bd.PlannedHours,bd.ActualHours,CONVERT( Varchar(12), bd.fromDate,103) as fromDate ,CONVERT( Varchar(12), bd.ToDate,103) as ToDate,bd.UBT,bd.ActualSpentHours,bd.Comments
  FROM BillingDetails bd
  INNER JOIN [Resource] rs ON rs.ResourceId =bd.ResourceId
  INNER JOIN [Role] r ON r.RoleId = bd.RoleId
  INNER JOIN [Project] p ON p.ProjectId = bd.ProjectID
  where rs.ResourceId=bd.ResourceId AND r.RoleId=bd.RoleId AND bd.ProjectID =@Project AND DATEPART(month,fromDate)=@month AND DATEPART(year,fromDate)=@year

END





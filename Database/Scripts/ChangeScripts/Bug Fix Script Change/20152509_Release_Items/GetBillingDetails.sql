
/****** Object:  StoredProcedure [dbo].[GetBillingDetails]    Script Date: 09/19/2012 10:17:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBillingDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetBillingDetails]
GO


/****** Object:  StoredProcedure [dbo].[GetBillingDetails]    Script Date: 09/19/2012 10:17:27 ******/
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

	CREATE  PROCEDURE [dbo].[GetBillingDetails]
	-- Add the parameters for the stored procedure here
	
 @editid int,
	@BillingID int,
	@projectId int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select r.[Role],CONVERT( Varchar(12), bd.fromDate,103) as fromDate ,CONVERT( Varchar(12), bd.ToDate,103) as ToDate,bd.PlannedHours,bd.ActualHours,rs.ResourceName,bd.ResourceId,p.ProjectName,p.ProjectId,bd.RoleId,bd.UBT,bd.ActualSpentHours,bd.Comments FROM BillingDetails bd INNER JOIN [Resource] rs ON rs.ResourceId =bd.ResourceId  INNER JOIN [Role] r ON r.RoleId = bd.RoleId INNER JOIN Project p on p.ProjectId = bd.ProjectID where bd.ResourceID =@editid and bd.ProjectID = @projectId and BillingID =@BillingID

END
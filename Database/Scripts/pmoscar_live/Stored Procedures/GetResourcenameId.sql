
/****** Object:  StoredProcedure [dbo].[GetResourcenameId]    Script Date: 09/19/2012 10:16:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetResourcenameId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetResourcenameId]
GO


/****** Object:  StoredProcedure [dbo].[GetResourcenameId]    Script Date: 09/19/2012 10:16:42 ******/
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

	CREATE  PROCEDURE [dbo].[GetResourcenameId]
	-- Add the parameters for the stored procedure here
	
 @editid int
	
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select ResourceName,ResourceId FROM Resource where ResourceId = @editid
END
GO



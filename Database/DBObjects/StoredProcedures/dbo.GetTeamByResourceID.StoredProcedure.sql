IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetTeamByResourceID') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetTeamByResourceID]
/****** Object:  StoredProcedure [dbo].[GetTeamByResourceID]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Abid
-- Create date: 8-5-2012
-- Description:	procedure to get team details for a resource
-- =============================================
CREATE PROCEDURE [dbo].[GetTeamByResourceID]
	-- Add the parameters for the stored procedure here
	
	@ResourceID int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   select t.Team,t.TeamID 
   from dbo.Resource r inner join dbo.Team t on r.TeamID=t.TeamID
    where r.ResourceId=@ResourceID

END
GO

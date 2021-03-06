IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetTeam') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetTeam]
/****** Object:  StoredProcedure [dbo].[GetTeam]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================          
-- Author:  Abid          
-- Create date: 4 May 2012          
-- Description: Procedure to get team details         
-- =============================================          
CREATE PROCEDURE [dbo].[GetTeam]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TeamID,Team,[Description]   from Team order by Team
END
GO

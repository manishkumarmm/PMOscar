IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetProjectById') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetProjectById]
/****** Object:  StoredProcedure [dbo].[GetProjectById]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetProjectById]
	-- Add the parameters for the stored procedure here
@ProjectID int 	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    
    Select ProjectId,ProjectName from Project where ProjectId =@ProjectID and IsActive = 0

END
GO

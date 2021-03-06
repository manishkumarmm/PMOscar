IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'CheckProjectDuplication') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CheckProjectDuplication]
/****** Object:  StoredProcedure [dbo].[CheckProjectDuplication]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckProjectDuplication] 
	-- Add the parameters for the stored procedure here
	@ProjectName varchar(50),
	@ProjectShortName varchar(50),
	@Status int out
AS
	BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;
		SET @Status=0
		
		IF  EXISTS(SELECT 1 FROM dbo.Project WHERE ProjectName=@ProjectName)
			SET @Status = 1
		ELSE IF EXISTS(SELECT 1 FROM dbo.Project WHERE ShortName=@ProjectShortName)
			SET @Status = 2
		
	END
GO

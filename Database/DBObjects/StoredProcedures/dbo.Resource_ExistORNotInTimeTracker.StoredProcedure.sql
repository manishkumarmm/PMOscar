IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Resource_ExistORNotInTimeTracker') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Resource_ExistORNotInTimeTracker]
/****** Object:  StoredProcedure [dbo].[Resource_ExistORNotInTimeTracker]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--EXEC Resource_ExistORNotInTimeTracker 219,2014,1,48,1
CREATE PROCEDURE [dbo].[Resource_ExistORNotInTimeTracker]
@ProjectId INT,
@Year INT,          
@Month INT,
@ResourceId INT,
@RoleId INT

AS
	DECLARE @returnValue INT
	SET @returnValue  = 0
	
BEGIN	
	IF EXISTS (
				SELECT 1 
				FROM TimeTracker tt
				INNER JOIN [Resource] rs ON tt.ResourceId = rs.ResourceId
				WHERE tt.ProjectId = @ProjectId 
				AND YEAR(tt.FromDate) = @Year 
				AND MONTH(tt.FromDate) = @Month 
				AND YEAR(tt.ToDate) = @Year 
				AND MONTH(tt.ToDate) = @Month 
				AND tt.ResourceId = @ResourceId 
				AND tt.RoleId = @RoleId 
				AND convert (datetime, convert(varchar(4), @Year)+'-'+Convert(varchar(2),@Month)+'-01') >= rs.BillingStartDate
				--AND @Month >= MONTH(rs.BillingStartDate)
			)
	BEGIN
	SET @returnValue = 1
	END
	SELECT @returnValue	AS ResourceExists		
END
GO

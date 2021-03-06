IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetResourceById') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetResourceById]
/****** Object:  StoredProcedure [dbo].[GetResourceById]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
   
CREATE PROCEDURE [dbo].[GetResourceById]
@ResourceID INT
AS
BEGIN
	SELECT ResourceId,ResourceName,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,IsActive,RoleId,TeamId,BillingStartDate 
	FROM Resource  
	WHERE ResourceId = @ResourceID 
END
GO

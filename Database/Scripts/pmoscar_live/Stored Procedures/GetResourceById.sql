IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetResourceById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetResourceById]
GO
   
CREATE PROCEDURE [dbo].[GetResourceById]
@ResourceID INT
AS
BEGIN
	SELECT ResourceId,ResourceName,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,IsActive,RoleId,TeamId,BillingStartDate 
	FROM Resource  
	WHERE ResourceId = @ResourceID 
END
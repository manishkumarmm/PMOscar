IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'CheckUserAuthentication') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CheckUserAuthentication]
/****** Object:  StoredProcedure [dbo].[CheckUserAuthentication]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sajid
-- Create date: 11/05/2012
-- Description:	Procedure to check user authentication by UserName and Password
-- =============================================
CREATE PROCEDURE [dbo].[CheckUserAuthentication] 
	@UserName varchar(50),
	@Password varchar(50)
AS
BEGIN

	SET NOCOUNT ON;

    SELECT UserID,FirstName,MiddleName,LastName,UserName,[Password],EmailId,U.UserRoleID,UR.UserRole,IsActive 
    FROM [User] U
    INNER JOIN dbo.UserRole UR ON U.UserRoleID = UR.UserRoleID
    WHERE UserName = @UserName AND [Password] = @Password
    
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ResetPassword') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ResetPassword]
/****** Object:  StoredProcedure [dbo].[ResetPassword]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ResetPassword]
	(
	    @UserId int,
	    @OldPassword varchar(50),
	    @NewPassword varchar(50),
	    @status varchar(50) out
	)
AS

Begin
  if exists(select * from [User] where UserID=@UserId and Password=@OldPassword)
  Begin
	UPDATE [User]
	SET
	   Password=@NewPassword
	WHERE
	    UserID=@UserId
	    
	   set @status=1
	   return;
	 End
	Else
	   set @status=0
	   return;
	 End
GO

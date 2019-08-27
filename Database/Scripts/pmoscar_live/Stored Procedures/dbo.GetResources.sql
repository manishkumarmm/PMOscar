SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetResources]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetResources]
GO



CREATE Procedure [dbo].[GetResources]
(

@Res_Id int

)
AS

Begin

Select * From Resource  where IsActive=1 order by ResourceName

End

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


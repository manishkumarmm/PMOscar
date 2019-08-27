
/****** Object:  StoredProcedure [dbo].[GetAllResource]    Script Date: 09/19/2012 10:23:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllResource]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllResource]
GO


/****** Object:  StoredProcedure [dbo].[GetAllResource]    Script Date: 09/19/2012 10:23:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[GetAllResource]      
     
AS      
      
Begin      
      
select ResourceId,ResourceName from Resource order by ResourceName
      
End 

GO



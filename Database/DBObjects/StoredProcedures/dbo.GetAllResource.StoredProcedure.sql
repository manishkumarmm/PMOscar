IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetAllResource') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllResource]

GO
/****** Object:  StoredProcedure [dbo].[GetAllResource]    Script Date: 4/19/2018 1:02:16 PM
Modified By: Joshwa George
Modified On:06/06/2018 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetAllResource]      
     
AS      
      
Begin      
      
select ResourceId,ResourceName from Resource where IsActive = 1 order by ResourceName
      
End
GO

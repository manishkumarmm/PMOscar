/****** Object:  StoredProcedure [dbo].[GetAllProjects]    Script Date: 09/19/2012 10:23:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllProjects]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllProjects]
GO


/****** Object:  StoredProcedure [dbo].[GetAllProjects]    Script Date: 09/19/2012 10:23:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[GetAllProjects]      
     
AS      
      
Begin      
      
select ProjectId,ProjectName from Project
	where ProjectName NOT IN ('Admin','Proposal')
 order by ProjectName
      
End 

GO


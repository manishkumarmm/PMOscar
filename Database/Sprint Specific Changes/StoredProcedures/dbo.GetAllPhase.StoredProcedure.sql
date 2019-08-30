IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetAllPhase') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllPhase]

GO
/****** Object:  StoredProcedure [dbo].[GetAllPhase]    Script Date: 4/19/2018 1:02:16 PM
Modified By: Joshwa George
Modified On:06/06/2018 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetAllPhase]      
     
AS      
      
Begin      
      
select PhaseId,Phase from Phase order by Phase
      
End
GO

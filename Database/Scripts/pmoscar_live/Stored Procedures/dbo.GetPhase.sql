SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetPhase]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetPhase]
GO


CREATE Procedure [dbo].[GetPhase]      
(      
      
@Role_Id int      
      
)      
AS      
      
Begin      
      
Select * From Phase Order By PhaseID
      
End 

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


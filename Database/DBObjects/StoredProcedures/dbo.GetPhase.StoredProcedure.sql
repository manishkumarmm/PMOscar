IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetPhase') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPhase]
/****** Object:  StoredProcedure [dbo].[GetPhase]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetPhase]            
  @PhaseID int=null       
AS            
            
Begin            
    if  (@PhaseID is not null)      
	 Begin
		Select * From Phase 
		Where PhaseID =@PhaseID
	 End
	
	Else
	Begin
		Select * 
		From Phase 
		Order By PhaseID 
	End

     
            
End 
GO

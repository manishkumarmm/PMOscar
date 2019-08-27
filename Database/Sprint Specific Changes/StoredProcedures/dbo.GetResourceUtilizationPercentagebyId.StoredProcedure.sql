IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetResourceUtilizationPercentagebyId') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetResourceUtilizationPercentagebyId]

GO
/****** Object:  StoredProcedure [dbo].[GetResourceUtilizationPercentagebyId]    Script Date: 08/3/2018 1:02:16 PM 
Created By: Kochurani Kuriakose
Created On: 08/03/2018
******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[GetResourceUtilizationPercentagebyId]      
@Id int
AS    
  
      
Begin      
        
   select 
   ResourceUtilizationPercentageID,
   ResourceID,
   StartDate,
   EndDate,
   UtilizationPercentage,
   AdjustmentFactor,
   CreatedBy,
   CreatedDate,
   UpdatedBy,
   UpdatedDate
   from ResourceUtilizationPercentage
   where ResourceUtilizationPercentageID=@Id
      
End 

GO

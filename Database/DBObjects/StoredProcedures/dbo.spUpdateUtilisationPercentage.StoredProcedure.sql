IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'spUpdateUtilisationPercentage') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spUpdateUtilisationPercentage]

GO

/****** Object:  StoredProcedure [dbo].[spUpdateUtilisationPercentage]    Script Date: 8/6/2018 11:23:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================  
-- Author:  Vibin MB
-- Create date: 03/08/2018 
-- Description: Sp to UpdateUtilization Percentage for trainees
-- =============================================  
CREATE PROCEDURE [dbo].[spUpdateUtilisationPercentage]  
 @ResourceUtilizationPercentageID int,  
 @ResourceID int,  
 @StartDate datetime,  
 @EndDate datetime,  
 @UtilizationPercentage decimal(18,2),  
 @AdjustmentFactor decimal(18,2),  
 @CreatedBy int,
 @Createddate datetime,
 @UpdatedBy int,
 @UpdatedDate datetime 

   
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
     
 
      
    Update [dbo].[ResourceUtilizationPercentage] 
Set
	ResourceID=@ResourceID,   
    StartDate=@StartDate,
    EndDate=@EndDate ,
    UtilizationPercentage=@UtilizationPercentage,
    AdjustmentFactor=@AdjustmentFactor,
    CreatedBy=@CreatedBy,
    CreatedDate=@Createddate,
    UpdatedBy=@UpdatedBy ,
    UpdatedDate=@UpdatedDate

where ResourceUtilizationPercentageID=@ResourceUtilizationPercentageID
End     

GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'spInsertUtilisationPercentage') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spInsertUtilisationPercentage]

GO

/****** Object:  StoredProcedure [dbo].[spInsertUtilisationPercentage] ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================  
-- Author:  Vibin MB
-- Create date: 03/08/2018 
-- Description: Sp to insert Utilization Percentage
-- =============================================  
CREATE PROCEDURE [dbo].[spInsertUtilisationPercentage]  
   
 @ResourceID int,  
 @StartDate datetime=null,
 @StartDate1 datetime=null,
 @StartDate2 datetime=null,
 @StartDate3 datetime=null,
 @StartDate4 datetime=null,  
 @EndDate datetime=null,
 @EndDate1 datetime=null,
 @EndDate2 datetime=null,
 @EndDate3 datetime=null,
 @EndDate4 datetime=null,          
 @UtilizationPercentage decimal(18,2)=null, 
 @UtilizationPercentage1 decimal(18,2)=null,
 @UtilizationPercentage2 decimal(18,2)=null,
 @UtilizationPercentage3 decimal(18,2)=null,
 @UtilizationPercentage4 decimal(18,2)=null,     
 @AdjustmentFactor decimal(18,2)=null,  
 @CreatedBy int = null,
 @Createddate datetime = null,
 @UpdatedBy int = null,
 @UpdatedDate datetime=null,
 @IsTrainee bit 

   
AS  
BEGIN  
   
 SET NOCOUNT ON;  
  
     
   --Begin Transaction  
if( @IsTrainee=1)
begin      
    insert into [dbo].[ResourceUtilizationPercentage] 
	(
	ResourceID,
	StartDate,
    EndDate,
    UtilizationPercentage,
    AdjustmentFactor,
    CreatedBy,
    CreatedDate,
    UpdatedBy,
    UpdatedDate
	)
  values
  (
  @ResourceID,  
 @StartDate,  
 @EndDate,  
 @UtilizationPercentage,  
 @AdjustmentFactor,  
 @CreatedBy,
 @Createddate,
 @UpdatedBy,
 @UpdatedDate 
  ),
  (
  @ResourceID,  
 @StartDate1,  
 @EndDate1,  
 @UtilizationPercentage1,  
 @AdjustmentFactor,  
 @CreatedBy,
 @Createddate,
 @UpdatedBy,
 @UpdatedDate 
  )
  ,
  (
  @ResourceID,  
 @StartDate2,  
 @EndDate2,  
 @UtilizationPercentage2,  
 @AdjustmentFactor,  
 @CreatedBy,
 @Createddate,
 @UpdatedBy,
 @UpdatedDate 
  )
  ,
  (
  @ResourceID,  
 @StartDate3,  
 @EndDate3,  
 @UtilizationPercentage3,  
 @AdjustmentFactor,  
 @CreatedBy,
 @Createddate,
 @UpdatedBy,
 @UpdatedDate 
  )
  ,
  (
  @ResourceID,  
 @StartDate4,  
 @EndDate4,  
 @UtilizationPercentage4,  
 @AdjustmentFactor,  
 @CreatedBy,
 @Createddate,
 @UpdatedBy,
 @UpdatedDate 
  )
  end
  else
	begin
		insert into [dbo].[ResourceUtilizationPercentage] 
	(
	ResourceID,
	StartDate,
    EndDate,
    UtilizationPercentage,
    AdjustmentFactor,
    CreatedBy,
    CreatedDate,
    UpdatedBy,
    UpdatedDate
	)
  values
  (
  @ResourceID,  
 @StartDate,  
 @EndDate,  
 @UtilizationPercentage,  
 @AdjustmentFactor,  
 @CreatedBy,
 @Createddate,
 @UpdatedBy,
 @UpdatedDate 
  )
	end
End    
GO



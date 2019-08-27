IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'UpdateUtilisationPercentage') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateUtilisationPercentage]

/****** Object:  StoredProcedure [dbo].[UpdateUtilisationPercentage]    Script Date: 12/28/2018 10:45:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO












-- =============================================  
-- Author:  Haritha E.S
-- Create date: 26/12/2018 
-- Description: Sp to update Utilization Percentage
-- =============================================  
CREATE PROCEDURE [dbo].[UpdateUtilisationPercentage]  
   
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
 @IsTrainee bit, 
 @Status int=null


   
AS  
BEGIN  
   
 SET NOCOUNT ON;  
 
  
    if(@status=1) -- trainee to developer
	BEGIN
	delete from ResourceUtilizationPercentage where ResourceID=@ResourceID  
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
	END
   ELSE IF(@Status=2)  --developer to trainee
	BEGIN
	delete from ResourceUtilizationPercentage where ResourceID=@ResourceID  
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
	END
	ELSE IF not EXISTS (select * from Resource where ResourceId=@ResourceID and JoinDate=@StartDate) --NORMAL SCENARIO--updation of start date
	BEGIN
	delete from ResourceUtilizationPercentage where ResourceID=@ResourceID  
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
	END
End 
GO



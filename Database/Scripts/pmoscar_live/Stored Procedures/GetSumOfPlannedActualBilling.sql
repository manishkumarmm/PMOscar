/****** Object:  StoredProcedure [dbo].[GetSumOfPlannedActualBilling]    Script Date: 09/19/2012 10:22:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSumOfPlannedActualBilling]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSumOfPlannedActualBilling]
GO


/****** Object:  StoredProcedure [dbo].[GetSumOfPlannedActualBilling]    Script Date: 09/19/2012 10:22:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


      
-- sp to delete billing details       
CREATE Procedure  [dbo].[GetSumOfPlannedActualBilling]  
(      
 @Month int,
	@ProjectID int,
	@Year int

)      
AS      
      
       
   Begin     
select sum(PlannedHours) as plannedTotal,sum(ActualHours) as actualTotal from BillingDetails where ProjectID=@ProjectID and DATEPART(month,fromDate)=@Month and DATEPART(year,fromDate)=@Year
   End
GO



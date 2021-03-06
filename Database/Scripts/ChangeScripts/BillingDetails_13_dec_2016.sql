

/**************** Script for Billing Details  *****************************/



-- ==========================================================================================
-- Stored Procedures for Billing 
-- ==========================================================================================


/****** Object:  StoredProcedure [dbo].[GetBillingDetailsByMonthYear]    Script Date: 10/30/2012 10:44:25 ******/
DROP PROCEDURE [dbo].[GetBillingDetailsByMonthYear]
GO

/****** Object:  StoredProcedure [dbo].[GetBillingDetailsByMonthYear]    Script Date: 10/30/2012 10:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
-- get the billing details for the selected month and year

CREATE PROCEDURE [dbo].[GetBillingDetailsByMonthYear]
	-- Add the parameters for the stored procedure here
	
	@Month nvarchar(50),
	@year int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  
 ;WITH sums AS 
(
select 
	bd.ProjectID,
	p.ProjectName,
	sum(bd.ActualHours) AS totalBillableHrs,
	(select sum(billDet.ActualHours) from BillingDetails billDet 
	where MONTH(billDet.ToDate) <= CAST(@Month  as int) - 1 
	and 
	YEAR(billDet.ToDate) <= @year and billDet.ProjectID = bd.ProjectID) as totalBilledTillLastMonth,
	(select sum(billDet.ActualHours) from BillingDetails billDet 
	where MONTH(billDet.ToDate) = CAST(@Month  as int) and YEAR(billDet.ToDate) = @year   and billDet.ProjectID = bd.ProjectID) as ActualHours,
	sum(bd.PlannedHours) as plannedTotal,
	(select distinct SUBSTRING(CONVERT(VARCHAR(11),fromDate,106),3,9) from BillingDetails where DATEPART(month,fromDate)=@Month  and DATEPART(year,fromDate)=@year) as month,
	bd.Freeze
from BillingDetails bd
 INNER JOIN Project p ON p.ProjectId = bd.ProjectID
 where DATEPART(month,bd.fromDate)= CAST(@Month  as int) and DATEPART(year,bd.fromDate)=@year
 GROUP BY bd.ProjectID,p.ProjectName,bd.Freeze
 )
 
 select s.ProjectID,
 s.ProjectName,
 isnull(s.totalBillableHrs,0) totalBillableHrs,
 isnull(s.totalBilledTillLastMonth,0) totalBilledTillLastMonth,isnull(s.ActualHours,0) ActualHours,
 isnull(s.totalBilledTillLastMonth,0)+ (select sum(b.ActualHours) from BillingDetails b where MONTH(b.fromDate) = CAST(@Month  as int) and MONTH(b.ToDate) = CAST(@Month as int) and YEAR(b.fromDate) = @year and YEAR(b.ToDate) = @year and b.ProjectID = s.ProjectID) as totalBilled,
 isnull(s.plannedTotal,0) plannedTotal,
 s.month,
 s.Freeze
 
 from sums s
 -- ORIGINAL BACKUP
 --select  bd.ProjectID,p.ProjectName,sum(bd.PlannedHours) as plannedTotal,sum(bd.ActualHours) as actualTotal, (select distinct SUBSTRING(CONVERT(VARCHAR(11),fromDate,106),3,9) from BillingDetails where DATEPART(month,fromDate)=@Month and DATEPART(year,fromDate)=@year) as month, bd.Freeze
 --from BillingDetails bd
 --INNER JOIN Project p ON p.ProjectId = bd.ProjectID
 --where DATEPART(month,bd.fromDate)=@Month and DATEPART(year,bd.fromDate)=@year
 --GROUP BY bd.ProjectID,p.ProjectName,bd.Freeze
 --ORIGINAL BACKUP
END
 GO




 
 
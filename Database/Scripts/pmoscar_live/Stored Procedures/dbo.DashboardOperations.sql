SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DashboardOperations]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DashboardOperations]
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DashboardOperations]
	@Name varchar(50),
	@FromDate datetime,
	@ToDate datetime,
	@PeriodType  char(1),
	@Status char(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF not exists(select DashboardID from [dbo].Dashboard where FromDate=@FromDate and ToDate=@ToDate )    
     begin
     
     Insert into dbo.Dashboard(Name,FromDate,ToDate,PeriodType,Status)
     values(@Name,@FromDate,@ToDate,@PeriodType,@Status)
     return SCOPE_IDENTITY();
     
     end 
    else
     begin
      Declare @ID int
      set @ID= (Select DashboardID from [dbo].Dashboard where FromDate=@FromDate and ToDate=@ToDate); 
      Delete from dbo.ProjectDashboardEstimation where DashboardID=@ID;
      Delete from  dbo.ProjectDashboard where DashboardID=@ID;
      return @ID;
     end  
    
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


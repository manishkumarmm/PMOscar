IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DashboardOperations') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DashboardOperations]
/****** Object:  StoredProcedure [dbo].[DashboardOperations]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================        
-- Author:  <Author,,Name>        
-- Create date: <Create Date,,>        
-- Description: <Description,,>        
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
         
      return @ID;      
              
     end          
            
END 
GO

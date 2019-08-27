
/****** Object:  StoredProcedure [dbo].[BillingDetailsCheckDuplicate]    Script Date: 09/19/2012 10:19:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BillingDetailsCheckDuplicate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BillingDetailsCheckDuplicate]
GO


/****** Object:  StoredProcedure [dbo].[BillingDetailsCheckDuplicate]    Script Date: 09/19/2012 10:19:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


      
-- sp to delete billing details       
CREATE Procedure  [dbo].[BillingDetailsCheckDuplicate]  
(      
 @ResourceID int,   
@BillingID int,
@ProjectID int,
@RoleID int,
@month int,
@year int

)      
AS      
      
       
   Begin     
select * from BillingDetails where ResourceID=@ResourceID and ProjectID = @ProjectID  and RoleID =@RoleID and BillingID=@BillingID and DATEPART(month,fromDate)=@month AND DATEPART(year,fromDate)=@year
   
   End

GO



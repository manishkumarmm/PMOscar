
/****** Object:  StoredProcedure [dbo].[CountofBillingDetailsbyResourceRoleProject]    Script Date: 09/19/2012 10:21:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountofBillingDetailsbyResourceRoleProject]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CountofBillingDetailsbyResourceRoleProject]
GO


/****** Object:  StoredProcedure [dbo].[CountofBillingDetailsbyResourceRoleProject]    Script Date: 09/19/2012 10:21:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


      
-- sp to delete billing details       
CREATE Procedure  [dbo].[CountofBillingDetailsbyResourceRoleProject]  
(      
 @ResourceID int,   
@ProjectID int,
@RoleID int,
@month int,
@year int

)      
AS      
      
       
   Begin     
select count(*)from BillingDetails where ResourceID=@ResourceID and ProjectID = @ProjectID and RoleID =@RoleID  and DATEPART(month,fromDate)=@month AND DATEPART(year,fromDate)=@year  
   End
   
   
 

GO



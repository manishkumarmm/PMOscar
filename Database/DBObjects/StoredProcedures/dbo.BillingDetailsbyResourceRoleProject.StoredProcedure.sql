IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'BillingDetailsbyResourceRoleProject') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BillingDetailsbyResourceRoleProject] 
/****** Object:  StoredProcedure [dbo].[BillingDetailsbyResourceRoleProject]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- sp to delete billing details       
CREATE Procedure  [dbo].[BillingDetailsbyResourceRoleProject]  
(      
 @ResourceID int,   
@ProjectID int,
@RoleID int,
@month int,
@year int
)      
AS      
      
       
   Begin     
select * from BillingDetails where ResourceID=@ResourceID and ProjectID = @ProjectID and RoleID =@RoleID and DATEPART(month,fromDate)=@month AND DATEPART(year,fromDate)=@year 
   End
GO

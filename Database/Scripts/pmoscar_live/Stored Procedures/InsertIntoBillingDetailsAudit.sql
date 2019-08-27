
/****** Object:  StoredProcedure [dbo].[InsertIntoBillingDetailsAudit]    Script Date: 09/19/2012 10:18:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertIntoBillingDetailsAudit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertIntoBillingDetailsAudit]
GO

/****** Object:  StoredProcedure [dbo].[InsertIntoBillingDetailsAudit]    Script Date: 09/19/2012 10:18:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


      
-- sp to insert billing details that is deleted from billingDetails table       
Create Procedure  [dbo].[InsertIntoBillingDetailsAudit]  
(      
    
@BillingID int
)      
AS      
      
       
   Begin     
Insert into BillingDetailsAudit(BillingID,ResourceID,ProjectID,RoleID,PlannedHours,ActualHours,FromDate,ToDate,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,[Status])select * from BillingDetails  where BillingID= @BillingID
   
   End

GO



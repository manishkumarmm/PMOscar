/****** Object:  StoredProcedure [dbo].[DeleteBillingDetails]    Script Date: 09/19/2012 10:19:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteBillingDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteBillingDetails]
GO


/****** Object:  StoredProcedure [dbo].[DeleteBillingDetails]    Script Date: 09/19/2012 10:19:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


      
-- sp to delete billing details       
CREATE Procedure  [dbo].[DeleteBillingDetails]  
(      
  
@BillingID int
)      
AS      
        
   Begin     
delete FROM BillingDetails where BillingID= @BillingID
   
   End

GO



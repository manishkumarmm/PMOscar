IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DeleteBillingDetails') AND type in (N'P', N'PC'))
DROP Procedure [dbo].[DeleteBillingDetails]
/****** Object:  StoredProcedure [dbo].[DeleteBillingDetails]    Script Date: 4/19/2018 1:02:16 PM ******/
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

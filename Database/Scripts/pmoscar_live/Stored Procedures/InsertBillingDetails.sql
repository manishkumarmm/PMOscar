
/****** Object:  StoredProcedure [dbo].[InsertBillingDetails]    Script Date: 09/19/2012 10:23:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertBillingDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertBillingDetails]
GO

/****** Object:  StoredProcedure [dbo].[InsertBillingDetails]    Script Date: 09/19/2012 10:23:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

      
CREATE Procedure  [dbo].[InsertBillingDetails]  
(      
  @ResourceId  int,
 @ProjectId int,      
 @RoleId int, 
 @Planned int,
 @Actual int,
 @Actualspent int,
 @FromDate datetime,
 @ToDate datetime,  
 @CreatedBy int,      
 @CreatedDate datetime,      
 @UpdatedBy int,      
 @UpdatedDate datetime ,
 @opmode nvarchar(20),  
 @editResource int,  
 @editRole int,
 @status int,
 @BillingID int,
 @StatusFlag int,
 @Comments nvarchar(MAX)
)      
AS      
      
Begin     


if(@opmode ='Add')
begin
	if (@StatusFlag=1)
	begin
		Insert into BillingDetails       
		(      
     
		ResourceID, 
		ProjectID,   
		RoleID,
		PlannedHours,
		ActualHours,
		FromDate, 
		ToDate,
		CreatedBy,      
		CreatedDate,      
		UpdatedBy,      
		UpdatedDate,
		[Status],  
        [UBT],
        ActualSpentHours,
        Comments
      
		)       
         
   Values      
   (      
    
     @ResourceId,  
     @ProjectId,
     @RoleId, 
     @Planned,
     @Actual,   
     @FromDate,
     @ToDate,
     @CreatedBy,      
     @CreatedDate,      
     @UpdatedBy,      
     @UpdatedDate,  
     @status,
     'Yes',
     @Actualspent,
     @Comments
      
   )      
   End 
   
   else if (@StatusFlag=0) 
   begin
   
		Insert into BillingDetails       
		(      
     
		ResourceID, 
		ProjectID,   
		RoleID,
		PlannedHours,
		ActualHours,
		FromDate, 
		ToDate,
		CreatedBy,      
		CreatedDate,      
		UpdatedBy,      
		UpdatedDate,
		[Status] ,
		 ActualSpentHours,
		 Comments
        
      
		)       
         
   Values      
   (      
    
     @ResourceId,  
     @ProjectId,
     @RoleId, 
     @Planned,
     @Actual,   
     @FromDate,
     @ToDate,
     @CreatedBy,      
     @CreatedDate,      
     @UpdatedBy,      
     @UpdatedDate,  
     @status,
     @Actualspent,
     @Comments
      
   )      
   End
   End   
   else if(@opmode='Edit')

   begin
   
   
	if (@StatusFlag=1)
	begin
     
		update BillingDetails set ResourceID=@ResourceId,ProjectID=@ProjectId,RoleID=@RoleId,PlannedHours=@Planned,[Status]=@status,
		ActualHours=@Actual,ActualSpentHours=@Actualspent,FromDate=@FromDate,ToDate=@ToDate,CreatedBy=@CreatedBy,CreatedDate=@CreatedDate,
		UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate,UBT='Yes', Comments=@Comments where ProjectID= @ProjectId and ResourceID=@editResource and RoleID=@editRole and BillingID=@BillingID;
   
   end
   
   else if(@StatusFlag=0)
   begin
		update BillingDetails set ResourceID=@ResourceId,ProjectID=@ProjectId,RoleID=@RoleId,PlannedHours=@Planned,[Status]=@status,
		ActualHours=@Actual,ActualSpentHours=@Actualspent,FromDate=@FromDate,ToDate=@ToDate,CreatedBy=@CreatedBy,CreatedDate=@CreatedDate,
		UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate, Comments=@Comments where ProjectID= @ProjectId and ResourceID=@editResource and RoleID=@editRole and BillingID=@BillingID;
   end
   
   end
   
   End
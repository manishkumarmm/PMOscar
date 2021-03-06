
/**************** Script for Billing Details Module *****************************/

--===============================================================================
-- Create tables (BillingDetails, BillingDetailsAudit) for Billing Details module
--===============================================================================

/****** Object:  Table [dbo].[BillingDetails]    Script Date: 10/30/2012 10:18:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BillingDetails](
	[BillingID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[ResourceID] [int] NOT NULL,
	[PlannedHours] [int] NULL,
	[ActualHours] [int] NULL,
	[fromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[Status] [varchar](1) NULL,
 CONSTRAINT [PK_BillingDetails] PRIMARY KEY CLUSTERED 
(
	[BillingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BillingDetailsAudit]    Script Date: 10/30/2012 10:18:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BillingDetailsAudit](
	[BillingAuditID] [int] IDENTITY(1,1) NOT NULL,
	[BillingID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[ResourceID] [int] NOT NULL,
	[PlannedHours] [int] NULL,
	[ActualHours] [int] NULL,
	[fromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[Status] [varchar](1) NULL,
 CONSTRAINT [PK_BillingDetailsAudit] PRIMARY KEY CLUSTERED 
(
	[BillingAuditID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

//********************************* End of Create Tables ************************************//

-- ==========================================================================================
-- Stored Procedures for Billing module
-- ==========================================================================================

/****** Object:  StoredProcedure [dbo].[DeleteBillingDetails]    Script Date: 10/30/2012 10:44:25 ******/
DROP PROCEDURE [dbo].[DeleteBillingDetails]
GO
/****** Object:  StoredProcedure [dbo].[CountofBillingDetailsbyResourceRoleProject]    Script Date: 10/30/2012 10:44:25 ******/
DROP PROCEDURE [dbo].[CountofBillingDetailsbyResourceRoleProject]
GO
/****** Object:  StoredProcedure [dbo].[BillingDetailsCheckDuplicate]    Script Date: 10/30/2012 10:44:25 ******/
DROP PROCEDURE [dbo].[BillingDetailsCheckDuplicate]
GO
/****** Object:  StoredProcedure [dbo].[BillingDetailsbyResourceRoleProject]    Script Date: 10/30/2012 10:44:25 ******/
DROP PROCEDURE [dbo].[BillingDetailsbyResourceRoleProject]
GO
/****** Object:  StoredProcedure [dbo].[GetProjectWiseBillingDetails]    Script Date: 10/30/2012 10:44:25 ******/
DROP PROCEDURE [dbo].[GetProjectWiseBillingDetails]
GO
/****** Object:  StoredProcedure [dbo].[InsertIntoBillingDetailsAudit]    Script Date: 10/30/2012 10:44:25 ******/
DROP PROCEDURE [dbo].[InsertIntoBillingDetailsAudit]
GO
/****** Object:  StoredProcedure [dbo].[InsertBillingDetails]    Script Date: 10/30/2012 10:44:25 ******/
DROP PROCEDURE [dbo].[InsertBillingDetails]
GO
/****** Object:  StoredProcedure [dbo].[GetSumOfPlannedActualBilling]    Script Date: 10/30/2012 10:44:25 ******/
DROP PROCEDURE [dbo].[GetSumOfPlannedActualBilling]
GO
/****** Object:  StoredProcedure [dbo].[GetAllProjects]    Script Date: 10/30/2012 10:44:25 ******/
DROP PROCEDURE [dbo].[GetAllProjects]
GO
/****** Object:  StoredProcedure [dbo].[GetProjectNameById]    Script Date: 10/30/2012 10:44:25 ******/
DROP PROCEDURE [dbo].[GetProjectNameById]
GO
/****** Object:  StoredProcedure [dbo].[GetBillingDetailsByMonthYear]    Script Date: 10/30/2012 10:44:25 ******/
DROP PROCEDURE [dbo].[GetBillingDetailsByMonthYear]
GO
/****** Object:  StoredProcedure [dbo].[GetBillingDetails]    Script Date: 10/30/2012 10:44:25 ******/
DROP PROCEDURE [dbo].[GetBillingDetails]
GO
/****** Object:  StoredProcedure [dbo].[GetAllResource]    Script Date: 10/30/2012 10:44:25 ******/
DROP PROCEDURE [dbo].[GetAllResource]
GO
/****** Object:  StoredProcedure [dbo].[ExportBillingDetails]    Script Date: 10/30/2012 10:44:25 ******/
DROP PROCEDURE [dbo].[ExportBillingDetails]
GO
/****** Object:  StoredProcedure [dbo].[GetBillingDetailsByResourceID]    Script Date: 10/30/2012 10:44:25 ******/
DROP PROCEDURE [dbo].[GetBillingDetailsByResourceID]
GO
/****** Object:  StoredProcedure [dbo].[GetResourcenameId]    Script Date: 10/30/2012 10:44:25 ******/
DROP PROCEDURE [dbo].[GetResourcenameId]
GO
/****** Object:  StoredProcedure [dbo].[GetResourcenameId]    Script Date: 10/30/2012 10:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dona
-- Create date: 8-5-2012
-- Description:	procedure to get team details for a resource
-- =============================================
-- display the billing details for a particular project

	CREATE  PROCEDURE [dbo].[GetResourcenameId]
	-- Add the parameters for the stored procedure here
	
 @editid int
	
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select ResourceName,ResourceId FROM Resource where ResourceId = @editid
END
GO
/****** Object:  StoredProcedure [dbo].[GetBillingDetailsByResourceID]    Script Date: 10/30/2012 10:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dona
-- Create date: 8-5-2012
-- Description:	procedure to get team details for a resource
-- =============================================
-- display the billing details for a particular project

CREATE PROCEDURE [dbo].[GetBillingDetailsByResourceID]
	-- Add the parameters for the stored procedure here
	
	@ResourceID int,
	@RoleId int,
	@Project int,
	@month int,
	@year int
	
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 select bd.BillingID,rs.ResourceName,bd.ResourceId,p.ProjectId,bd.RoleId,r.[Role],bd.PlannedHours,bd.ActualHours,CONVERT( Varchar(12), bd.fromDate,103) as fromDate ,CONVERT( Varchar(12), bd.ToDate,103) as ToDate
  FROM BillingDetails bd
  INNER JOIN [Resource] rs ON rs.ResourceId =bd.ResourceId
  INNER JOIN [Role] r ON r.RoleId = bd.RoleId
  INNER JOIN [Project] p ON p.ProjectId = bd.ProjectID
  where rs.ResourceId=bd.ResourceId AND r.RoleId=bd.RoleId AND bd.ProjectID =@Project AND DATEPART(month,fromDate)=@month AND DATEPART(year,fromDate)=@year

END
GO
/****** Object:  StoredProcedure [dbo].[ExportBillingDetails]    Script Date: 10/30/2012 10:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dona
-- Create date: 8-5-2012
-- Description:	procedure to get team details for a resource
-- =============================================
--sp to get the billing details for selected month and year.... used for exporting data

CREATE PROCEDURE [dbo].[ExportBillingDetails]
	-- Add the parameters for the stored procedure here
	
	@month int,
	@year int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
select p.ProjectName,SUBSTRING(CONVERT(VARCHAR(11),bd.fromDate,106),3,9) as Month,rs.ResourceName,r.Role,
bd.PlannedHours as PlannedBillable,bd.ActualHours as ActualBillable
  FROM BillingDetails bd
  INNER JOIN [Resource] rs ON rs.ResourceId =bd.ResourceId
  INNER JOIN [Role] r ON r.RoleId = bd.RoleId
  INNER JOIN [Project] p ON p.ProjectId = bd.ProjectID
  where DATEPART(month,fromDate)=@month AND DATEPART(year,fromDate)=@year order by p.ProjectName

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllResource]    Script Date: 10/30/2012 10:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetAllResource]      
     
AS      
      
Begin      
      
select ResourceId,ResourceName from Resource order by ResourceName
      
End
GO
/****** Object:  StoredProcedure [dbo].[GetBillingDetails]    Script Date: 10/30/2012 10:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dona
-- Create date: 8-5-2012
-- Description:	procedure to get team details for a resource
-- =============================================
-- display the billing details for a particular project

	CREATE  PROCEDURE [dbo].[GetBillingDetails]
	-- Add the parameters for the stored procedure here
	
 @editid int,
	@BillingID int,
	@projectId int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select r.[Role],CONVERT( Varchar(12), bd.fromDate,103) as fromDate ,CONVERT( Varchar(12), bd.ToDate,103) as ToDate,bd.PlannedHours,bd.ActualHours,rs.ResourceName,bd.ResourceId,p.ProjectName,p.ProjectId,bd.RoleId FROM BillingDetails bd INNER JOIN [Resource] rs ON rs.ResourceId =bd.ResourceId  INNER JOIN [Role] r ON r.RoleId = bd.RoleId INNER JOIN Project p on p.ProjectId = bd.ProjectID where bd.ResourceID =@editid and bd.ProjectID = @projectId and BillingID =@BillingID

END
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

  
 select  bd.ProjectID,p.ProjectName,sum(bd.PlannedHours) as plannedTotal,sum(bd.ActualHours) as actualTotal, (select distinct SUBSTRING(CONVERT(VARCHAR(11),fromDate,106),3,9) from BillingDetails where DATEPART(month,fromDate)=@Month and DATEPART(year,fromDate)=@year) as month
 from BillingDetails bd
 INNER JOIN Project p ON p.ProjectId = bd.ProjectID
 where DATEPART(month,bd.fromDate)=@Month and DATEPART(year,bd.fromDate)=@year
 GROUP BY bd.ProjectID,p.ProjectName
  
  

END
GO
/****** Object:  StoredProcedure [dbo].[GetProjectNameById]    Script Date: 10/30/2012 10:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetProjectNameById]        
    
@projectId int 

AS        
        
Begin       
        
	select ProjectName from Project where ProjectId =@projectId 
	
End
GO
/****** Object:  StoredProcedure [dbo].[GetAllProjects]    Script Date: 10/30/2012 10:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetAllProjects]      
     
AS      
      
Begin      
      
select ProjectId,ProjectName from Project order by ProjectName
      
End
GO
/****** Object:  StoredProcedure [dbo].[GetSumOfPlannedActualBilling]    Script Date: 10/30/2012 10:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- sp to delete billing details       
CREATE Procedure  [dbo].[GetSumOfPlannedActualBilling]  
(      
 @Month int,
	@ProjectID int,
	@Year int

)      
AS      
      
       
   Begin     
select sum(PlannedHours) as plannedTotal,sum(ActualHours) as actualTotal from BillingDetails where ProjectID=@ProjectID and DATEPART(month,fromDate)=@Month and DATEPART(year,fromDate)=@Year
   End
GO
/****** Object:  StoredProcedure [dbo].[InsertBillingDetails]    Script Date: 10/30/2012 10:44:25 ******/
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
 @BillingID int
)      
AS      
      
Begin     


if(@opmode ='Add')
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
     [Status]  
        
      
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
     @status
      
   )      
      
   End   
   else if(@opmode='Edit')

   begin
     update BillingDetails set ResourceID=@ResourceId,ProjectID=@ProjectId,RoleID=@RoleId,PlannedHours=@Planned,[Status]=@status,
     ActualHours=@Actual,FromDate=@FromDate,ToDate=@ToDate,CreatedBy=@CreatedBy,CreatedDate=@CreatedDate,
     UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate where ProjectID= @ProjectId and ResourceID=@editResource and RoleID=@editRole and BillingID=@BillingID;
   
   end
   
   End
GO
/****** Object:  StoredProcedure [dbo].[InsertIntoBillingDetailsAudit]    Script Date: 10/30/2012 10:44:25 ******/
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
/****** Object:  StoredProcedure [dbo].[GetProjectWiseBillingDetails]    Script Date: 10/30/2012 10:44:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--get billing details

CREATE PROCEDURE [dbo].[GetProjectWiseBillingDetails]
	-- Add the parameters for the stored procedure here
	
	@Month nvarchar(50),
	@ProjectID int,
	@Year int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 
   select distinct r.[Role],sum(bd.PlannedHours) as p,sum(bd.ActualHours) as a
   FROM BillingDetails bd 
   INNER JOIN [Role] r ON r.RoleId = bd.RoleId 
   where DATEPART(month,bd.fromDate)=@Month and bd.ProjectID =@ProjectID  and DATEPART(year,bd.fromDate) =@Year
   group by r.[Role]

END
GO
/****** Object:  StoredProcedure [dbo].[BillingDetailsbyResourceRoleProject]    Script Date: 10/30/2012 10:44:25 ******/
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
/****** Object:  StoredProcedure [dbo].[BillingDetailsCheckDuplicate]    Script Date: 10/30/2012 10:44:25 ******/
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
/****** Object:  StoredProcedure [dbo].[CountofBillingDetailsbyResourceRoleProject]    Script Date: 10/30/2012 10:44:25 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteBillingDetails]    Script Date: 10/30/2012 10:44:25 ******/
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

/***************************** End of Stored Procedures ******************************/


/**************************************** End of Stored ProceduresBilling Module Script *********************/
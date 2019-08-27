SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProjectDashboardOperations]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProjectDashboardOperations]
GO

-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[ProjectDashboardOperations]  
 @ProjectID int, 
 @PhaseID int,
 @ClientStatus int,  
 @TimelineStatus int,  
 @BudgetStatus int,  
 @EscalateStatus int,  
 @CreatedBy int,  
 @CreatedDate datetime,  
 @UpdatedBy int,  
 @UpdatedDate datetime,  
 @Status char(1),  
 @DashboardID int  ,
 @ProjectName varchar(50),  
 @ShortName varchar(50),
 @ProjectType char(1),
 @ProjectOwner int,
 @ProjectManager int,
 @DeliveryDate datetime,
 @RevisedDeliveryDate datetime,
 @PMComments nvarchar(max),
 @DeliveryComments nvarchar(max),
 @isActive bit,
 @Comments nvarchar(max)
   
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
   
 if(@Status='I')  
  begin  
	insert into dbo.ProjectDashboard
				(ProjectID,
				PhaseID,
				ClientStatus,
				TimelineStatus,
				BudgetStatus,
				EscalateStatus,  
				CreatedBy,
				CreatedDate,
				UpdatedBy,
				UpdatedDate,
				Comments,
				ProjectName,
				ShortName,				  
				ProjectType,
				ProjectOwner,
				ProjectManager,
				DeliveryDate,
				RevisedDeliveryDate,				
				PMComments,
				DeliveryComments,
				IsActive,
				DashboardID
	)  
				values
				(@ProjectID,
				@PhaseID,   
				@ClientStatus,
				@TimelineStatus,
				@BudgetStatus,
				@EscalateStatus,  
				@CreatedBy,
				@CreatedDate,
				@UpdatedBy,
				@UpdatedDate,
				@Comments,
				@ProjectName, 
				@ShortName,			   		  
				@ProjectType,
				@ProjectOwner,
				@ProjectManager,
				@DeliveryDate,
				@RevisedDeliveryDate,					
				@PMComments,
				@DeliveryComments,
				@IsActive,
				@DashboardID);  
	return SCOPE_IDENTITY();
  end   
  
 else if  (@Status='U')  
 begin  
	update ProjectDashboard set ClientStatus=@ClientStatus,  
	   TimelineStatus=@TimelineStatus,BudgetStatus=@BudgetStatus, EscalateStatus=@EscalateStatus
				,Comments=@Comments where  ProjectDashboardID=@DashboardID;  
	   return @DashboardID;
  end   
   
	else  
      
	begin  
		  Delete from ProjectDashboard where  DashboardID=@DashboardID;  
	end   
     
      
END  
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


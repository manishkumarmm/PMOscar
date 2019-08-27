SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CloneResourcePlanningDetails]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CloneResourcePlanningDetails]
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CloneResourcePlanningDetails]
	@fromdate nvarchar(50),
	@todate nvarchar(50),
	@clonefromdate nvarchar(50),
	@clonetodate nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
DECLARE @ProjectId int
DECLARE @ResourceId int
DECLARE @RoleId int
Declare @EstimatedHours decimal(18,0)
Declare @ActualHours decimal(18,0)
Declare @CreatedBy int
Declare @CreatedDate datetime
Declare @UpdatedBy int
Declare @UpdatedDate datetime
Declare @PhaseID int
Declare @WeeklyComments nvarchar(max)
declare @counter int;    
declare @max int;   
declare @resourcedetails as table (Id int identity(1,1),ProjectId int ,ResourceId int,RoleId int,EstimatedHours decimal(18,0),
ActualHours decimal(18,0),CreatedBy int,CreatedDate datetime,UpdatedBy int,UpdatedDate datetime,PhaseID int,WeeklyComments nvarchar(max));    

insert into @resourcedetails
Select ProjectId,ResourceId,RoleId,EstimatedHours,ActualHours,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,PhaseID,WeeklyComments
From TimeTracker where FromDate>=@fromdate and ToDate<=@todate


 set @counter = 1;    
  select @max = isnull(max(Id), 0) from @resourcedetails;    
      
  while(@counter <= @max)    
  begin    
       
    select @ProjectId = ProjectId,@ResourceId=ResourceId,@RoleId=RoleId,@EstimatedHours=EstimatedHours,@ActualHours=ActualHours,@CreatedBy=CreatedBy,@CreatedDate=CreatedDate,@UpdatedBy=UpdatedBy,@UpdatedDate=UpdatedDate,@PhaseID=PhaseID,@WeeklyComments=WeeklyComments
    from @resourcedetails where Id = @counter;    
       
   insert into TimeTracker(ProjectId,ResourceId,RoleId,FromDate,ToDate,EstimatedHours,ActualHours,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,PhaseID,WeeklyComments)
   values(@ProjectId,@ResourceId,@RoleId,@clonefromdate,@clonetodate,@EstimatedHours,0,@CreatedBy,@CreatedDate,@UpdatedBy,@UpdatedDate,@PhaseID,@WeeklyComments)

       
   set @counter = @counter + 1;    
       
  end   

End
   

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


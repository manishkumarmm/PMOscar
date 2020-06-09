
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'InsertBudgetRevisionLog') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertBudgetRevisionLog]
/****** Object:  StoredProcedure dbo].InsertBudgetRevisionLog   Script Date: 08/03/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==============================================================================
-- Description: Insert BudgetRevisionLog 
-- ==============================================================================
-- Created By : Kochurani Kuriakose
-- Created On : 2018-08-29
-- Updated By : Joshwa George
-- Updated On : 2019-01-15
-- ==============================================================================

--exec InsertBudgetRevisionLog 79,'','','','','','','','','','','',''
CREATE Procedure  [dbo].InsertBudgetRevisionLog 
(   
        @ProjectID int,
		@CreatedBy int,
		@CreatedDate datetime,
		@UpdatedBy int,
		@UpdatedDate datetime,
		@Comments varchar(500),
		@Purpose varchar(500),
		@IsInitialRevision bit,
		@IsMailSent bit,
		@BudgetRevisionName varchar(500),
		@ApprovedDate datetime,
		@RequestedDate datetime,
		@BudgetRevisionID int OUTPUT
)      
AS      
      
Begin     

--This code is to fix the Budget Name issue fastly. This could be populated from the code when get enough time

Declare @LatestBudgetRevisionCount int
		,@ProjectNAme varchar(100)

select @LatestBudgetRevisionCount=	count(1) 
from BudgetRevisionLog 
where ProjectID= @ProjectID

select @ProjectNAme = ProjectName 
from Project 
where ProjectID= @ProjectID

set @BudgetRevisionName= @ProjectNAme+'_BudgetRevision_'+convert(varchar,@LatestBudgetRevisionCount+1)

	insert into BudgetRevisionLog
		(ProjectID,
		CreatedBy ,
		CreatedDate ,
		UpdatedBy ,
		UpdatedDate ,
		Comments ,
		Purpose ,
		IsInitialRevision ,
		IsMailSent,
		BudgetRevisionName
		)
		VALUES
		(
		@ProjectID ,
		@CreatedBy ,
		@CreatedDate ,
		@UpdatedBy ,
		@UpdatedDate ,
		@Comments ,
		@Purpose ,
		@IsInitialRevision ,
		@IsMailSent
		,@BudgetRevisionName
		)
		  SELECT @BudgetRevisionID= SCOPE_IDENTITY() 
    
   End 
     
 
GO




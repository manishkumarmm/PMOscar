
/*=================================================================*/
/* PMOscar Phase-5 database changes script	                       */
/* Date: 29-11-2012					                               */
/*=================================================================*/

-- Adding column 'BillingStartDate' into Resource table
ALTER TABLE dbo.Resource ADD BillingStartDate DateTime NULL

-- Set '01-01-2005' as default BillingStartDate for existing resources
UPDATE [Resource] SET BillingStartDate='01-01-2008'

/******************************************************************/

-- Create procedure to get resource details by resource id
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==============================================================
-- Author     :	Sajid
-- Create Date: 29-11-2012
-- Description:	Procedure to get Resource details by resource id.
-- ==============================================================
CREATE PROCEDURE [dbo].[GetResourceDetailsById] 
		@ResourceID int
AS
	BEGIN
			SELECT ResourceId,ResourceName,R.RoleId,T.TeamID,IsActive,BillingStartDate
			FROM Resource RES 
			INNER JOIN Role R ON R.RoleID = RES.RoleID   
			LEFT JOIN  Team T ON T.TeamID = RES.TeamID OR T.TeamID = NULL 
			WHERE ResourceId=@ResourceID 
	END
/******************************************************************/

-- Update procedure ResourceOperations to add 'BillingStartDate'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
     
ALTER Procedure [dbo].[ResourceOperations]      
(      
      
@ResourceId  int,      
@ResourceName VARCHAR(50),      
@CreatedBy int,      
@CreatedDate datetime,      
@UpdatedBy int,      
@UpdatedDate datetime,      
@OpMode varchar(10),    
@IsActive bit,  
@RoleId int,      
@TeamID int,
@BillingStartDate datetime ='01-01-2005'     
)      
AS      
      
Begin      
  If @OpMode = 'INSERT'      
        
   Begin      
      
    Insert into Resource       
    (      
      
     ResourceName,      
     CreatedBy,      
     CreatedDate,      
     UpdatedBy,      
     UpdatedDate,    
     IsActive,  
     RoleId,
     TeamID,
     BillingStartDate    
    )       
         
   Values      
   (  
     @ResourceName,      
     @CreatedBy,      
     @CreatedDate,      
     @UpdatedBy,      
     @UpdatedDate,    
     @IsActive,  
     @RoleId,
     @TeamID,     
     @BillingStartDate 
   )      
      
   End      
      
 If @OpMode = 'UPDATE'      
        
   Begin      
        
    Update Resource        
        
    Set     
     IsActive =@IsActive,      
     ResourceName = @ResourceName,      
     UpdatedBy = @UpdatedBy,      
     UpdatedDate = @UpdatedDate,  
     RoleId = @RoleId,
     TeamID = @TeamID,         
     BillingStartDate = @BillingStartDate
    Where ResourceId = @ResourceId       
        
   End      
        
 If @OpMode = 'DELETE'      
        
   Begin   
   
   Declare @isactive1 bit;
     set @isactive1=(Select IsActive from Resource where ResourceId = @ResourceId);
      if @isactive1='1'
       begin
        Update Resource   
       Set        
		IsActive = 0        
		Where ResourceId = @ResourceId        
       end
      else
       begin
         Update Resource   
       Set        
		IsActive = 1        
		Where ResourceId = @ResourceId       
       end     
  End      
         
 If @OpMode = 'SELECTALL'      
        
    Begin         
         
		SELECT     
		ResourceId    
	   ,ResourceName    
	   ,IsActive  
	   ,Role
	   ,Team  
	   ,CASE IsActive     
	   WHEN 'True' THEN 'Active'    
	   ELSE 'Inactive'    
	   END As Status1    
	   FROM Resource 
	   Inner Join Role on Role.RoleID = Resource.RoleID Left join Team on Team.TeamID = Resource.TeamID  Order By ResourceName
      
   End      
      
End  

-- ==============================================================
-- Author     :	Sajid
-- Create Date: 19-03-2013
-- Description:	Create new table CompanyUtilizationReport.
-- ==============================================================
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CompanyUtilizationReport](
	[CompanyUtilizationReportID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[ResourceID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[TeamID] [int] NOT NULL,
	[AvailableHours] [int] NULL,
	[BilledHours] [int] NULL,
	[UtilizedButNotBilledHours]  AS ((([Admin]+[Open])+[VAS])+[Proposal]) PERSISTED,
	[Finalize] [bit] NOT NULL,
	[Admin] [int],
	[Open] [int],
	[VAS] [int],
	[Proposal] [int]
 CONSTRAINT [PK_CompanyUtilizationReport] PRIMARY KEY CLUSTERED 
(
	[CompanyUtilizationReportID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/*********************************************************************************************/
-- ====================================================================================
-- Author     :	Riya
-- Create Date: 19-03-2013
-- Description: Procedure to get project details in Company Utilization -Summary Report.
-- =====================================================================================

--EXEC GetCompanyUtilization_Summary
CREATE PROCEDURE [dbo].[GetCompanyUtilization_Summary]
AS
BEGIN
	
	SELECT [Date],Month_Year,AvailableHours
	,BilledHours
	,UtilizedButNotBilledHours
	,[Admin],[Open+VAS],Proposal,Finalize FROM
	(SELECT [Date]
	,datename(month,dateadd(month,MONTH([Date]) - 1, 0))+ ' '+cast(YEAR([Date]) as char(4)) AS Month_Year
	,SUM(AvailableHours) AS AvailableHours
	,ISNULL(SUM(UtilizedButNotBilledHours), 0 ) AS UtilizedButNotBilledHours
	,Finalize
	,ISNULL(SUM([Admin]), 0 ) AS [Admin]
	,SUM(ISNULL([Open], 0 )+ISNULL([VAS], 0 )) AS [Open+VAS]
	,ISNULL(SUM(Proposal), 0 ) AS Proposal
	FROM CompanyUtilizationReport
	GROUP BY [Date]
	,Finalize
	)tbl1
	LEFT JOIN
	(
		SELECT t.[Date] AS tbl2Date,SUM(t.BilledHours) AS BilledHours
		FROM
			(SELECT [Date],[BilledHours] 
			FROM CompanyUtilizationReport
			WHERE BilledHours IS NOT NULL
			GROUP BY ProjectId,[BilledHours],[Date])t
		GROUP BY t.[Date]
	)tbl2
	ON tbl1.[Date] = tbl2.tbl2Date
END

/*********************************************************************************************/

-- ===================================================================================================
-- Author     :	Riya
-- Create Date: 21-03-2013
-- Description: Procedure to get project details in Company Utilization -Summary Report by Month and Year
-- ======================================================================================================

--EXEC GetSummaryReportByMonthYear 2013,3
CREATE PROCEDURE [dbo].[GetSummaryReportByMonthYear]
@Year INT,          
@Month INT
AS
BEGIN
	DECLARE @date1 DATETIME 
	SET @date1 = CONVERT(VARCHAR(10), DATEADD(MONTH,@Month-1,DATEADD(YEAR,@Year-1900,0)), 101);  
	
	SELECT [Date]
	,SUM(AvailableHours) AS AvailableHours
	--,ActualHours
	,CASE WHEN VAS IS NOT NULL THEN 0 ELSE 
	 ISNULL(BilledHours,0) END AS BilledHours,[Admin],[Open],[Proposal],VAS
	,ProjectId,ProjectName
	--,CASE WHEN PhaseId IS NULL THEN 0 ELSE PhaseId END AS PhaseId
	,ResourceId
	,RoleId,TeamId
	FROM
		(SELECT * FROM	
			(SELECT * FROM
				(SELECT * FROM
					(SELECT * FROM
						(SELECT 
						/*Get project details*/
						@date1 AS [Date]
						,pr.ProjectId ProjectId,pr.ProjectName ProjectName
						,tt.EstimatedHours AvailableHours
						,tt.ActualHours ActualHours
						,ph.PhaseId PhaseId
						,rs.ResourceId ResourceId
						,rl.RoleId RoleId
						,tm.TeamId TeamId,tt.TimeTrackerId
						FROM TimeTracker tt
						JOIN [Resource] rs ON rs.ResourceId=tt.ResourceId
						JOIN [Role] rl ON rl.RoleId=tt.RoleId
						LEFT JOIN Phase ph ON ph.PhaseId=tt.PhaseId
						JOIN Project pr ON pr.ProjectId=tt.ProjectId
						JOIN Team tm on tm.TeamId=tt.TeamID
						WHERE (YEAR(tt.FromDate) = @Year AND MONTH(tt.FromDate) = @Month AND @Year >= YEAR(rs.BillingStartDate) AND  @Month >= MONTH(rs.BillingStartDate))
						)tbl
						
						LEFT JOIN 
						/*Get actual billable of each project in given month*/
						(SELECT bd.ProjectId AS BillProjectId,bd.ActualHours AS BilledHours,rl.RoleId AS BillRoleId
						FROM BillingDetails bd
						JOIN [Resource] rs ON rs.ResourceId=bd.ResourceId
						JOIN Team tm ON tm.TeamID = rs.TeamID
						JOIN [Role] rl ON rl.RoleId=bd.RoleId
						WHERE (YEAR(bd.FromDate) = @Year and MONTH(bd.FromDate) = @Month AND @Year >= YEAR(rs.BillingStartDate) AND  @Month >= MONTH(rs.BillingStartDate))
						)bill
						ON bill.BillProjectId = tbl.ProjectId AND bill.BillRoleId = tbl.RoleId
					LEFT JOIN 
					/*Get hours spent in Project 'Admin' in a month*/
					(SELECT 
					tt.ActualHours AS [Admin],tt.TimeTrackerId AS AdTimeTrackerId
					FROM TimeTracker tt
					JOIN [Resource] rs ON rs.ResourceId=tt.ResourceId
					JOIN [Role] rl ON rl.RoleId=tt.RoleId
					LEFT JOIN Phase ph ON ph.PhaseId=tt.PhaseId
					JOIN Project pr ON pr.ProjectId=tt.ProjectId AND ( pr.ProjectId = 2) /* ProjectId = 2 ProjectId  of Admin */
					JOIN Team tm on tm.TeamId=tt.TeamID
					WHERE (YEAR(tt.FromDate) = @Year and MONTH(tt.FromDate) = @Month AND @Year >= YEAR(rs.BillingStartDate) AND  @Month >= MONTH(rs.BillingStartDate))
					)a
					ON a.AdTimeTrackerId = tbl.TimeTrackerId
					) tblAdmin
				LEFT JOIN 
				/*Get hours spent in Project 'Open' in a month*/
				(SELECT 
				tt.ActualHours AS [Open],tt.TimeTrackerId AS OpTimeTrackerId
				FROM TimeTracker tt
				JOIN [Resource] rs ON rs.ResourceId=tt.ResourceId
				JOIN [Role] rl ON rl.RoleId=tt.RoleId
				LEFT JOIN Phase ph ON ph.PhaseId=tt.PhaseId
				JOIN Project pr ON pr.ProjectId=tt.ProjectId AND ( pr.ProjectId = 16) /* ProjectId = 16 ProjectId  of Open */
				JOIN Team tm on tm.TeamId=tt.TeamID
				WHERE (YEAR(tt.FromDate) = @Year and MONTH(tt.FromDate) = @Month AND @Year >= YEAR(rs.BillingStartDate) AND  @Month >= MONTH(rs.BillingStartDate))
				)o	
				ON o.OpTimeTrackerId = tblAdmin.TimeTrackerId
				)tblOpen
			LEFT JOIN
			/*Get hours spent in Project 'Proposal' in a month*/
			(SELECT 
			tt.ActualHours AS [Proposal],tt.TimeTrackerId AS ProTimeTrackerId
			FROM TimeTracker tt
			JOIN [Resource] rs ON rs.ResourceId=tt.ResourceId
			JOIN [Role] rl ON rl.RoleId=tt.RoleId
			LEFT JOIN Phase ph ON ph.PhaseId=tt.PhaseId
			JOIN Project pr ON pr.ProjectId=tt.ProjectId AND ( pr.ProjectId = 57) /* ProjectId = 57 ProjectId  of Proposal */
			JOIN Team tm on tm.TeamId=tt.TeamID
			WHERE (YEAR(tt.FromDate) = @Year and MONTH(tt.FromDate) = @Month AND @Year >= YEAR(rs.BillingStartDate) AND  @Month >= MONTH(rs.BillingStartDate))
			)p
			ON p.ProTimeTrackerId = tblOpen.TimeTrackerId
			)tblProposal
		LEFT JOIN
		/*Get total hours spent in a project's phase - VAS in a month*/
		(SELECT 
		tt.ActualHours VAS,tt.TimeTrackerId AS VASTimeTrackerId
		FROM TimeTracker tt
		JOIN [Resource] rs ON rs.ResourceId=tt.ResourceId
		JOIN [Role] rl ON rl.RoleId=tt.RoleId
		JOIN Phase ph ON ph.PhaseId=tt.PhaseId AND ph.PhaseId = 6 /* PhaseId = 6 PhaseId of VAS */
		JOIN Project pr ON pr.ProjectId=tt.ProjectId
		JOIN Team tm on tm.TeamId=tt.TeamID
		WHERE (YEAR(tt.FromDate) = @Year and MONTH(tt.FromDate) = @Month AND @Year >= YEAR(rs.BillingStartDate) AND  @Month >= MONTH(rs.BillingStartDate))
		)vas
		ON vas.VASTimeTrackerId = tblProposal.TimeTrackerId
		)
		JoinedDetails
		GROUP BY
		ResourceId
		,[Date]
		,BilledHours,[Admin],[Open],[Proposal],VAS
		,ProjectId,ProjectName --,ActualHours,PhaseId
		,RoleId,TeamId
END

/*********************************************************************************************/

-- =======================================================================================
-- Author     :	Riya
-- Create Date: 25-03-2013
-- Description: Procedure to Delete project details from Company Utilization Summary 
-- ========================================================================================

--EXEC DeleteSummaryReportDetails '2013','2'
CREATE PROCEDURE [dbo].[DeleteSummaryReportDetails]
	@Year INT,          
	@Month INT
AS
BEGIN	
	DELETE 
	FROM CompanyUtilizationReport
	WHERE (YEAR([Date]) = @Year and MONTH([Date]) = @Month)
END

/*********************************************************************************************/

-- =======================================================================================
-- Author     :	Riya
-- Create Date: 25-03-2013
-- Description: Procedure to insert project details to Company Utilization -Summary
-- ========================================================================================

--EXEC InsertSummaryReportDetails '2013-03-01 00:00:00.000',83,6,3,1,1,168,NULL,1,NULL ,NULL ,NULL ,50
CREATE PROCEDURE [dbo].[InsertSummaryReportDetails]
	@Date DATETIME,
	@ProjectID INT,
	--@PhaseID INT,
	@ResourceID INT,
	@RoleID INT,
	@TeamID INT,
	@AvailableHours INT=NULL,
	@BilledHours INT=NULL,
	@Finalize BIT,
	@Admin INT=0,
	@Open INT=0,
	@VAS INT=0,
	@Proposal INT=0
AS
BEGIN	
	INSERT 
	INTO CompanyUtilizationReport ([Date],ProjectID
	--,PhaseID
	,ResourceID,RoleID,TeamID,AvailableHours,BilledHours,Finalize,[Admin],[Open],VAS,Proposal)
	VALUES (@Date,@ProjectID
	--,@PhaseID
	,@ResourceID,@RoleID,@TeamID,@AvailableHours,@BilledHours,@Finalize,@Admin,@Open,@VAS,@Proposal)
END

/*********************************************************************************************/

-- =================================================================================
-- Author     :	Riya
-- Create Date: 25-03-2013
-- Description: Procedure to finalize project details to Company Utilization -Summary
-- =================================================================================

CREATE PROCEDURE [dbo].[FinalizeSummaryReportDetails] 
	@Year INT,
	@Month INT
AS
BEGIN
	UPDATE CompanyUtilizationReport 
	SET Finalize=1
	WHERE (YEAR(Date) = @Year and MONTH(Date) = @Month)
END

/*********************************************************************************************/

-- ====================================================================================
-- Author     :	Riya
-- Create Date: 26-03-2013
-- Description: Procedure to get project details in Company Utilization -Detailed Report.
-- =====================================================================================

CREATE PROCEDURE [dbo].[GetCompanyUtilization_Detailed]
@Year INT,          
@Month INT 

AS
BEGIN
	SELECT t.Team,t.[Role],t.RoleId
	,SUM(AvailableHours) AS AvailableHours,SUM(BilledHours) AS BilledHours, SUM(UtilizedButNotBilledHours) AS UtilizedButNotBilledHours
	,SUM([Admin]) AS [Admin],SUM([Open+VAS]) AS [Open+VAS],SUM(Proposal) AS Proposal
	FROM
	(
		SELECT 
			rs.ResourceName,rs.ResourceId ResourceId
			,tm.Team Team,rl.[Role] [Role],rl.RoleId RoleId
			,cu.AvailableHours AvailableHours
			,CASE WHEN cu.BilledHours IS NULL THEN 0 ELSE cu.BilledHours END AS BilledHours
			,CASE WHEN cu.UtilizedButNotBilledHours IS NULL THEN 0 ELSE cu.UtilizedButNotBilledHours END AS UtilizedButNotBilledHours
			,ISNULL(SUM(cu.[Admin]), 0 ) AS [Admin]
			,SUM(ISNULL(cu.[Open], 0 )+ISNULL(cu.[VAS], 0 )) AS [Open+VAS]
			,ISNULL(SUM(cu.Proposal), 0 ) AS Proposal
		FROM CompanyUtilizationReport cu
		JOIN Team tm ON tm.TeamID = cu.TeamID
		JOIN [Role] rl ON rl.RoleId = cu.RoleId
		JOIN [Resource] rs ON rs.ResourceID = cu.ResourceID
		WHERE (YEAR([Date]) = @Year and MONTH([Date]) = @Month)
		GROUP BY cu.AvailableHours,tm.Team,rl.[Role],rs.ResourceName,rs.ResourceId,rl.RoleId,BilledHours,UtilizedButNotBilledHours
	)t
	GROUP BY t.Team,t.[Role],t.RoleId
END


